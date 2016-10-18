using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace ZFrame.UGUI
{
    public abstract class UISelectable : UIBehaviour
    {
        public Selectable selectable;

        public bool IsInteractable() { return !selectable || selectable.IsInteractable(); }

        protected override void Start()
        {
            if (!selectable) selectable = GetComponent<Selectable>();
        }
    }
}
