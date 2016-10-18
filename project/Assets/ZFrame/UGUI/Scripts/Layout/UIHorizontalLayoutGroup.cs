using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

namespace ZFrame.UGUI
{
    public class UIHorizontalLayoutGroup : UIHorizontalOrVerticalLayoutGroup
    {
        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();
            CalcAlongAxis(0, false);
        }

        public override void CalculateLayoutInputVertical()
        {
            CalcAlongAxis(1, false);
        }

        public override void SetLayoutHorizontal()
        {
            SetChildrenAlongAxisEx(0, false);
        }

        public override void SetLayoutVertical()
        {
            SetChildrenAlongAxisEx(1, false);
        }
    }
}
