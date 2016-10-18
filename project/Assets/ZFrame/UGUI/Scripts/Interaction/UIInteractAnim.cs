using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using DG.Tweening;

namespace ZFrame.UGUI
{
    public abstract class UIInteractAnim : UISelectable
    {
        public float originScale = 1f;
        public float animScale = 1.1f;
        public float duration = 0.2f;
        public Ease easeType = Ease.Linear;
        
        protected override void OnEnable()
        {
            transform.localScale = Vector3.one * originScale;
        }

        protected override void OnDisable()
        {
            this.DOKill();
        }
        
        protected void ExecuteAnim(bool forward)
        {
            float toValue = forward ? animScale : originScale;
            transform.DOScale(toValue, duration).SetEase(easeType).SetUpdate(true).SetId(this);
        }
    }
}
