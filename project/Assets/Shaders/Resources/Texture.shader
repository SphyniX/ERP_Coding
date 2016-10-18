Shader "ME/Opaque/Texture" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Pass {
			Name "BASE"
			
            settexture[_MainTex] {
                combine texture
            }
        }
	} 
	FallBack "Diffuse"
}
