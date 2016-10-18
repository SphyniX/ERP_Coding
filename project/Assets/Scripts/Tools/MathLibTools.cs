using UnityEngine;
using System.Collections;
using Dest.Math;
using Vectrosity;

public static class MathLibTools {
    public static VectorLine Draw3D(this Box2 self, float y, Color color, float time = 0)
    {
        Vector2[] v2Box = self.CalcVertices();
        Vector3[] v3Point = new Vector3[v2Box.Length + 1];
        for (int i = 0; i < v3Point.Length; ++i) {
			var point = v2Box[i % v2Box.Length];
			v3Point[i] = new Vector3(point.x, y, point.y);
        }
        
		var ret = time == 0 ? VectorLine.SetLine3D(color, v3Point) : VectorLine.SetLine3D(color, time, v3Point);
		ret.name = "BoxLine";
		return ret;
    }

	private const int CIRCLE_POINTS = 32;
	public static VectorLine Draw3D(this Circle2 self, float y, Color color, float time = 0)
	{
		Vector3[] v3Point = new Vector3[CIRCLE_POINTS];
		float unit = Mathf.PI * 2 / (v3Point.Length - 1);
		for (int i = 0; i < v3Point.Length - 1; ++i) {
			var point = self.Eval(i * unit);
			v3Point[i] = new Vector3(point.x, y, point.y);
		}
		v3Point[v3Point.Length - 1] = v3Point[0];

		var ret = time == 0 ? VectorLine.SetLine3D(color, v3Point) : VectorLine.SetLine3D(color, time, v3Point);
		ret.name = "CircleLine";
		return ret;
	}

    public static Vector3[] CalcSectorPoints3D(this Circle2 self, float y, float from, float to, int amountOfPoints)
    {
        Vector3[] v3Point = new Vector3[amountOfPoints];
        float unit = Mathf.Abs(from - to) / (v3Point.Length - 3);
        float PI2 = Mathf.PI * 2;
        for (int i = 0; i < v3Point.Length - 2; ++i) {
            var f = i * unit + from;
            if (f < 0) f += PI2;
            if (f > PI2) f -= PI2;
            var point = self.Eval(f);
            v3Point[i] = new Vector3(point.x, y, point.y);
        }
        v3Point[v3Point.Length - 2] = new Vector3(self.Center.x, y, self.Center.y);
        v3Point[v3Point.Length - 1] = v3Point[0];
        return v3Point;
    }

	public static VectorLine DrawSector3D(this Circle2 self, float y, float from, float to, Color color, float time = 0)
	{
        Vector3[] v3Point = self.CalcSectorPoints3D(y, from, to, CIRCLE_POINTS);
        
		var ret = time == 0 ? VectorLine.SetLine3D(color, v3Point) : VectorLine.SetLine3D(color, time, v3Point);
		ret.name = "SectorLine";
		return ret;
	}
}
