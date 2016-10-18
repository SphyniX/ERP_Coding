Shader "Toon/Alpha Lit Ghost Outline" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
        _OutlineColor("Outline Color", Color) = (0,0,0,1)
        _Outline("Outline width", Float) = 0.1
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_AlphaTex ("Alpha (RGB)", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {} 

		_GhostColor("Ghost Color", Color) = (0.5,0.5,0.5,1)
		_MatCap("MatCap (RGB)", 2D) = "white" {}
	}

	SubShader {
		Tags{ "Queue" = "AlphaTest" }
				
		UsePass "Hidden/Toon/Ghost/BASE"
		UsePass "Toon/Alpha Lit/FORWARD"
        UsePass "Toon/Alpha Outline/OUTLINE"
	} 

	Fallback "Toon/Basic"
}