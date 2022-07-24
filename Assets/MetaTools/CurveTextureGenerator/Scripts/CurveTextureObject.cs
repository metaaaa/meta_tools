namespace MetaTools
{
#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class CurveTextureObject : ScriptableObject
{
    public AnimationCurve curve = new AnimationCurve() ;
    public int width = 512;
    public int height = 1;
    public TextureFormat textureFormat = TextureFormat.R16;
    public TextureWrapMode textureWrapMode = TextureWrapMode.Clamp;

    [HideInInspector] public Texture2D texture;
    [HideInInspector] public bool hasSubAsset = false;

    public void OnParamUpdated()
    {
        if(!hasSubAsset){
            AssetDatabase.AddObjectToAsset(texture, this);
            AssetDatabase.SaveAssets();
            hasSubAsset = true;
        }
        CreateSubAsset();
    }

    public void CreateSubAsset()
    {
        if(this.texture == null) {
            this.texture = new Texture2D(width, height, textureFormat, false);
        }else {
            this.texture.Resize(width, height, textureFormat, false);
        }

        texture.wrapMode = textureWrapMode;
        for (int yIndex = 0; yIndex < height; yIndex++)
        {
            for (int xIndex = 0; xIndex < width; xIndex++)
            {
                float time = (float)xIndex / (width - 1);
                var curveVal = curve.Evaluate(time);
                var color = new Color(curveVal, curveVal, curveVal, curveVal);
                texture.SetPixel(xIndex, yIndex, color);
            }
        }
        texture.Apply();
    }
}

#endif
}
