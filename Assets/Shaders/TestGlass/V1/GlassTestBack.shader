Shader "Toon/BackGlassAdvancedURPGlass"
{
    Properties
    {
        _Color("Main Color", Color) = (1,1,1,0.2)
        _SColor("Specular Color", Color) = (1,1,1,1)

        _SpecSize("Specular Size", Range(0,1)) = 0.8
        _SpecSmooth("Spec Smoothness", Range(0.001,0.2)) = 0.05

        _RimColor("Rim Color", Color) = (0.49,0.94,0.64,1)
        _RimPower("Rim Power", Range(0,8)) = 2

        _InnerColor("Inner Glow Color", Color) = (0.49,0.94,0.64,1)
        _InnerPower("Inner Glow Power", Range(0,8)) = 1

        _Alpha("Transparency", Range(0,1)) = 0.25
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Front

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 normalWS : TEXCOORD0;
                float3 viewDirWS : TEXCOORD1;
                float3 positionWS : TEXCOORD2;
            };

            float4 _Color;
            float4 _SColor;

            float _SpecSize;
            float _SpecSmooth;

            float4 _RimColor;
            float _RimPower;

            float4 _InnerColor;
            float _InnerPower;

            float _Alpha;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                VertexPositionInputs pos = GetVertexPositionInputs(IN.positionOS.xyz);

                OUT.positionHCS = pos.positionCS;
                OUT.positionWS = pos.positionWS;

                OUT.normalWS = normalize(TransformObjectToWorldNormal(IN.normalOS));

                OUT.viewDirWS = normalize(GetWorldSpaceViewDir(pos.positionWS));

                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float3 N = normalize(IN.normalWS);
                float3 V = normalize(IN.viewDirWS);

                // fake directional light
                float3 L = normalize(float3(0.4, 1, 0.3));

                // half vector
                float3 H = normalize(L + V);

                // toon diffuse
                float NdotL = dot(N, L);
                float toon = smoothstep(0.0, 0.02, NdotL);

                // specular
                float spec = dot(N, H);
                spec = smoothstep(_SpecSize, _SpecSize + _SpecSmooth, spec);

                // outer fresnel
                float rim = 1.0 - saturate(dot(V, N));
                rim = pow(rim, _RimPower);

                // inner fresnel
                float inner = saturate(dot(V, N));
                inner = pow(inner, _InnerPower);

                float3 col = 0;

                col += _Color.rgb * toon;
                col += _SColor.rgb * spec * 4;
                col += _RimColor.rgb * rim;
                col += _InnerColor.rgb * inner * 0.25;

                float alpha = _Alpha;
                alpha += rim * 0.3;
                alpha += spec * 0.2;

                return half4(col, saturate(alpha));
            }

            ENDHLSL
        }
    }
}