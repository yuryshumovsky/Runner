using System.Collections.Generic;
using Runner.Scripts.Gameplay.Interfaces;
using Runner.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace Runner.Scripts.Gameplay.Misc
{
    public class TouchAndMouseHandler : IInitializable, ITickable
    {
        [Inject] private List<IScreenActionController> listOfControllers = new();

        private bool _enable = false;
        private Touch _touch;
        private Vector3 _prevMousePosition;
        private bool _touchMode = false;
        private float prevTapTime = float.MaxValue;
        private bool _mouseReleaseCheckEnable;

        public bool Enable
        {
            set { _enable = value; }
        }

        public void Initialize()
        {
            _touchMode = Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer;
        }

        public void SubscribeNewOne(IScreenActionController actionController)
        {
            listOfControllers.Add(actionController);
        }

        private bool IsOverUI
        {
            get { return UIUtil.IsPointerOverUIObject(); }
        }

        public void Tick()
        {
            if (_enable)
            {
                if (_touchMode)
                {
                    HandleTouch();
                }
                else
                {
                    HandleMouse();
                }
            }
        }

        private void HandleMouse()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mouseReleaseCheckEnable = true;
                _prevMousePosition = Input.mousePosition;
            }

            if (!Input.GetMouseButton(0))
            {
                if (_mouseReleaseCheckEnable)
                {
                    DownOrTapHandler(_prevMousePosition);
                    _mouseReleaseCheckEnable = false;
                }
            }
        }

        private void HandleTouch()
        {
            if (Input.touchCount == 1)
            {
                _touch = Input.GetTouch(0);

                if (_touch.phase == TouchPhase.Began)
                {
                    DownOrTapHandler(_touch.position);
                }
            }
        }

        private void DownOrTapHandler(Vector2 touchPosition)
        {
            float tapDiff = Time.time - prevTapTime;
            if (tapDiff < 0.3f)
            {
                //double time
                foreach (IScreenActionController screenActionController in listOfControllers)
                {
                    screenActionController.DoubleDownOrTapHandler();
                }
            }
            else
            {
                //single tap
                foreach (IScreenActionController screenActionController in listOfControllers)
                {
                    screenActionController.SingleDownOrTapHandler();
                }
            }


            prevTapTime = Time.time;
        }

        public void Unsubscribe(IScreenActionController screenActionController)
        {
            listOfControllers.Remove(screenActionController);
        }
    }
}