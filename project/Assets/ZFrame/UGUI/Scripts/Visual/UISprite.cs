using UnityEngine;
using System.Collections;

namespace ZFrame.UGUI
{
    using Asset;
	using Tween;

    public class UISprite : UnityEngine.UI.Image, ITweenable
    {
        /// <summary>
        /// 从指定的AssetBundle中加载Sprite
        /// </summary>
        public string spritePath
        {
            set
            { 
				if (string.IsNullOrEmpty(value) || value.EndsWith("/")) {
					sprite = null;
				} else {
					sprite = AssetsMgr.A.Load<Sprite>(value, false);
					if (sprite == null) {
                        LogMgr.W("Load <Sprite> Fail! path = {0}", value);
					}
				}
				enabled = sprite;
            }
        }

        /// <summary>
        /// 从UI/Atlas@中根据其名字加载Sprite
        /// </summary>
        public string spriteName
        {
            set
            {
				if (string.IsNullOrEmpty(value) || value.EndsWith("/")) {
                    sprite = null;
                } else {
                    string[] splits = value.Split('/');
                    var prefab = AssetsMgr.A.Load<GameObject>("UI/Atlas@" + splits[0]);
                    if (prefab) {
                        var objLib = prefab.GetComponent<ObjectLibrary>();
                        sprite = objLib.Get<Sprite>(splits[1]);
                        if (sprite == null) {
                            LogMgr.W("Load <Sprite> Fail! path = {0}", value);
                        }
                    }					
                }
                enabled = sprite;
            }
        }

		public bool grayscale
		{
            get { return material == UGUITools.grayScaleMat; }
			set { material = value ? UGUITools.grayScaleMat : null; }            
		}

        private void OnSpriteLoaded(Object o, object param)
        {
            sprite = o as Sprite;
            enabled = sprite != null;
        }

        public void Load(string path, DelegateObjectLoaded onLoaded = null, object param = null)
        {
            var loadedCfg = OnSpriteLoaded + onLoaded;
            if (string.IsNullOrEmpty(path) || path.EndsWith("/")) {
                loadedCfg.Invoke(null, param);
            } else {
                AssetsMgr.A.LoadAsync(typeof(Sprite), path, true, loadedCfg, param);
            }
        }

        public override void SetNativeSize()
        {
            if (sprite) {
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sprite.rect.width);
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sprite.rect.height);
            }
        }

        public ZTweener Tween(object from, object to, float duration)
        {
            ZTweener tw = null;
            if (to is Color) {
                tw = this.TweenColor((Color)to, duration);
                if (from is Color) {
                    color = (Color)from;
					tw.StartFrom(color);
                }
            } else if (to is float) {
				tw = this.TweenFill((float)to, duration);
                if (from is float) {
					fillAmount = (float)from;
					tw.StartFrom(fillAmount);
                }
            }
            if (tw != null) tw.SetTag(this);
            return tw;
        }

        #region 不规则热点
        private PolygonCollider2D m_Polygon;

        public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
        {
            var valid = base.IsRaycastLocationValid(screenPoint, eventCamera);
            if (valid && m_Polygon != null) {
                Vector3 spV3 = new Vector3(screenPoint.x, screenPoint.y, rectTransform.position.z - eventCamera.transform.position.z);
                Vector2 point = eventCamera.ScreenToWorldPoint(spV3);
                valid = m_Polygon.OverlapPoint(point);
            }
            return valid;
        }
        #endregion

        protected override void Start ()
		{
#if UNITY_EDITOR
			UGUITools.AutoUIRoot(this);
#endif
			base.Start ();
            enabled = sprite;
            m_Polygon = GetComponent<PolygonCollider2D>();
		}
    }
}
