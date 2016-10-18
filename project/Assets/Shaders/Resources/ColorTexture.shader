Shader "ME/Opaque/Color Texture" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color("Main Color", Color) = (1,1,1,1)
	}
	SubShader{
		Tags { "RenderType" = "Opaque" }
		Lighting Off
		LOD 200

		Pass {
			Name "Color"
			
            settexture[_MainTex] {
				constantColor[_Color]
                combine texture * constant double
            }
        }
	} 
	FallBack "Diffuse"
}
