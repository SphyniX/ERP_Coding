using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

namespace ZFrame.UGUI
{
    public class ObjectVisible : UIBehaviour
    {
        public GameObject target;
        private int m_Layer;

        public void SetUITarget(GameObject target)
        {
            this.target = target;
            m_Layer = target.layer;
        }

        protected override void Start()
        {
            OnCanvasGroupChanged();
        }

        protected override void OnEnable()
        {
            if (target) {
                m_Layer = target.layer;
            }
        }

        protected override void OnCanvasGroupChanged()
        {
            if (target && target.activeInHierarchy) {
                var cv = this.FindCanvasGroup();
                if (cv) {
                    if (cv.alpha == 1) {
                        target.SetLayerRecursively(m_Layer);
                    } else if (target.layer == m_Layer) {
                        target.SetLayerRecursively(LAYERS.iInvisible);
                    }
                }
            }
        }
    }
}
