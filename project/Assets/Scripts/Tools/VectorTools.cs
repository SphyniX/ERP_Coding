using UnityEngine;
using System.Collections;

public static class VectorTools {

    public static float HorizontalAngle(this Vector3 self)
    {
        return Mathf.Atan2(self.x, self.z) * Mathf.Rad2Deg;
    }

    public static Vector2 Round(this Vector2 self, float precision)
    {
        return new Vector2(
            Mathf.Round(self.x * precision) / precision,
            Mathf.Round(self.y * precision) / precision);
    }

    public static Vector3 Round(this Vector3 self, float precision)
    {
        return new Vector3(
            Mathf.Round(self.x * precision) / precision,
            Mathf.Round(self.y * precision) / precision, 
            Mathf.Round(self.z * precision) / precision);
    }

    public static bool ScreenRaycast(this Vector3 self, float maxDistance, int layer, out RaycastHit hit)
    {
        var mainCam = Camera.main;
        if (mainCam) {
            Ray ray = Camera.main.ScreenPointToRay(self);
            return Physics.Raycast(ray, out hit, maxDistance, layer);
        } else {
            hit = new RaycastHit();
            return false;
        }
    }

    public static RaycastHit[] ScreenRaycastAll(this Vector3 self, float maxDistance, int layer)
    {
        Ray ray = Camera.main.ScreenPointToRay(self);
        return Physics.RaycastAll(ray, maxDistance, layer);
    }

    public static bool IsNaNOrInfinity(this Vector2 self)
    {
        return float.IsNaN(self.x) || float.IsInfinity(self.x)
            || float.IsNaN(self.y) || float.IsInfinity(self.y);
    }

    public static bool IsNaNOrInfinity(this Vector3 self)
    {
        return float.IsNaN(self.x) || float.IsInfinity(self.x)
            || float.IsNaN(self.y) || float.IsInfinity(self.y)
            || float.IsNaN(self.z) || float.IsInfinity(self.z);
    }

    public static bool IsNaNOrInfinity(this Quaternion self)
    {
        return float.IsNaN(self.x) || float.IsInfinity(self.x)
            || float.IsNaN(self.y) || float.IsInfinity(self.y)
            || float.IsNaN(self.z) || float.IsInfinity(self.z)
            || float.IsNaN(self.w) || float.IsInfinity(self.w);
    }
}
