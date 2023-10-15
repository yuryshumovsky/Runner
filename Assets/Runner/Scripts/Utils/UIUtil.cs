using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runner.Scripts.Utils
{
    public class UIUtil
    {
        public static bool IsPointerOverUIObject()
        {
            if (EventSystem.current == null) return false;
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

        public static int HasObject(List<GameObject> listToCheck)
        {
            if (EventSystem.current == null)
            {
                return -1;
            }

            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            for (int i = 0; i < listToCheck.Count; i++)
            {
                for (int j = 0; j < results.Count; j++)
                {
                    if (listToCheck[i] == results[j].gameObject)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
    }
}