Shader "Toon/Alpha Rim" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_AlphaTex ("Alpha (RGB)", 2D) = "white" {}
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { }
		_RimColor ("Rim Color", Color) = (1, 1, 1, 1)
		_RimWidth ("Rim Width", Float) = 0.7
	}
	SubShader {
		Pass {
			Name "RIM"
			Lighting Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;				
				float3 cubenormal : TEXCOORD1;
				fixed3 color : COLOR;
			};

			uniform float4 _MainTex_ST;
			uniform fixed4 _RimColor;
			float _RimWidth;

			v2f vert (appdata_base v) {
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);

				float3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
				float dotProduct = 1 - dot(v.normal, viewDir);
			   
				o.color = smoothstep(1 - _RimWidth, 1.0, dotProduct);
				o.color *= _RimColor;

				o.uv = v.texcoord.xy;
				o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0));
				return o;
			}

			uniform sampler2D _MainTex;
			uniform sampler2D _AlphaTex;
			samplerCUBE _ToonShade;
			uniform fixed4 _Color;

			fixed4 frag(v2f i) : COLOR {
				fixed4 col = _Color * tex2D(_MainTex, i.uv);
				col.rgb += i.color;
				fixed4 cube = texCUBE(_ToonShade, i.cubenormal);
				fixed4 texcol = fixed4(2.0f * cube.rgb * col.rgb, col.a);
				
				fixed4 c = tex2D(_AlphaTex, i.uv);
				clip(c.r - 0.3f);
				texcol.a = c.r;
				return texcol;
			}
			ENDCG
		}
	}
	
	Fallback "Diffuse"
}
