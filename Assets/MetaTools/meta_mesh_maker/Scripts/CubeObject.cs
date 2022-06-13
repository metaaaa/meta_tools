namespace MetaTools
{
#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class CubeObject : ScriptableObject
{
    public int xnum = 10;
    public int ynum = 10;
    public int znum = 10;

    public Vector3 scale = new Vector3(1.0f, 1.0f, 1.0f);

    [HideInInspector] public Mesh mesh;
    [HideInInspector] public bool hasSubAsset = false;

    public void OnParamUpdated()
    {
        if(!hasSubAsset){
            // var subassetCount = AssetDatabase
            //     .LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(this))
            //     .Where(x => AssetDatabase.IsSubAsset(x))
            //     .Count();
            AssetDatabase.AddObjectToAsset(mesh, this);
            AssetDatabase.SaveAssets();
            hasSubAsset = true;
        }
        CreateMesh();
    }

    public void CreateMesh()
    {
        if(mesh == null) {
            mesh = new Mesh();
        }else {
            mesh.Clear();
        }

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
    }
}

#endif
}
