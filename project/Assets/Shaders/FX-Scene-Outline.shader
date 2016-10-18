Shader "FX/Scene Outline" {
	Properties {
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Float) = .001
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_CutOff("Cut Off", float) = 0.5
	}

	SubShader {
		Tags {"RenderType"="AlphaTest"}
		
		UsePass "FX/Lightmap/FORWARD"
		
		Pass {
			Lighting Off
			Cull Front

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#include "UnityCG.cginc"
			
			struct appdata {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				UNITY_FOG_COORDS(0)
				fixed4 color : COLOR;
			};
			
			uniform float _Outline;
			uniform float4 _OutlineColor;
			
			fixed4 frag(v2f i) : SV_Target
			{
				UNITY_APPLY_FOG(i.fogCoord, i.color);
				return i.color;
			}
			
			v2f vert(appdata v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(v.normal, 0) * _Outline);

				//o.pos   = normalize(mul ((float3x3)UNITY_MATRIX_IT_MV, v.vertex + float4(v.normal, 0) * _Outline));
				//float2 offset = TransformViewToProjection(norm.xy);

				//o.pos.xy += offset * o.pos.z * _Outline;
				o.color = _OutlineColor;
				
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}
			ENDCG
		}
	
	}
	
	Fallback "FX/Lightmap"
}
