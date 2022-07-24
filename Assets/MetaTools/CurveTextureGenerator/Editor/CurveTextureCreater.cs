namespace MetaTools
{
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Collections;

public  static class CurveTextureCreater
{
    [MenuItem("Assets/Create/ScriptableTexture/CurveTexture")]
    public static void CreateScriptableObject()
    {
        // CurveTextureObject作成
        var CurveTextureObject = ScriptableObject.CreateInstance<CurveTextureObject>();
        CurveTextureObject.CreateSubAsset();
        var subAsset = CurveTextureObject.texture;
        subAsset.name = "CurveTexture";
        CurveTextureObject.texture = subAsset;
        ProjectWindowUtil.CreateAsset(CurveTextureObject, "CurveTextureObject.asset");
    }
}

#endif
}