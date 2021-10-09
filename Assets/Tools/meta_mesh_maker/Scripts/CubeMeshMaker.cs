using System.Collections.Generic;
using UnityEngine;

public class CubeMeshMaker {

    public Mesh GenerageMesh(int xnum, int ynum, int znum)
    {
        Mesh mesh = new Mesh();

        int vertexCount = xnum * ynum * znum;

        float xdiv = 1f / xnum;
        float ydiv = 1f / ynum;
        float zdiv = 1f / znum;

        List<Vector3> vertices = new List<Vector3>();
        List<int> indices = new List<int>();
        int index = 0;

        for (int i = 0; i < vertexCount; i++)
        {
            Vector3 pos = new Vector3((i % xnum) * xdiv, (i / xnum % ynum) * ydiv, (i / (xnum * ynum) % znum) * zdiv);
            vertices.Add(pos);
            indices.Add(index++);
        }
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.vertices = vertices.ToArray();
        mesh.SetIndices(indices.ToArray(), MeshTopology.Points, 0);
        mesh.RecalculateBounds();

        return mesh;
    }
}