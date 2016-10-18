using UnityEngine;
using System.Collections;

namespace ZFrame.UGUI
{
	using Tween;

    public class UILabel : UnityEngine.UI.Text, ITweenable
    {
        [NoToLuaAttribute]
        public static Localization LOC;

        public bool localize;
        public override string text
        {
            get
            {
                //if (localize && LOC) {
                //    var txt = LOC.Get(m_Text);
                //    if (txt == null) {
                //        if (Application.isPlaying) {
                //            LogMgr.W("本地化获取失败：Lang = {0}, Key = {1} @ {2}", 
                //                LOC.currentLang, m_Text, rectTransform.GetHierarchy(null));
                //        }
                //        return m_Text;
                //    }
                //    return txt;
                //} else {
                //    return m_Text;
                //}

                return m_Text;
            }
            set { base.text = value; SetLayoutDirty(); }
        }

        public string rawText { get { return m_Text; } }
        
        public string textFormat = "{0}";
        public void SetFormatArgs(params object[] Args)
        {
            text = string.Format(textFormat, Args);
        }
        public void UpdateNumber(float value)
        {
            SetFormatArgs(value);
        }        

        public ZTweener Tween(object from, object to, float duration)
        {
            ZTweener tw = null;
            if (to is string) {
				tw = this.TweenString((string)to, duration);
                if (from != null) {
                    text = (string)from;
					tw.StartFrom(text);
                }
            }
            if (to is Color) {
                tw = this.TweenColor((Color)to, duration);
                if (from is Color) {
                    color = (Color)from;
					tw.StartFrom(color);
                }
            }
            if (to is float) {
                tw = this.TweenAlpha((float)to, duration);
                if (from is float) {
                    var a = (float)from;
                    color = new Color(color.r, color.g, color.b, a);
					tw.StartFrom(a);
                }
            }
            if (tw != null) tw.SetTag(this);
            return tw;
        }

		protected override void Start ()
		{
#if UNITY_EDITOR
			UGUITools.AutoUIRoot(this);
#endif
			base.Start ();
		}

        protected override void OnEnable()
        {
            base.OnEnable();            
        }

    }
}
