Shader "Toon/Alpha Lit Rim" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
        _RimPower ("Rim Power", float) = 0.5
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_AlphaTex ("Alpha (RGB)", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {}
	}

	SubShader {
		Tags { "Queue" = "AlphaTest" }
		
		CGPROGRAM
		#pragma surface surf ToonRamp nolightmap nodirlightmap noforwardadd approxview halfasview
      
		sampler2D _Ramp;
		sampler2D _MainTex;
		sampler2D _AlphaTex;
		float4 _Color;
		
		// custom lighting function that uses a texture ramp based
		// on angle between light direction and normal
		#pragma lighting ToonRamp exclude_path:prepass
		inline half4 LightingToonRamp (SurfaceOutput s, half3 lightDir, half atten)
		{
			#ifndef USING_DIRECTIONAL_LIGHT
			lightDir = normalize(lightDir);
			#endif
			
			half d = dot (s.Normal, lightDir)*0.5 + 0.5;
			half3 ramp = tex2D (_Ramp, float2(d,d)).rgb;
			
			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
			c.a = 0;
			return c;
		}


		struct Input {
			float2 uv_MainTex : TEXCOORD0;
			float3 viewDir;
		};
		
		float4 _RimColor;
      	float _RimPower;
      	
		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D(_AlphaTex, IN.uv_MainTex);
			clip(c.r - 0.5);
			o.Alpha = c.r;
			
			c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			
	        half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
	        o.Emission = _RimColor.rgb * pow (rim, _RimPower);
		}
		ENDCG
	} 

	Fallback "Toon/Basic"
}
