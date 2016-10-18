Shader "ME/Fx/TrailShadow"
{
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "white" {}
		_Color ("Tint (RGBA)", Color) = (1,1,1,1)
	}
	
	SubShader
	{
		Tags
		{
			//"Queue"="Transparent"
			"Queue" = "Geometry-100"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		
		Pass
		{		
			//ZTest Greater
			Lighting Off
			//ZWrite Off
			//Offset -1, -1
			//ColorMask RGB
			//AlphaTest Greater .01			
			Blend SrcAlpha OneMinusSrcAlpha
			//ColorMaterial AmbientAndDiffuse
			
			SetTexture [_MainTex]
			{
				constantcolor[_Color]
                combine constant * texture
			}
		}
	}
}