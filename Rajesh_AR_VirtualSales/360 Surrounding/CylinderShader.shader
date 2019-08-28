﻿Shader "Unlit/CylinderShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		LeftTex("LeftTexture", 2D) = "white" {}
		RightTex("RightTexture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
		Cull off

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
				float4 objvertex : TEXCOORD1;
				float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			sampler2D LeftTex;
			float4 LeftTex_ST;
			sampler2D RightTex;
			float4 RightTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
				o.objvertex = v.vertex;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				fixed2 uv;
				
				float xz = (i.objvertex.x * i.objvertex.x + i.objvertex.z * i.objvertex.z); //sqrt(i.objvertex.x * i.objvertex.x + i.objvertex.z * i.objvertex.z);
				float latitude = i.objvertex.y; // atan2(i.objvertex.y, xz);
				float longitude = atan2(i.objvertex.z, i.objvertex.x); 
				uv.y = 0.5 + latitude / 3.14159; 
				uv.x = 1 * longitude / (2 * 3.14159);

                // sample the texture
                fixed4 col = tex2D(_MainTex, uv);
				if (unity_StereoEyeIndex == 0)
				{
					col = tex2D(LeftTex, uv);
				}
				else
				{
					col = tex2D(RightTex, uv);
				}
                return col;
            }
            ENDCG
        }
    }
}
