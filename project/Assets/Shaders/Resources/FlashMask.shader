Shader "Custom/FlashMask" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_FlowTex ("Flow Light (A)", 2D) = "black" {}
		_uvSpeed ("Speed", float) = 2
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
		//Tags { "RenderType"="Opaque" }
		LOD 200		
		Blend SrcAlpha OneMinusSrcAlpha

		CGPROGRAM
		#pragma surface surf Lambert
		
		sampler2D _MainTex;
		sampler2D _FlowTex;
		float _uvSpeed;

		struct Input {
			float2 uv_MainTex;
		};
		
		void surf (Input IN, inout SurfaceOutput o) {			
			half4 c = tex2D (_MainTex, IN.uv_MainTex);

			float2 uv = IN.uv_MainTex;
			uv.x /= 3;
			uv.x += _Time.y * _uvSpeed;

			float flow = tex2D(_FlowTex, uv).a;

			//o.Albedo = c.rgb + half3(flow, flow, flow);
			o.Emission = c.rgb + half3(flow, flow, flow);
			o.Alpha = c.a;
		}
		ENDCG
	} 
	//FallBack "Diffuse"
}
