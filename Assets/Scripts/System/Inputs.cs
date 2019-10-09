using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace System
{
    public class Inputs : MonoBehaviour
    {
        public static Vector3 ClickStartPosition = new Vector3(0, 0, 0);
        public static Vector3 ClickPosition = new Vector3(0, 0, 0);
        public static Vector3 ClickScreenStartPosition = new Vector3(0, 0, 0);
        public static Vector3 ClickScreenPosition = new Vector3(0, 0, 0);
        public static bool ClickStarted;
        public static bool DoubleClick;
        public static bool ClickEnded;
        public static bool IsClicking;
        public static float ClickTime;
        public static float ClickTimeDelta;
        public static GameObject ClickedObject;
        public static bool IsClickingObject;
        public static float ClickMagnitude;
        public static float ClickMagnitudeMax;
        public static bool HighMagnitudeClick;
        public static readonly float DoubleClickDelay = 0.2f;

        private static Camera mainCamera;

        private void Start() => mainCamera = Camera.main;

        private void Update()
        {
            ClickStarted = false;
            ClickEnded = false;

#if UNITY_IOS || UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                Vector3 screenPosTouch = Input.GetTouch(0).position;
                ClickScreenPosition = screenPosTouch;
                screenPosTouch.z = 10.0f;
                var posTouch = mainCamera.ScreenToWorldPoint(screenPosTouch);
                posTouch.z = 0;
                ClickPosition = posTouch;
                
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    StartClick(posTouch, screenPosTouch);
                }

                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    EndClick();
                }
            }
#endif

#if UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN

            var screenPos = Input.mousePosition;
            ClickScreenPosition = screenPos;
            screenPos.z = 10.0f;
            var pos = mainCamera.ScreenToWorldPoint(screenPos);
            pos.z = 0;
            ClickPosition = pos;

            if (Input.GetMouseButtonUp(0))
            {
                EndClick();
            }

            if (Input.GetMouseButtonDown(0))
            {
                StartClick(pos, screenPos);
            }

#endif
            void StartClick(Vector3 v, Vector3 v2)
            {
                ClickStartPosition = v;
                ClickScreenStartPosition = v2;
                ClickStarted = true;
                if (ClickTimeDelta < DoubleClickDelay) DoubleClick = true;
                ClickTimeDelta = 0;
                IsClicking = true;
                ClickedObject = null;
                ClickMagnitudeMax = 0;

                var objects = RaycastClick();
                if (objects.Count == 0) return;
                IsClickingObject = true;
                ClickedObject = objects[0].gameObject;
            }

            void EndClick()
            {
                ClickEnded = true;
                DoubleClick = false;
                IsClicking = false;
                IsClickingObject = false;
                ClickTime = 0;
                HighMagnitudeClick = false;
            }

            if (IsClicking)
            {
                ClickTime += Time.deltaTime;
                ClickMagnitude = Vector3.Distance(ClickPosition, ClickStartPosition);
                ClickMagnitudeMax = Math.Max(ClickMagnitude, ClickMagnitudeMax);
                if (ClickMagnitude > 1) HighMagnitudeClick = true;
            }

            ClickTimeDelta += Time.deltaTime;
        }

        public static List<Collider2D> RaycastClick()
        {
            var pos = ClickPosition;
            pos.z = 10;
            var collidersList = Physics2D.OverlapCircleAll(pos, 0.01f).ToList();
            return collidersList;
        }

        public static bool IsUi(GameObject obj)
        {
            return obj.layer == 5;
        }
    }
}