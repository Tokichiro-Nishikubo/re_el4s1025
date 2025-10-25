Shader "Custom/DistanceFog"
{
    Properties
    {
        _FogColor ("Fog Color", Color) = (0.8, 0.8, 0.8, 1)
        _FogStart ("Fog Start Distance", Float) = 10
        _FogEnd ("Fog End Distance", Float) = 100
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        Pass
        {
            ZTest Always Cull Off ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            float4 _FogColor;
            float _FogStart;
            float _FogEnd;

            Varyings Vert (Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.worldPos = TransformObjectToWorld(v.positionOS.xyz);
                return o;
            }

            half4 Frag (Varyings i) : SV_Target
            {
                float3 camPos = GetCameraPositionWS();
                float dist = distance(i.worldPos, camPos);
                float fogFactor = saturate((dist - _FogStart) / (_FogEnd - _FogStart));
                return half4(_FogColor.rgb, fogFactor);
            }
            ENDHLSL
        }
    }
}
