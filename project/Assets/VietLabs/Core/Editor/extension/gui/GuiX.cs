using System;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using vietlabs;

public static class GuiX {
    public static VtHzGL hzLayout {
        get { return new VtHzGL(true); }
    }

    public static VtHzGL vtLayout {
        get { return new VtHzGL(false); }
    }

    public static VtHzGL HzLayout2(params GUILayoutOption[] options) { return new VtHzGL(true, options); }
    public static VtHzGL VtLayout2(params GUILayoutOption[] options) { return new VtHzGL(true, options); }

    public static VtHzGL HzLayout2(GUIContent content, GUIStyle style, params GUILayoutOption[] options) {
        return new VtHzGL(true, content, style, options);
    }

    public static VtHzGL VtLayout2(GUIContent content, GUIStyle style, params GUILayoutOption[] options) {
        return new VtHzGL(true, content, style, options);
    }

    public static VtHzGL HzLayout2(Texture image, GUIStyle style, params GUILayoutOption[] options) {
        return new VtHzGL(true, image, style, options);
    }

    public static VtHzGL VtLayout2(Texture image, GUIStyle style, params GUILayoutOption[] options) {
        return new VtHzGL(true, image, style, options);
    }

    public static VtHzGL HzLayout2(string text, GUIStyle style, params GUILayoutOption[] options) {
        return new VtHzGL(true, text, style, options);
    }

    public static VtHzGL VtLayout2(string text, GUIStyle style, params GUILayoutOption[] options) {
        return new VtHzGL(true, text, style, options);
    }

    public static VtHzGL HzLayout2(GUIStyle style, params GUILayoutOption[] options) {
        return new VtHzGL(true, style, options);
    }

    public static VtHzGL VtLayout2(GUIStyle style, params GUILayoutOption[] options) {
        return new VtHzGL(true, style, options);
    }

    public static ScrollGL xScrollView(this Rect clipRect, ref Rect contentRect, ref Vector2 scrollPosition) {
        return new ScrollGL(clipRect, ref contentRect, ref scrollPosition);
    }

    public static ColorG GUIColor(Color c) { return new ColorG(c, 0); }
    public static ColorG GUIContentColor(Color c) { return new ColorG(c, 1); }
    public static ColorG GUIBgColor(Color c) { return new ColorG(c, 2); }

    public static DisableG DisableGroup(bool disable) { return new DisableG(disable); }

    public static bool xIsLayout(this Event evt) { return evt.type == EventType.layout; }
    public static bool xIsNotLayout(this Event evt) { return evt.type != EventType.layout; }
    public static bool xIsNotUsed(this Event evt) { return evt.type != EventType.used; }

    public static bool xIconSelector(this Rect r, ref int selected, params Texture2D[] textureList) {
        var n = textureList.Length;
        var rectList = r.w(16 * n).h(16f).xHzSubRectsLeftDivide(n);
        var changed = false;

        GUI.DrawTexture(r.xExpand(4f), Color.black.xGetTexture2D());
        
        for (var i = 0; i < textureList.Length; i++) {
            var rr = rectList[i];
            if (i == selected) GUI.DrawTexture(rr, Color.blue.xGetTexture2D());
            GUI.DrawTexture(rr, textureList[i]);

            if ((i != selected) && rr.xLMB_isDown().noModifier) {
                selected = i;
                changed = true;
            }
        }

        return changed;
    }

    public static void xDrawTextureColor(this Rect r, Texture2D tex, Color c) {
        var oColor = GUI.color;
        GUI.color = c;
        GUI.DrawTexture(r, tex);
        GUI.color = oColor;

        if (r.xLMB_isDown().noModifier) {
            EditorGUIUtility.DrawColorSwatch(r.dy(20f).w(200), c);
        }
    }

    public static void xDrawTitleBar(string title, float barDx = 0f) {
        var r = GUILayoutUtility.GetRect(0, Screen.width, 20f, 20f);
        var c = new ColorHSL(0f, 0f, 0.5f).xProSkinAdjust();

        GUI.DrawTexture(r.dx(barDx), c.xGetTexture2D());
        GUI.Label(r.dy(2f), title, EditorStyles.boldLabel);
        //temp = EditorGUILayout.Slider(temp, -1f, 1f);
    }

    public static void xDrawTitleBar(string title, ref bool enable, float barDx = 0f, float toggleDx = 0f) {
        var r = GUILayoutUtility.GetRect(0, Screen.width, 20f, 20f);
        var c = new ColorHSL(0f, 0f, 0.5f).xProSkinAdjust();

        GUI.DrawTexture(r.dx(barDx), c.xGetTexture2D());

        var r2 = r.dy(2f);
        GUI.Label(r2, title, EditorStyles.boldLabel);
        enable = GUI.Toggle(r2.xSubRectRight(20f).dx(toggleDx), enable, "");
    }


    public static void DrawResource(ref string[] icons, ref Color[] colors) {
        var rr = GUILayoutUtility.GetRect(80, 20f).wh(80f, 20f).xHzSplitByWeight(1, 1, 1, 1);
        Color light = new Color32(192,192,192,255);
        Color dark = new Color32(49,49,49,255);

        GUI.DrawTexture(rr[0].w(40f), dark.xGetTexture2D());
        GUI.DrawTexture(rr[0].dx(40f).w(40f), light.xGetTexture2D());

        var oColor = GUI.color;
        for (var i = 0; i < icons.Length; i++) {
            GUI.color = colors[i];
            GUI.DrawTexture(rr[i].wh(16f, 16f), EditorResource.GetTexture2D(icons[i]));
        }


        GUI.color = oColor;
    }

    public static bool GLToggle(bool value, string label, float w) {
        EditorGUI.BeginChangeCheck();
        var v = EditorGUILayout.ToggleLeft(label, value, GUILayout.Width(w));
        return EditorGUI.EndChangeCheck() ? v : value;
    }

    public static bool xMiniTag(this Rect r, string lb, Color c, bool autoSize = true, float lbAlign = 0.5f) {
        var style = EditorStyles.miniLabel;
        var lbRect = style.CalcSize(new GUIContent(lb));

        var rr = r.wh(autoSize ? lbRect.x : r.width, 15f);
        GUI.DrawTexture(rr, c.xGetTexture2D());
        var offsetX = Mathf.Max(0, rr.width - lbRect.x) * lbAlign;
        GUI.Label(rr.dx(offsetX).dy(-1f), lb, EditorStyles.miniLabel);

        var isClicked = rr.xLMB_isDown().noModifier;
        return isClicked;
    }

    public static bool xMiniButton(this Rect r, string lb, bool autoSize = true, float lbAlign = 0.5f, bool drawButton = true) {
        //lb = "999+";
        var style = EditorStyles.miniLabel;
        var lbRect = style.CalcSize(new GUIContent(lb));
        var rr = r.wh( (autoSize ? lbRect.x : r.width), 14f);

        lbRect = EditorStyles.label.CalcSize(new GUIContent(lb));
        var isClicked = drawButton && GUI.Button(rr, "", EditorStyles.miniButton);
        GUI.Label(rr.dx((rr.width - lbRect.x) * lbAlign).dy(-1f), lb, drawButton ? EditorStyles.miniLabel : EditorStyles.label);

        return isClicked;
    }



    /*private static Color? bgColor;
    private static Color? cColor;
    public static void SaveBGColor(Color c) {
        if (bgColor != null) {
            Debug.LogWarning("Multiple level of SaveBG Color not yet support");
            return;
        }
        bgColor = GUI.backgroundColor;
        GUI.backgroundColor = c;
    }
    public static void RestoreBGColor() {
        if (bgColor == null) {
            Debug.LogWarning("Try to SaveBG Color before restore");
            return;
        }
        GUI.backgroundColor = bgColor.Value;
        bgColor = null;
    }

    public static void SaveContentColor(Color c)
    {
        if (cColor != null)
        {
            Debug.LogWarning("Multiple level of SaveBG Color not yet support");
            return;
        }
        cColor = GUI.color;
        GUI.color = c;
    }
    public static void RestoreContentColor()
    {
        if (cColor == null)
        {
            Debug.LogWarning("Try to SaveBG Color before restore");
            return;
        }
        GUI.color = cColor.Value;
        cColor = null;
    }*/

}

public struct ColorG : IDisposable {
    private readonly Color c;
    private readonly int t;

    public ColorG(Color color, int type) {
        t = type;
        switch (type) {
            case 0:
                c = GUI.color;
                GUI.color = color;
            break;
            case 1:
                c = GUI.contentColor;
                GUI.contentColor = color;
            break;
            default:
                c = GUI.backgroundColor;
                GUI.backgroundColor = color;
            break;
        }
    }

    public void Dispose() {
        switch (t) {
            case 0:
                GUI.color = c;
                break;
            case 1:
                GUI.contentColor = c;
                break;
            default:
                GUI.backgroundColor = c;
                break;
        }
    }
}

public struct DisableG : IDisposable {
    public DisableG(bool isDisable) {
        EditorGUI.BeginDisabledGroup(isDisable);
    }

    public void Dispose() {
        EditorGUI.EndDisabledGroup();
    }
}

public struct VtHzGL : IDisposable /* Using struct to prevent gc */ {
    private readonly bool m_isHorz;

    public VtHzGL(bool hz, params GUILayoutOption[] options) {
        m_isHorz = hz;
        if (m_isHorz) GUILayout.BeginHorizontal(options);
        else GUILayout.BeginVertical(options);
    }

    public VtHzGL(bool hz, GUIContent content, GUIStyle style, params GUILayoutOption[] options) {
        m_isHorz = hz;
        if (m_isHorz) GUILayout.BeginHorizontal(content, style, options);
        else GUILayout.BeginVertical(content, style, options);
    }

    public VtHzGL(bool hz, Texture image, GUIStyle style, params GUILayoutOption[] options) {
        m_isHorz = hz;
        if (m_isHorz) GUILayout.BeginHorizontal(image, style, options);
        else GUILayout.BeginVertical(image, style, options);
    }

    public VtHzGL(bool hz, string text, GUIStyle style, params GUILayoutOption[] options) {
        m_isHorz = hz;
        if (m_isHorz) GUILayout.BeginHorizontal(text, style, options);
        else GUILayout.BeginVertical(text, style, options);
    }

    public VtHzGL(bool hz, GUIStyle style, params GUILayoutOption[] options) {
        m_isHorz = hz;
        if (m_isHorz) GUILayout.BeginHorizontal(style, options);
        else GUILayout.BeginVertical(style, options);
    }

    public void Dispose() {
        if (m_isHorz) GUILayout.EndHorizontal();
        else GUILayout.EndVertical();
    }
}

public struct ScrollGL : IDisposable {
    private readonly bool m_scroll;

    public ScrollGL(Rect clipRect, ref Rect contentRect, ref Vector2 scrollPosition) {
        var horz = contentRect.width > clipRect.width;
        var vert = contentRect.height > clipRect.height;
        m_scroll = horz || vert;

        if (!m_scroll) return;

        if (vert) contentRect.width -= 18;
        if (horz) contentRect.height -= 18;
        scrollPosition = GUI.BeginScrollView(clipRect, scrollPosition, contentRect, horz, vert);
    }

    public void Dispose() { if (m_scroll) GUI.EndScrollView(); }
}