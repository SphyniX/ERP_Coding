#define USE_DOTween
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ZFrame.Tween
{
    public delegate void CallbackOnUpdate(float t, object target);
    public delegate void CallbackOnComplete(object target);

	public enum UpdateType 
	{
		Normal, Late, Fixed,
	}

	public enum LoopType
	{
		Restart, Yoyo, Incremental
	}

	public enum RotateMode
	{
		Fast, FastBeyond360, WorldAxisAdd, LocalAxisAdd
	}

	public enum Ease
	{
		Unset,
		Linear,
		InSine,
		OutSine,
		InOutSine,
		InQuad,
		OutQuad,
		InOutQuad,
		InCubic,
		OutCubic,
		InOutCubic,
		InQuart,
		OutQuart,
		InOutQuart,
		InQuint,
		OutQuint,
		InOutQuint,
		InExpo,
		OutExpo,
		InOutExpo,
		InCirc,
		OutCirc,
		InOutCirc,
		InElastic,
		OutElastic,
		InOutElastic,
		InBack,
		OutBack,
		InOutBack,
		InBounce,
		OutBounce,
		InOutBounce,
	}

    public partial class ZTweener
    {
    }
}

namespace ZFrame.Tween
{
#if USE_DOTween
    using DG.Tweening;
    using DG.Tweening.Core;

    public partial class ZTweener
    {
        public Tweener tweener;
        public ZTweener(Tweener tw) { tweener = tw; }
    }

    public static partial class ZTween
    {
        #region Tween Config
        public static bool IsTweening(this ZTweener self)
        {
            return self.tweener.IsPlaying();
        }

        public static ZTweener SetTag(this ZTweener self, object tag)
        {
            self.tweener.SetId(tag);
            return self;
        }

        public static ZTweener SetUpdate(this ZTweener self, UpdateType updateType, bool ignoreTimeScale)
        {
            self.tweener.SetUpdate((DG.Tweening.UpdateType)updateType, ignoreTimeScale);
            return self;
        }

        public static ZTweener StartFrom(this ZTweener self, object from)
        {
            self.tweener.ChangeStartValue(from);
            return self;
        }

        public static ZTweener EndAt(this ZTweener self, object at)
        {
            self.tweener.ChangeEndValue(at);
            return self;
        }

        public static ZTweener DelayFor(this ZTweener self, float time)
        {
            self.tweener.SetDelay(time);
            return self;
        }

        public static ZTweener LoopFor(this ZTweener self, int loops, LoopType loopType)
        {
            self.tweener.SetLoops(loops, (DG.Tweening.LoopType)loopType);
            return self;
        }

        public static ZTweener EaseBy(this ZTweener self, Ease ease)
        {
            self.tweener.SetEase((DG.Tweening.Ease)ease);
            return self;
        }

        public static ZTweener UpdateWith(this ZTweener self, CallbackOnUpdate onUpdate)
        {
            if (onUpdate != null) {
                var tweener = self.tweener;
                tweener.OnUpdate(() => {
                    onUpdate.Invoke(0f, tweener.target);
                });
            }
            return self;
        }

        public static ZTweener CompleteWith(this ZTweener self, CallbackOnComplete onComplete)
        {
            if (onComplete != null) {
                var tweener = self.tweener;
                tweener.OnComplete(() => {
                    onComplete.Invoke(tweener.target);
                });
            } else {
                var tweener = self.tweener;
                tweener.OnComplete(null);
            }

            return self;
        }

        public static ZTweener Reset(this ZTweener self)
        {
            self.tweener.Restart();
            return self;
        }

        public static ZTweener Play(this ZTweener self, bool forward)
        {
            if (forward) {
                self.tweener.PlayForward();
            } else {
                self.tweener.PlayBackwards();
            }
            return self;
        }

        public static ZTweener Stop(this ZTweener self, bool complete = false)
        {
            self.tweener.Kill(complete);
            return self;
        }

        public static int Stop(object tarOrTag, bool complete = false)
        {
            return DOTween.Kill(tarOrTag, complete);
        }
        #endregion

        #region Tween Alpha
        public static ZTweener TweenAlpha(this CanvasGroup self, float to, float duration)
        {
            return new ZTweener(self.DOFade(to, duration));
        }

        public static ZTweener TweenAlpha(this CanvasGroup self, float from, float to, float duration)
        {
            self.alpha = from;
            return new ZTweener(self.DOFade(to, duration).ChangeStartValue(from));
        }

        public static ZTweener TweenAlpha(this Graphic self, float to, float duration)
        {
            return new ZTweener(self.DOFade(to, duration));
        }

        public static ZTweener TweenAlpha(this Graphic self, float from, float to, float duration)
        {
            var c = self.color;
            c.a = from;
            self.color = c;
            return new ZTweener(self.DOFade(to, duration).ChangeStartValue(from));
        }
        #endregion

        #region Tween Color
        public static ZTweener TweenColor(this Graphic self, Color to, float duration)
        {
            return new ZTweener(self.DOColor(to, duration));
        }

        public static ZTweener TweenColor(this Graphic self, Color from, Color to, float duration)
        {
            self.color = from;
            return new ZTweener(self.DOColor(to, duration).ChangeStartValue(from));
        }
        #endregion

        #region Tween String
        public static ZTweener TweenFill(this Image self, float to, float duration)
        {
            return new ZTweener(self.DOFillAmount(to, duration));
        }

        public static ZTweener TweenFill(this Image self, float from, float to, float duration)
        {
            self.fillAmount = from;
            return new ZTweener(self.DOFillAmount(to, duration).ChangeStartValue(from));
        }
        #endregion

        #region Tween String
        public static ZTweener TweenString(this Text self, string to, float duration)
        {
            return new ZTweener(self.DOText(to, duration));
        }

        public static ZTweener TweenString(this Text self, string from, string to, float duration)
        {
            self.text = from;
            return new ZTweener(self.DOText(to, duration).ChangeStartValue(from));
        }
        #endregion

        #region Tween Size
        public static ZTweener TweenSize(this RectTransform self, Vector2 to, float duration)
        {
            return new ZTweener(self.DOSizeDelta(to, duration));
        }

        public static ZTweener TweenSize(this RectTransform self, Vector2 from, Vector2 to, float duration)
        {
            self.sizeDelta = from;
            return new ZTweener(self.DOSizeDelta(to, duration).ChangeStartValue(from));
        }
        #endregion

        #region Tween Anchor Position
        public static ZTweener TweenAnchorPos(this RectTransform self, Vector3 to, float durtion)
        {
            return new ZTweener(self.DOAnchorPos3D(to, durtion));
        }

        public static ZTweener TweenAnchorPos(this RectTransform self, Vector3 from, Vector3 to, float durtion)
        {
            self.anchoredPosition3D = from;
            return new ZTweener(self.DOAnchorPos3D(to, durtion).ChangeStartValue(from));
        }
        #endregion

        #region Tween Position
        public static ZTweener TweenPosition(this Transform self, Vector3 to, float durtion)
        {
            return new ZTweener(self.DOMove(to, durtion));
        }

        public static ZTweener TweenPosition(this Transform self, Vector3 from, Vector3 to, float durtion)
        {
            self.position = from;
            return new ZTweener(self.DOMove(to, durtion).ChangeStartValue(from));
        }
        #endregion

        #region Tween LocalPosition
        public static ZTweener TweenLocalPosition(this Transform self, Vector3 to, float durtion)
        {
            return new ZTweener(self.DOLocalMove(to, durtion));
        }

        public static ZTweener TweenLocalPosition(this Transform self, Vector3 from, Vector3 to, float durtion)
        {
            self.localPosition = from;
            return new ZTweener(self.DOLocalMove(to, durtion).ChangeStartValue(from));
        }
        #endregion

        #region Tween Rotation
        public static ZTweener TweenRotation(this Transform self, Vector3 to, float durtion, RotateMode mode = RotateMode.Fast)
        {
            return new ZTweener(self.DORotate(to, durtion, (DG.Tweening.RotateMode)mode));
        }

        public static ZTweener TweenRotation(this Transform self, Vector3 from, Vector3 to, float durtion, RotateMode mode = RotateMode.Fast)
        {
            self.rotation = Quaternion.Euler(from);
            return new ZTweener(self.DORotate(to, durtion, (DG.Tweening.RotateMode)mode).ChangeStartValue(from));
        }
        #endregion

        #region Tween LocalRotation
        public static ZTweener TweenLocalRotation(this Transform self, Vector3 to, float durtion, RotateMode mode = RotateMode.Fast)
        {
            return new ZTweener(self.DOLocalRotate(to, durtion, (DG.Tweening.RotateMode)mode));
        }

        public static ZTweener TweenLocalRotation(this Transform self, Vector3 from, Vector3 to, float durtion, RotateMode mode = RotateMode.Fast)
        {
            self.localRotation = Quaternion.Euler(from);
            return new ZTweener(self.DOLocalRotate(to, durtion, (DG.Tweening.RotateMode)mode).ChangeStartValue(from));
        }
        #endregion

        #region Tween LocalRotation
        public static ZTweener TweenScaling(this Transform self, Vector3 to, float durtion)
        {
            return new ZTweener(self.DOScale(to, durtion));
        }

        public static ZTweener TweenScaling(this Transform self, Vector3 from, Vector3 to, float durtion)
        {
            self.localScale = from;
            return new ZTweener(self.DOScale(to, durtion).ChangeStartValue(from));
        }
        #endregion

        #region Shake
        public static ZTweener ShakePosition(this Camera self, float duration, float strength = 3f, int vibrato = 10)
        {
            return new ZTweener(self.DOShakePosition(duration, strength, vibrato));
        }

        #endregion

        #region Tween T
        public static ZTweener Tween(DOGetter<float> getter, DOSetter<float> setter, float to, float duration)
        {
            return new ZTweener(DOTween.To(getter, setter, to, duration));
        }

        public static ZTweener Tween(DOGetter<Vector2> getter, DOSetter<Vector2> setter, Vector2 to, float duration)
        {
            return new ZTweener(DOTween.To(getter, setter, to, duration));
        }

        public static ZTweener Tween(DOGetter<Color> getter, DOSetter<Color> setter, Color to, float duration)
        {
            return new ZTweener(DOTween.To(getter, setter, to, duration));
        }

        #endregion

        #region YieldInstruction
        public static YieldInstruction WaitForCompletion(this ZTweener self)
        {
            return self.tweener.WaitForCompletion();
        }
        #endregion
    }
#endif
}
