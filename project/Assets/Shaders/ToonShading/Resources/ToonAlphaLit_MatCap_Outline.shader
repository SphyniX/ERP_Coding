Shader "Toon/Alpha Lit MatCap Outline" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_AlphaTex ("Alpha (RGB)", 2D) = "white" {}
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_Outline("Outline width", Float) = .001
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {} 
		_MatCap("MatCap (RGB)", 2D) = "black" {}
		_MColor("MatCap Color", Color) = (0.5,0.5,0.5,1)
	}

	SubShader {
		Tags { "Queue" = "AlphaTest" }
		
		CGPROGRAM
		#pragma surface surf ToonRamp vertex:vert nolightmap nodirlightmap noforwardadd approxview halfasview

		sampler2D _Ramp;
		sampler2D _MainTex;
		sampler2D _AlphaTex;
		sampler2D _MatCap;
		float4 _Color;
		float4 _MColor;
		
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
			half2 matcap;
		};

		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			//MATCAP
			float3 worldNorm = normalize(_World2Object[0].xyz * v.normal.x + _World2Object[1].xyz * v.normal.y + _World2Object[2].xyz * v.normal.z);
			worldNorm = mul((float3x3)UNITY_MATRIX_V, worldNorm);
			o.matcap.xy = worldNorm.xy * 0.5 + 0.5;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D(_AlphaTex, IN.uv_MainTex);
			clip(c.r - 0.5);
			o.Alpha = c.r;
			
			c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;

			fixed3 matcap = tex2D(_MatCap, IN.matcap).rgb;
			matcap *= _MColor * c.a;
			o.Emission += matcap;
		}
		ENDCG

		UsePass "Toon/Alpha Outline/OUTLINE"
	} 

	Fallback "Toon/Basic"
}