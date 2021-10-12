Shader "CustomRenderTexture/#SCRIPTNAME#"
{
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            Name "Update"

            CGPROGRAM

            #include "UnityCustomRenderTexture.cginc"

            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag

            half4 frag(v2f_customrendertexture i) : SV_Target
            {
                float tw = 1 / _CustomRenderTextureWidth;

                float2 uv = i.globalTexcoord;

                return tex2D(_SelfTexture2D, uv + half2(tw, 0) * 10);
            }

            ENDCG
        }
    }
}