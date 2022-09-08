Shader "MyShaders/ShowNormals"
{
    SubShader
    {
        Tags{"RenderPipeline"="UniversalPipeline"}
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            struct Attributes
            {
                float4 position: POSITION;
                half3 normal: NORMAL;
            };
            struct v2f
            {
                float4 positionHCS : SV_POSITION;
                half3 normal : TEXCOORD0;
            };
            v2f vert(Attributes v)
            {
                v2f o;
                o.positionHCS = TransformObjectToHClip(v.position.xyz);                
                o.normal = TransformObjectToWorldNormal(v.normal);
                return o;
            }
            half4 frag(v2f i) : SV_Target
            {
                half4 color = 0;
                color.rgb = i.normal * 0.5 + 0.5;
                return color;
            }
            ENDHLSL
        }
    }
    Fallback "Diffuse"
}