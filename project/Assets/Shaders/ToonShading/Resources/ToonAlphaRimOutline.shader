Shader "Toon/Alpha Rim Outline" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Float) = .001
		_RimColor ("Rim Color", Color) = (1, 1, 1, 1)
		_RimWidth ("Rim Width", Float) = 0.7
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_AlphaTex ("Alpha (RGB)", 2D) = "white" {}
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { }		
	}

	SubShader {
		Tags {"Queue"="AlphaTest"}
		UsePass "Toon/Alpha Rim/RIM"
		UsePass "Toon/Alpha Outline/OUTLINE"
	}
	
	Fallback "Diffuse"
}
