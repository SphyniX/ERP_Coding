Shader "ME/Fx/ShadowBody" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}		
		_Color ("Tint (RGBA)", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		LOD 200
		
		Pass
		{
			Name "INNER"
			Lighting off
			Fog { mode off }
			Cull off
			zwrite off
			Blend SrcAlpha One
			//Blend One One
			//Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
				
			#include "UnityCG.cginc"
	
			struct appdata_t
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
			};
	
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
			};
	
			sampler2D _MainTex;
			fixed4 _Color;
				
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = v.texcoord;
				o.color = v.color;
				return o;
			}
				
			fixed4 frag (v2f i) : COLOR
			{
				fixed4 col = tex2D(_MainTex, i.texcoord);				
				fixed grey = dot(col.rgb, _Color);
        		col.rgb = fixed3( _Color.r, _Color.g, _Color.b) * grey;
				col.a = _Color.a;			
				return col;
				
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
