namespace MetaTools
{
#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CircleObject : ScriptableObject
{
    public int div = 10;
    public float radius = 0.5f;

    [HideInInspector] public Mesh mesh;

    public void OnParamUpdated()
    {
        CreateMesh();
    }

    public void CreateMesh()
    {
        if(mesh == null) {
            mesh = new Mesh();
        }else {
            mesh.Clear();
        }

        List<Vector3> vertices = new List<Vector3>();
        List<int> indices = new List<int>();
        int index = 0;

        float rad = Mathf.PI * 2.0f / (float)div;

        for (int i = 0; i < div; i++)
        {
            Vector3 pos = new Vector3(Mathf.Cos(rad * i), 0, Mathf.Sin(rad * i)) * radius;
            vertices.Add(pos);
            indices.Add(index++);
        }
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.vertices = vertices.ToArray();
        mesh.SetIndices(indices.ToArray(), MeshTopology.Points, 0);
        mesh.RecalculateBounds();
    }
}

#endif
}
