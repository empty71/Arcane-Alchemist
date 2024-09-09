Shader "Custom/Terrain"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {} // This is the texture property
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }

            Pass
            {
                HLSLPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

                struct Attributes
                {
                    float4 positionOS : POSITION;   // Object space position
                    float2 uv : TEXCOORD0;          // UV coordinates for texture sampling
                };

                struct Varyings
                {
                    float4 positionHCS : SV_POSITION; // Homogeneous clip space position
                    float2 uv : TEXCOORD0;            // Passing UV coordinates to fragment shader
                };

                // Declare the texture and sampler
                TEXTURE2D(_MainTex);
                SAMPLER(sampler_MainTex);

                Varyings vert(Attributes IN)
                {
                    Varyings OUT;
                    OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz); // Transform vertex position
                    OUT.uv = IN.uv; // Pass UV coordinates to fragment shader
                    return OUT;
                }

                half4 frag(Varyings IN) : SV_Target
                {
                    // Sample the texture using the UV coordinates
                    half4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
                    return texColor; // Return the texture color as the output
                }
                ENDHLSL
            }
        }
}
