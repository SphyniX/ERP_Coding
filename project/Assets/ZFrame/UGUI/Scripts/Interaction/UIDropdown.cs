using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

namespace ZFrame.UGUI
{
    public class UIDropdown : Dropdown
    {
        public static UIDropdown current;

        public UnityAction<UIDropdown> onAction;

        private void doValueChanged(int index)
        {
            current = this;
            if (onAction != null) {
                onAction.Invoke(this);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            onValueChanged.AddListener(doValueChanged);
        }
    }
}
