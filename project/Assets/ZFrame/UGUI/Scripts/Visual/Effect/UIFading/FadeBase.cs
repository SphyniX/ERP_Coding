using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;

namespace ZFrame.UGUI
{
	using Tween;

	public enum FadeGroup { In, Out, }
	
	public enum AutoFade
	{
		OnEnable = 1 << 0,
		PointerEnter = 1 << 1,
		PointerExit = 1 << 2,
		PointerDown = 1 << 3,
		PointerUp = 1 << 4,
		PointerClick = 1 << 5,
		Drop = 1 << 6,
		UpdateSelected = 1 << 7,
		Select = 1 << 8,
		Deselect = 1 << 9,
		InitializePotentialDrag = 1 << 10,
		BeginDrag = 1 << 11,
		EndDrag = 1 << 12,
		Submit = 1 << 13,
		Cancle = 1 << 14,
	}

    [ExecuteInEditMode]
    public abstract class FadeBase : UISelectable
    {
        [SerializeField, HideInInspector]
        private FadeGroup m_Group;
        [SerializeField, HideInInspector]
        private GameObject m_Target;
        [SerializeField, HideInInspector, FormerlySerializedAs("easeType")]
        protected Ease m_Ease = Ease.Linear;
        [SerializeField, HideInInspector]
        private float m_Duration;
        [SerializeField, HideInInspector]
        private float m_Delay;
        [SerializeField, HideInInspector]
        private int m_Loops;
        [SerializeField, HideInInspector]
        private bool m_IgnoreTimescale;
        [SerializeField, HideInInspector]
        public LoopType loopType;
        [SerializeField, HideInInspector]
        public int autoFade = 0;

		public FadeGroup fadeGroup { get { return m_Group; } }
		public GameObject target { get { if (!m_Target) m_Target = gameObject; return m_Target; } }
		public float duration { get { return m_Duration; } }
		public Ease easeType { get { return m_Ease; } }
		public float delay { get { return m_Delay; } }
		public int loops { get { return m_Loops; } }

		public abstract object source { get ; }
		public abstract object destina { get ; }

		protected abstract void Restart();
		protected abstract ZTweener AnimateFade(bool forward);
		
		protected ZTweener m_Tweener;
		public ZTweener tweener { get { return m_Tweener; } }
        
		public bool DOFade(bool reset, bool forward = true)
		{
            OnDisable();
            m_Tweener = AnimateFade(forward);

            if (m_Tweener != null) {
				m_Tweener.SetTag(gameObject).SetUpdate(UpdateType.Normal, m_IgnoreTimescale);
				if (loops != 0) {
					m_Tweener.LoopFor(loops, loopType);
				}
				if (reset) {
                    if (delay == 0) {
                        this.Restart();
                        m_Tweener.StartFrom(forward ? source : destina);
                    }
                }
			}
			return m_Tweener != null;
		}

        public void Play(bool forward)
        {
            DOFade(true, forward);
        }

        public abstract void Apply();

		protected override void OnEnable()
		{
#if UNITY_EDITOR
            if (!Application.isPlaying) return;
#endif
            if (ChkAutoFade(AutoFade.OnEnable)) {
                DOFade(true);
            } else {
                if (delay == 0) {
                    Restart();
                }
            }
		}
		
		protected override void OnDisable()
		{
			if (m_Tweener != null && m_Tweener.IsTweening()) {
				m_Tweener.CompleteWith(null);
				m_Tweener.Stop(true);
			}
		}

        protected bool ChkAutoFade(AutoFade fade)
        {
            var fit = (autoFade & (int)fade) != 0;
            return fit;
        }
    }
}
