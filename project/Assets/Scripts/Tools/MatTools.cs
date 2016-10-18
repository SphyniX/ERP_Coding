using UnityEngine;
using System.Collections;

public static class MatTools {
    public static void CopyColor(this Material self, string propertyName, Material tarMat)
    {
        if (tarMat) {
            self.SetColor(propertyName, tarMat.GetColor(propertyName));
        }
    }
    public static void CopyInt(this Material self, string propertyName, Material tarMat)
    {
        if (tarMat) {
            self.SetInt(propertyName, tarMat.GetInt(propertyName));
        }
    }
    public static void CopyFloat(this Material self, string propertyName, Material tarMat)
    {
        if (tarMat) {
            self.SetFloat(propertyName, tarMat.GetFloat(propertyName));
        }
    }
}
