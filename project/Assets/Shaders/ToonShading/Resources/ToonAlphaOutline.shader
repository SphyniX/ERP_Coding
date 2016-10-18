Shader "Toon/Alpha Outline" {
	Properties {
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Float ) = .001
		_AlphaTex ("Alpha (RGB)", 2D) = "white" {}
	}
	CGINCLUDE
	#include "UnityCG.cginc"
	//#pragma multi_compile_fog
	
	struct appdata {
		float4 vertex : POSITION;
		float2 texcoord: TEXCOORD0;
		float3 normal : NORMAL;				
	};

	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
		fixed4 color : COLOR;
	};
	
	sampler2D _AlphaTex;
	float4 _MainTex_ST;
	uniform float _Outline;
	uniform float4 _OutlineColor;
	
	v2f vert(appdata v) {
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

		float3 norm   = normalize(mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal));
		float2 offset = TransformViewToProjection(norm.xy);

		o.pos.xy += offset * o.pos.z * _Outline * 0.01;
		o.pos.z += 0.004;
		o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
		o.color = _OutlineColor;
		return o;
	}	

	ENDCG
	
	SubShader {
		Tags {"Queue"="AlphaTest"}
		Pass {
			Name "OUTLINE"
			Lighting Off
			Cull Front
			//Fog { Mode Off }
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 c = tex2D(_AlphaTex, i.uv);
				clip(c.r - 0.3f);
				i.color.a = c.r;
				return i.color; 
			}
			ENDCG
		}
	}
	
	Fallback "Diffuse"
}
