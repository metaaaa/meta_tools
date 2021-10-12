Shader "CustomRenderTexture/#SCRIPTNAME#"
{
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            Name "Initialize"
            CGPROGRAM

            #include "UnityCustomRenderTexture.cginc"

            #pragma vertex InitCustomRenderTextureVertexShader
            #pragma fragment frag

            half4 frag(v2f_init_customrendertexture i) : SV_Target
            {
                return 1.0 - step(0.1, distance(0.5, i.texcoord));
            }
            ENDCG
        }
    }
}