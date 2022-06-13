namespace MetaTools
{
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Collections; 

public  static class CircleObjectCreater
{
    [MenuItem("Assets/Create/meta_meshmaker/Circle Object")]
    public static void CreateScriptableObject()
    {
        // CircleObject作成
        var circleObject = ScriptableObject.CreateInstance<CircleObject>();
        circleObject.CreateMesh();
        var mesh = circleObject.mesh;
        mesh.name = "circle";
        circleObject.mesh = mesh;
        ProjectWindowUtil.CreateAsset(circleObject, "circleObject.asset");
    }
}

#endif
}