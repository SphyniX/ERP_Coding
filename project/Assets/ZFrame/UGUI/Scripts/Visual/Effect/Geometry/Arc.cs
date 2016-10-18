using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace ZFrame.UGUI
{    
    /// <summary>
    /// 用一张Simple类型的Image画个弧形
    /// </summary>
    public class Arc : Figure
    {
        /// <summary>
        ///  1      2|3
        /// 
        ///  0|5    4
        /// </summary>

        public float radius = 50;
        [Range(0, 360)]
        public float angle = 360;
		public float width = 1;
        public int segment = 10;

        private float m_Tan;
        private int oddSegment { get { return segment * 2 + 1; } }

        private void CopyVertexes(List<UIVertex> sourceList, List<UIVertex> destnationList, float offset)
        {
            for (int i = 0; i < sourceList.Count; ++i) {
                var vert = sourceList[i];
                var pos = Quaternion.Euler(0, 0, offset) * vert.position;
                vert.position = pos;
                destnationList.Add(vert);
            }
        }

        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive()) return;
            if (segment < 1) return;
            
            var verts = ListPool<UIVertex>.Get();
            vh.GetUIVertexStream(verts);

            if (verts.Count == 6) {
				// 起始点(angle = 0)
                var rot = Quaternion.Euler(0, 0, 90);                
                var points = new Vector3[6] {
                    verts[0].position,
                    verts[1].position,
                    verts[2].position,
                    verts[3].position,
                    verts[4].position,
                    verts[5].position,
                };
                float yOff = radius - width / 2;
                for (int i = 0; i < points.Length; ++i) {                    
                    points[i].y -= yOff;
                }

                var xOff = width * m_Tan;
                if (points[1].y > points[0].y) {
                    points[1].x += xOff;
                    points[2].x -= xOff;
                    points[3] = points[2];
                } else {
                    points[4].x -= xOff;
                    points[0].x += xOff;                    
                    points[5] = points[0];
                }

                for (int i = 0; i < verts.Count; ++i) {
                    var vert = verts[i];
                    vert.position = rot * points[i];
                    verts[i] = vert;
                }

                var newVerts = ListPool<UIVertex>.Get();                
                
                var offset = angle / oddSegment;
                for (int i = -segment; i <= segment; ++i) {
                    CopyVertexes(verts, newVerts, i * offset);
                }

                vh.Clear();
                vh.AddUIVertexTriangleStream(newVerts);

                ListPool<UIVertex>.Release(newVerts);
            }
            ListPool<UIVertex>.Release(verts);
        }
        
        protected override void UpdateImage()
        {
            if (!IsActive()) return;
            if (segment < 1) return;

            var rect = GetComponent<RectTransform>();
            rect.pivot = new Vector2(0.5f, 0.5f);
            m_Tan = Mathf.Tan(Mathf.PI * (angle / 360) / oddSegment);
            var sideLen = m_Tan * radius * 2;
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sideLen);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, width);

            var img = GetComponent<Image>();
            img.type = Image.Type.Simple;
        }
    }
}
