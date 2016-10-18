Shader "Toon/Alpha Phong" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_AlphaTex ("Alpha (RGB)", 2D) = "white" {}
		_Metallic ("Metallic", Range(0, 1))  = 0
	}

	SubShader {
		Tags { "Queue" = "AlphaTest" }
		
		CGPROGRAM
		#pragma surface surf BlinnPhong nolightmap nodirlightmap noforwardadd approxview halfasview

		sampler2D _MainTex;
		sampler2D _AlphaTex;
		float4 _Color;
		half _Metallic;

		struct Input {
			float2 uv_MainTex : TEXCOORD0;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D(_AlphaTex, IN.uv_MainTex);
			clip(c.r - 0.5);
			o.Alpha = c.r;
			
			c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			//o.Albedo = c.rgb;
			o.Albedo = lerp(unity_ColorSpaceDielectricSpec.rgb, c.rgb, _Metallic);
		}
		ENDCG
	} 

	Fallback "Toon/Basic"
}