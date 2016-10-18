using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using ZFrame.Tween;

public class ThreeDButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private List<Material> m_Mats = new List<Material>();
    private List<Color> m_Colors = new List<Color>();
    private Color m_TempColor = Color.white;

    [SerializeField]
    private Color m_MultiColor = Color.white;
    public Color color {
        get { return m_MultiColor; }
        set { m_MultiColor = value; SetColor(value); }
    }    
    public UnityAction<GameObject> onClick;

    // Use this for initialization
    private void Start () 
    {
#if UNITY_EDITOR
        if (!Application.isPlaying) return;
#endif
        var rdrs = GetComponentsInChildren<Renderer>();
        if (rdrs != null && rdrs.Length > 0) {
            for (int i = 0; i < rdrs.Length; ++i) {
                var rdr = rdrs[i];
                if (rdr) {
                    var mats = rdr.materials;
                    for (int j = 0; j < mats.Length; ++j) {
                        if (mats[j]) {
                            m_Mats.Add(mats[j]);
                            m_Colors.Add(mats[j].color);
                        }
                    }
                }
            }
        }

        SetColor(m_MultiColor);
    }
    
    [NoToLua]
    public void OnPointerClick(PointerEventData eventData)
    {
        //ZTween.Tween(GetColor, SetColor, Color.white, 0.2f);
        if (onClick != null) onClick.Invoke(gameObject);
    }

    [NoToLua]
    public void OnPointerDown(PointerEventData eventData)
    {
        TweenColor(Color.gray);
    }

    [NoToLua]
    public void OnPointerUp(PointerEventData eventData)
    {
        TweenColor(m_MultiColor);
    }

    public void TweenColor(Color to)
    {
        ZTween.Stop(this);
        ZTween.Tween(GetColor, SetColor, to, 0.2f).SetTag(this);
    }
    
    private Color GetColor()
    {
        return m_TempColor;
    }

    public void SetColor(Color color)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying) return;
#endif
        m_TempColor = color;
        for (int i = 0; i < m_Mats.Count; ++i) {
            m_Mats[i].color = m_Colors[i] * color;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        SetColor(m_MultiColor);
    }

#endif
}
