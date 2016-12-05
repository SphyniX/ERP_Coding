using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace ZFrame.UGUI
{
    public class UIInput : UnityEngine.UI.InputField
    {
        public static UIInput current;

        // 事件回调
        public UnityAction<UIInput> onSubmit;

        private void DoSumbit(string text)
        {
            current = this;
            if (onSubmit != null) onSubmit.Invoke(this);
        }

        protected override void Awake()
        {
            base.Awake();
            onEndEdit.AddListener(DoSumbit);
        }

        public void SetInteractable(bool interactable)
        {
            this.interactable = interactable;
            UGUITools.SetGrayscale(gameObject, !interactable);
        }
    }
}
