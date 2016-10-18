using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

namespace ZFrame.UGUI
{
	public static class UGUITools
    {
#if UNITY_EDITOR
        static public GameObject SelectedRoot(bool createIfMissing)
	    {
	        GameObject go = Selection.activeGameObject;

			if (go && (!go.activeInHierarchy || !go.GetComponentInParent<Canvas>())) go = null;

	        if (go == null) {
	            var cv = GameObject.FindObjectOfType<Canvas>();
                if (cv) {
                    if (cv.transform.parent as RectTransform) {
                        cv = null;
                    }
                }
                go = cv ? cv.gameObject : null;
	        }
	        
	        if (createIfMissing && go == null) {
				var uiLyaer = LayerMask.NameToLayer("UI");

				var root = new GameObject("UIROOT");
				root.transform.position = Vector3.zero;
	            
				var goCam = new GameObject("UICamera", typeof(Camera));
				goCam.transform.SetParent(root.transform);
				goCam.transform.localPosition = Vector3.zero;
				var camera = goCam.GetComponent<Camera>();
                camera.depth = 1;
				camera.clearFlags = CameraClearFlags.Depth;
				camera.cullingMask = 1 << uiLyaer;
                camera.fieldOfView = 30;

				go = new GameObject("UICanvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
				go.transform.SetParent(root.transform);
				go.transform.localPosition = Vector3.zero;

	            var cv = go.GetComponent<Canvas>();
	            cv.renderMode = RenderMode.ScreenSpaceCamera;
                cv.worldCamera = camera;
	            
				var cvScl = go.GetComponent<CanvasScaler>();
	            cvScl.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
				cvScl.referenceResolution = new Vector2(1280, 720);
	            cvScl.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
				cvScl.matchWidthOrHeight = 1;
                
                var goEvt = new GameObject("EventSystems", typeof(UnityEngine.EventSystems.EventSystem));
                goEvt.transform.SetParent(root.transform);

				root.SetLayerRecursively(uiLyaer);
	        }
	        return go;
	    }

	    static Font m_selectedFont;
	    public static Font lastSelectedFont
	    {
	        set { m_selectedFont = value; }
	        get
	        {
	            if (m_selectedFont == null) {
                    var font = AssetDatabase.LoadAssetAtPath("Assets/Artwork/Font/MainFont.TTF", typeof(Font)) as Font;
	                m_selectedFont = font;
	            }
	            return m_selectedFont;
	        }
	    }

		public static void AutoUIRoot(Graphic graphic)
		{
			if (!Application.isPlaying) {
				var canvas = graphic.GetComponentInParent<Canvas>();
                if (!canvas) {
					var root = graphic.rectTransform.root;
					root.SetParent(SelectedRoot(true).transform, false);
					UnityEditor.Selection.activeTransform = root;
				}
			}
		}
#endif
        /// <summary>
        /// 灰度图共用材质
        /// </summary>
        private static Material s_GrayscaleMat;
        public static Material grayScaleMat
        {
            get
            {
                if (s_GrayscaleMat == null) {
                    s_GrayscaleMat = new Material(Shader.Find("UI/Grayscale"));
                }
                return s_GrayscaleMat;
            }
        }

        /// <summary>
        /// 图片模糊公用材质
        /// </summary>
        private static Material s_BlurMat;
        public static Material blurMat
        {
            get
            {
                if (s_BlurMat == null) {
                    s_BlurMat = new Material(Shader.Find("UI/Blur"));
                }
                return s_BlurMat;
            }
        }

        public static void SetGrayscale(GameObject go, bool grayscale)
		{
			if (go) {
				var sps = go.GetComponentsInChildren<UISprite>();
				for (int i = 0; i < sps.Length; ++i) {
					sps[i].grayscale = grayscale;
				}
			}
		}

        /// <summary>
        /// 限制在屏幕内位置
        /// </summary>
        public static Vector2 InsideScreenPosition(this RectTransform self)
        {
            var canvas = self.GetComponentInParent<Canvas>();
            while (!canvas.isRootCanvas) {
                canvas = canvas.GetComponentInParent<Canvas>();
            }
            var parent = self.parent as RectTransform;
            if (canvas && parent) {
                var canvasRect = canvas.GetComponent<RectTransform>();
                var scrSiz = canvasRect.sizeDelta / 2;                
                var bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(canvasRect, self);
                var v2Anchored = self.anchoredPosition;
                
                var v2Max = bounds.max;
                var v2Min = bounds.min;

                if (v2Max.x > scrSiz.x) {
                    v2Anchored.x -= (v2Max.x - scrSiz.x);
                }
                if (v2Max.y > scrSiz.y) {
                    v2Anchored.y -= (v2Max.y - scrSiz.y);
                }
                if (v2Min.x < -scrSiz.x) {
                    v2Anchored.x -= v2Min.x;
                }
                if (v2Min.y < -scrSiz.y) {
                    v2Anchored.y -= v2Min.y;
                }
                
                return v2Anchored;
            }
            return self.anchoredPosition;
        }

        /// <summary>
        /// 计算锚点对齐位置
        /// </summary>
        public static Vector2 AnchorPosition(this Canvas canvas, RectTransform from, Vector2 pivotFrom, RectTransform to, Vector2 pivotTo, Vector2 offset)
        {
            Vector2 v2To = from.anchoredPosition;
            var rect = from.parent as RectTransform;
            var v2Screen = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, to.position);
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, v2Screen, canvas.worldCamera, out v2To)) {
                var pivotOffset = new Vector2(to.pivot.x - pivotTo.x, to.pivot.y - pivotTo.y);
                v2To.x -= to.sizeDelta.x * pivotOffset.x;
                v2To.y -= to.sizeDelta.y * pivotOffset.y;
                v2To.x += offset.x;
                v2To.y += offset.y;

                pivotOffset = new Vector2(from.pivot.x - pivotFrom.x, from.pivot.y - pivotFrom.y);

                v2To.x += from.sizeDelta.x * pivotOffset.x;
                v2To.y += from.sizeDelta.y * pivotOffset.y;                
            }
            return v2To;
        }

        public static CanvasGroup FindCanvasGroup(this UIBehaviour bvr)
        {
            var cv = bvr.GetComponentInParent<CanvasGroup>();
            while (cv != null) {
                if (cv.ignoreParentGroups) break;

                var parent = cv.transform.parent;
                var top = parent ? parent.GetComponentInParent<CanvasGroup>() : null;
                if (top) {
                    cv = top;
                } else break;
            }

            return cv;
        }
	}
}
