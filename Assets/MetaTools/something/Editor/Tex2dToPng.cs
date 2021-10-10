using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tex2dToPng
{
    [MenuItem("metaaa/something/tex2png")]
    public static void SaveSelection()
    {
        var tex = (Texture2D)Selection.activeObject;
        if (tex == null)
            return;
        var pngData = tex.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/tex.png", pngData);
        Object.DestroyImmediate(tex);
    }
}

public class Tex2dToExr
{
    [MenuItem("metaaa/something/tex2exr")]
    public static void SaveSelection()
    {
        var tex = (Texture2D)Selection.activeObject;
        if (tex == null)
            return;
        var exrData = tex.EncodeToEXR(Texture2D.EXRFlags.CompressZIP);
        System.IO.File.WriteAllBytes(Application.dataPath + "/tex.exr", exrData);
        Object.DestroyImmediate(tex);
    }
}