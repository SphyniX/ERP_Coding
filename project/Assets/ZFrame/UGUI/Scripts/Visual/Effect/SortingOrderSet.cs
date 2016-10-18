using UnityEngine;
using System.Collections;

namespace ZFrame.UGUI 
{
    public class SortingOrderSet : MonoBehaviour 
    {
        [SerializeField]
        private string m_Layer = "Default";
        [SerializeField]
        private int m_Order;

    	// Use this for initialization
    	private void Start () 
        {
            var canvas = GetComponent<Canvas>();
            if (canvas) {
                canvas.overrideSorting = true;
                canvas.sortingOrder = m_Order;
            } else {
                Renderer[] rdrs = GetComponentsInChildren<Renderer>();
                for (int i = 0; i < rdrs.Length; ++i) {
                    rdrs[i].sortingLayerName = m_Layer;
                    rdrs[i].sortingOrder = m_Order;
                }
            }
    	}
    }
}
