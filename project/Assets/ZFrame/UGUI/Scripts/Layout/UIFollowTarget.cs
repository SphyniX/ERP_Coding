using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Attaching this script to an object will make it visibly follow another object, even if the two are using different cameras to draw them.
/// 改自NGUI的脚本，以适配Unity5中的官方UI系统使用
/// </summary>
namespace ZFrame.UGUI
{
    /// <summary>
    /// UI对象跟随游戏对象
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class UIFollowTarget : MonoBehaviour
    {
        /// <summary>
        /// 3D target that this object will be positioned above.
        /// </summary>

        public Transform target;

        /// <summary>
        /// Game camera to use.
        /// </summary>

        public Camera gameCamera;

        /// <summary>
        /// UI camera to use.
        /// </summary>

        public Camera uiCamera;

        /// <summary>
        /// Whether the children will be disabled when this object is no longer visible.
        /// </summary>

        public bool disableIfInvisible = false;

        [System.Serializable]
        public class FollowTargetEvent : UnityEvent<bool> { public FollowTargetEvent() { } }        
        public FollowTargetEvent onLeaveScreen = new FollowTargetEvent();

        private RectTransform m_Rect;
        private RectTransform rectTransform { get { if (!m_Rect) m_Rect = GetComponent<RectTransform>(); return m_Rect; } }

        private RectTransform cvRect;

        bool mInsideScreen = false;
        bool mIsVisible = false;
        
        /// <summary>
        /// Find both the UI camera and the game camera so they can be used for the position calculations
        /// </summary>

        private void Start()
        {
            Init();
        }

        private void SetInside(bool val)
        {
            mInsideScreen = val;
            if (onLeaveScreen != null) {
                onLeaveScreen.Invoke(!val);
            }
        }

        /// <summary>
        /// Enable or disable child objects.
        /// </summary>

        private void SetVisible(bool val)
        {
            mIsVisible = val;

            for (int i = 0, imax = rectTransform.childCount; i < imax; ++i) {
                rectTransform.GetChild(i).gameObject.SetActive(val);
            }
        }

        /// <summary>
        /// Update the position of the HUD object every frame such that is position correctly over top of its real world object.
        /// </summary>

        private void Update()
        {
            if (target == null || gameCamera == null) return;
            
            if (uiCamera) {
                Vector3 pos = gameCamera.WorldToScreenPoint(target.position);
                
                // Determine the visibility and the target alpha
                bool insideScreen =  pos.z > 0 && gameCamera.pixelRect.Contains(pos);
                bool isVisible = !disableIfInvisible || insideScreen;

                if (mInsideScreen != insideScreen) SetInside(insideScreen);
                // Update the visibility flag
                if (mIsVisible != isVisible) SetVisible(isVisible);

                // If visible, update the position
                if (isVisible) {
                    Vector2 anchoredPos;
                    if (RectTransformUtility.ScreenPointToLocalPointInRectangle(cvRect, pos, uiCamera, out anchoredPos)) {
                        rectTransform.anchoredPosition = anchoredPos;
                    }
                }
                OnUpdate(isVisible);
            } else {
                Vector3 pos = gameCamera.WorldToScreenPoint(target.position);

                bool isVisible = true;
                if (mIsVisible != isVisible) SetVisible(isVisible);

                if (isVisible) {
                    rectTransform.position = pos.SetPositionZ(0);
                }
                OnUpdate(isVisible);
            }
        }

		private void OnEnable()
		{
			Update();
		}

        /// <summary>
        /// Custom update function.
        /// </summary>

        protected virtual void OnUpdate(bool isVisible) { }

        public Transform followTarget { set { target = value; Init(); } get { return target; } }
        public void Init()
        {
            if (target != null) {
                gameCamera = target.gameObject.FindCameraForLayer();
                var canvas = rectTransform.GetComponentInParent<Canvas>();
                if (canvas) {
                    uiCamera = canvas.worldCamera;
                    if (!uiCamera) uiCamera = gameObject.FindCameraForLayer();
                    cvRect = rectTransform.parent as RectTransform;

                    enabled = true;
                    Update();
                    return;
                }
            }
            enabled = false;
        }
    }
}