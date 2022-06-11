namespace MetaTools
{
#if UNITY_EDITOR
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
    }
}

public class RenderTex2dToExr
{
    [MenuItem("metaaa/something/rendertex2exr")]
    public static void SaveSelection()
    {
        var renderTex = (RenderTexture)Selection.activeObject;
        if (renderTex != null)
        {
            int width = renderTex.width;
            int height = renderTex.height;

            Texture2D tex = new Texture2D(width, height, TextureFormat.RGBAFloat, false);

            // Read screen contents into the texture
            Graphics.SetRenderTarget(renderTex);
            tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            tex.Apply();

            // Encode texture into the EXR
            byte[] bytes = tex.EncodeToEXR(Texture2D.EXRFlags.CompressZIP);
            System.IO.File.WriteAllBytes(Application.dataPath + "/render_tex.exr", bytes);

        }
    }
}
#endif
}