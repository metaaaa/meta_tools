namespace MetaTools
{
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Collections; 

public  static class CubeObjectCreater
{
    [MenuItem("Assets/Create/meta_meshmaker/Cube Object")]
    public static void CreateScriptableObject()
    {
        // cubeObject作成
        var cubeObject = ScriptableObject.CreateInstance<CubeObject>();
        cubeObject.CreateMesh();
        var mesh = cubeObject.mesh;
        mesh.name = "cube";
        cubeObject.mesh = mesh;
        ProjectWindowUtil.CreateAsset(cubeObject, "cubeObject.asset");
    }
}

#endif
}