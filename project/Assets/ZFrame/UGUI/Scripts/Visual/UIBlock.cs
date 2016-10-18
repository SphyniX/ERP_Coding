using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

namespace ZFrame.UGUI
{
    /// <summary>
    /// 不可见的UI控件，仅仅用来阻挡UI射线
    /// </summary>
    public class UIBlock : Graphic, ICanvasRaycastFilter
    {
        public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
        {
            return true;
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            // It's invisible...
        }

        public override Texture mainTexture { get { return null; } }

        public override Material materialForRendering { get { return null; } }
    }
}
