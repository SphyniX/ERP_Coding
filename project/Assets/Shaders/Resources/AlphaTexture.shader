Shader "ME/Transparent/Color Texture" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color("Main Color", Color) = (1,1,1,1)
	}
	SubShader{
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
		Lighting Off
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaTest Greater .01
		LOD 200

		Pass {
			Name "Alpha"
			
            settexture[_MainTex] {
				constantColor[_Color]
                combine texture * constant double
            }
        }
	} 
	FallBack "Diffuse"
}
