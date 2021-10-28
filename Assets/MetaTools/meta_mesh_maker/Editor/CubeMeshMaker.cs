namespace MetaTools
{
using System.Collections.Generic;
using UnityEngine;

public class CubeMeshMaker {

    public Mesh GenerageMesh(int xnum, int ynum, int znum, Vector3 scale)
    {
        Mesh mesh = new Mesh();

        int vertexCount = xnum * ynum * znum;

        float xdiv = scale.x / xnum;
        float ydiv = scale.y / ynum;
        float zdiv = scale.z / znum;

        List<Vector3> vertices = new List<Vector3>();
        List<int> indices = new List<int>();
        int index = 0;

        for (int i = 0; i < vertexCount; i++)
        {
            Vector3 pos = new Vector3((i % xnum) * xdiv, (i / xnum % ynum) * ydiv, (i / (xnum * ynum) % znum) * zdiv);
            pos -= scale*0.5f;
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
}
