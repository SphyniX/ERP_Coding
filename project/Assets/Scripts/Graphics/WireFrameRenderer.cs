using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class WireFrameRenderer : MonoBehaviour
{
    //****************************************************************************************
    //  Material options
    //****************************************************************************************
    public Color LineColor = Color.green;
    public bool ZWrite = true;
    public bool AWrite = true;
    public bool Blend = true;
    public int Fidelity = 3;

    //****************************************************************************************
    // Line Data
    //****************************************************************************************
    private List<Line> m_Lines = new List<Line>();
    private Material m_LineMat;

    private Vector3[] m_Vertices;
    private int[] m_Triangles;

    //*****************************************************************************************
    // Helper class, Line is defined as two Points
    //*****************************************************************************************
    private class Line 
    {
        public Vector3 PointA;
        public Vector3 PointB;

        public Line(Vector3 a, Vector3 b)
        {
            PointA = a;
            PointB = b;
        }

        private int m_0, m_1;
        public void GetPoint(Vector3[] vertices, int[] triangles, out Vector3 A, out Vector3 B)
        {            
            A = vertices[triangles[m_0]];
            B = vertices[triangles[m_1]];
        }

        public Line(int indexA, int indexB)
        {
            m_0 = indexA;
            m_1 = indexB;
        }

        //*****************************************************************************************
        // A == B if   Aa&Ab == Ba&Bb or Ab&Ba == Aa  Bb
        //*****************************************************************************************
        public static bool operator ==(Line lA, Line lB)
        {
            //if (lA.PointA == lB.PointA && lA.PointB == lB.PointB )
            //{
            //    return true;
            //}

            //if (lA.PointA == lB.PointB && lA.PointB == lB.PointA )
            //{
            //    return true;
            //}

            if (lA.m_0 == lB.m_0 && lA.m_1 == lB.m_1)
            {
                return true;
            }

            if (lA.m_0 == lB.m_1 && lA.m_1 == lB.m_0)
            {
                return true;
            }

            return false;
        }

        //*****************************************************************************************
        // A != B if   !(Aa&Ab == Ba&Bb or Ab&Ba == Aa  Bb)
        //*****************************************************************************************
        public static bool operator !=(Line lA, Line lB)
        {
            return !(lA == lB);
        }

        public override bool Equals(object obj)
        {
            var line = obj as Line;
            return line != null ? this == line : base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    private Renderer m_Renderer;

    private void Awake()
    {
        m_Renderer = GetComponent<Renderer>();
        m_LineMat = new Material(Shader.Find("Lines/Colored Blended"));
        m_LineMat.hideFlags = HideFlags.HideAndDontSave;
        m_LineMat.shader.hideFlags = HideFlags.HideAndDontSave;
    }

    private void OnDestroy()
    {
        Destroy(m_LineMat);
    }

    //*****************************************************************************************
    // Parse the mesh this is attached to and save the line data
    //*****************************************************************************************
    private void OnEnable()
    {   
        Mesh mesh = null;
        var skin = m_Renderer as SkinnedMeshRenderer;
        if (skin) {
            mesh = skin.sharedMesh;
        } else {
            var filter = GetComponent<MeshFilter>();
            if (filter) mesh = filter.sharedMesh;
        }

        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        m_Vertices = vertices;
        m_Triangles = triangles;

        for (int i = 0; i < triangles.Length / 3; i++) {
            int j = i * 3;
            //Line lineA = new Line(vertices[triangles[j]], vertices[triangles[j + 1]]);
            //Line lineB = new Line(vertices[triangles[j + 1]], vertices[triangles[j + 2]]);
            //Line lineC = new Line(vertices[triangles[j + 2]], vertices[triangles[j]]);
            var lineA = new Line(j, j + 1);
            var lineB = new Line(j + 1, j + 2);
            var lineC = new Line(j + 2, j);

            if (Fidelity == 3) {
                AddLine(lineA);
                AddLine(lineB);
                AddLine(lineC);
            } else if (Fidelity == 2) {
                AddLine(lineA);
                AddLine(lineB);
            } else if (Fidelity == 1) {
                AddLine(lineA);
            }
        }
    }

    //****************************************************************************************
    // Adds a line to the array if the equivalent line isn't stored already
    //****************************************************************************************
    private void AddLine(Line l)
    {
        bool found = false;
        for (int i = 0; i < m_Lines.Count; ++i) {
            var line = m_Lines[i];
            if (l == line) { found = true; break; }
        }

        if (!found) { m_Lines.Add(l); }
    }

    //****************************************************************************************
    // Deferred rendering of wireframe, this should let materials go first
    //****************************************************************************************
    private void OnRenderObject()
    {
        m_LineMat.SetPass(0);

        GL.PushMatrix();
        GL.MultMatrix(transform.localToWorldMatrix);
        GL.Begin(GL.LINES);
        GL.Color(LineColor);

        for (int i = 0; i < m_Lines.Count; ++i) {
            var line = m_Lines[i];
            Vector3 A, B;
            line.GetPoint(m_Vertices, m_Triangles, out A, out B);
            GL.Vertex(A);
            GL.Vertex(B);
        }

        GL.End();
        GL.PopMatrix();
    }
}