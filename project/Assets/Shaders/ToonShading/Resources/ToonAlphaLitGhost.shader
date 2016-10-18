Shader "Toon/Alpha Lit Ghost" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
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
	} 

	Fallback "Toon/Basic"
}