using System;
using System.Collections.Generic;
using UnityEngine;
using vietlabs;

public static class TextureX {
    private static Dictionary<string, Texture2D> Map;

    public static Texture2D xToTexture2D(this string base64, string id = null) {
        var tex = new Texture2D(16, 16);
        tex.xSetFlag(HideFlags.HideAndDontSave, true);
        tex.LoadImage(Convert.FromBase64String(base64));

        if (string.IsNullOrEmpty(id)) return tex;
        if (Map == null) Map = new Dictionary<string, Texture2D>();
        if (!Map.ContainsKey(id) || Map[id] == null) Map.Add(id, tex);
        else {
            Debug.Log("vlbTexture.ToTexture2D() Error :: id <" + id + "> already exist and will be replaced");
            Map[id] = tex;
        }

        return tex;
    }

    public static bool HasTextureId(string id) { return Map != null && Map.ContainsKey(id) && Map[id] != null; }

    public static Texture2D xGetTextureFromId(this string id) {
        if (string.IsNullOrEmpty(id)) {
            Debug.LogWarning("vlbTexture.GetTextureFromId() Error :: id should not be null or empty");
            return null;
        }
        if (Map == null || !Map.ContainsKey(id)) {
            Debug.LogWarning(
                "vlbTexture.GetTextureFromId() Error :: id <" + id
                + "> not found, consider adding it first by calling base64Source.ToTexture2D(" + id + ")");
            return null;
        }

        if (Map[id] != null) return Map[id];

        Debug.LogWarning(
            "vlbTexture.GetTextureFromId() Error : texture with id <" + id + "> is destroyed, consider adding it again");
        return null;
    }

    public static Texture2D xGetTexture2D(this Color c) {
        var colorKey = c.xToInt().ToString();

        if (Map == null) Map = new Dictionary<string, Texture2D>();
        if (Map.ContainsKey(colorKey) && Map[colorKey] != null) return Map[colorKey];
        var tex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        tex.xSetFlag(HideFlags.HideAndDontSave, true);
        tex.SetPixel(0, 0, c);
        tex.Apply();
        Map.Add(colorKey, tex);
        return tex;
    }

    public static Rect xGetRect(this Texture2D tex) { return new Rect(0, 0, tex.width, tex.height); }
}