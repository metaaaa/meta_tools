namespace MetaTools.RampTextureGenerator
{
#if UNITY_EDITOR
using UnityEngine;

    public class TextureGenerator
    {
        public static Texture2D CreateTexture(Gradient gradient, int width, int height)
        {
            var texture = new Texture2D(width, height);
            texture.wrapMode = TextureWrapMode.Clamp;
            for (int yIndex = 0; yIndex < height; yIndex++)
            {
                for (int xIndex = 0; xIndex < width; xIndex++)
                {
                    float time = (float)xIndex / (width - 1);
                    var color = gradient.Evaluate(time);
                    texture.SetPixel(xIndex, yIndex, color);
                }
            }
            texture.Apply();
            
            return texture;
        }
    }
#endif
}
