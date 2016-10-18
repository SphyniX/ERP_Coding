using UnityEngine;
using System.Collections;

public static class ColorTools 
{
    public static Color ToColor(this int colorValue)
    {
        var a = (colorValue >> 24);
        var r = (colorValue >> 16) & 255;
        var g = (colorValue >> 8) & 255;
        var b = colorValue & 255;
        return new Color32((byte)r, (byte)g, (byte)b, (byte)a);
    }
}
