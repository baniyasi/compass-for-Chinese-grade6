Shader "Custom/ShadowedSprite"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ShadowColor ("Shadow Color", Color) = (0,0,0,0.5)
        _ShadowOffset ("Shadow Offset", Vector) = (0.01, 0.01, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 shadowPos : TEXCOORD1;
            };
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _ShadowColor;
            float2 _ShadowOffset;
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.shadowPos = UnityObjectToClipPos(v.vertex + float4(_ShadowOffset, 0, 0));
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 shadow = tex2D(_MainTex, i.shadowPos.xy / i.shadowPos.w); // Adjust for perspective correction if needed.
                return col * (1 - shadow * _ShadowColor.a); // Blend shadow color with sprite color.
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}