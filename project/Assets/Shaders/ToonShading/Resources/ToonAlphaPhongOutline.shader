Shader "Toon/Alpha Phong Outline" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Float) = .001
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_AlphaTex ("Alpha (RGB)", 2D) = "white" {}
	}

	SubShader {
		Tags {"Queue"="AlphaTest"}
		UsePass "Toon/Alpha Phong/FORWARD"
		UsePass "Toon/Alpha Outline/OUTLINE"
	}
	
	Fallback "Diffuse"
}
