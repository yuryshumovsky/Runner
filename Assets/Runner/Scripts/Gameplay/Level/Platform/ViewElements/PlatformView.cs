using System;
using Runner.Scripts.Gameplay.Interfaces;
using Runner.Scripts.Gameplay.Level.Platform.Data;
using UnityEngine;
using Zenject;

namespace Runner.Scripts.Gameplay.Level.Platform.ViewElements
{
    public class PlatformView : MonoBehaviour, IPoolable<PlatformParams, IMemoryPool>, IDisposable, ILevelItemView
    {
        //[SerializeField] private Material _material;
        private PlatformPassView _passIndicatprObject;

        public Action<PlatformView> OnPassPlatform;
        public Action OnTriggerPlayer;
        public Action OnKillSomeone;

        private IMemoryPool _pool;
        private Vector3 _size;
        private PlatformKillElementView _killElement;
        private MeshRenderer _meshRenderer;

        public Vector3 Size
        {
            get
            {
                if (_size == default)
                {
                    PlatformPositionIndicatorView indicatorView =
                        GetComponentInChildren<PlatformPositionIndicatorView>();
                    _size = indicatorView.GetComponent<Renderer>().bounds.size;
                }

                return _size;
            }
        }

        PlatformPassView PlatformPass
        {
            get
            {
                if (_passIndicatprObject == null)
                {
                    _passIndicatprObject = GetComponentInChildren<PlatformPassView>();
                }

                return _passIndicatprObject;
            }
        }

        private void Start()
        {
            PlatformPass.OnTriggerByPlayer += TriggerByPlayerHandler;
            if (KillElementView)
            {
                KillElementView.OnKillSomeone += KillSomeoneHandler;
            }
        }

        private void KillSomeoneHandler()
        {
            OnKillSomeone?.Invoke();
        }

        private PlatformKillElementView KillElementView
        {
            get
            {
                if (_killElement == null)
                {
                    _killElement = GetComponentInChildren<PlatformKillElementView>();
                }

                return _killElement;
            }
        }

        private void TriggerByPlayerHandler()
        {
            OnPassPlatform?.Invoke(this);
        }

        public Vector3 Position
        {
            set { transform.position = value; }
            get => transform.position;
        }

        public string PlatformName { get; set; }

        public Color Color
        {
            set
            {
                if (!_meshRenderer)
                {
                    _meshRenderer = GetComponent<MeshRenderer>();
                }

                _meshRenderer.material.color = value;
            }
        }

        public void OnDespawned()
        {
        }

        public void OnSpawned(PlatformParams platformParams, IMemoryPool pool)
        {
            _pool = pool;
            gameObject.SetActive(true);
        }

        public void Dispose()
        {
            gameObject.SetActive(false);
            _pool?.Despawn(this);
        }

        public class Factory : PlaceholderFactory<PlatformParams, PlatformView>
        {
        }

        public class FacadePool : MonoPoolableMemoryPool<PlatformParams, IMemoryPool, PlatformView>
        {
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnTriggerPlayer?.Invoke();
        }

        public string ID { get; set; }

        public void HideAndDeactivate()
        {
            //
        }
    }
}