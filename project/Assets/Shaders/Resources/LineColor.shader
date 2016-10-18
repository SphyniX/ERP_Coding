
Shader "Lines/Colored Blended" 
{ 
	SubShader 
	{ 
		Pass 
		{ 
			Blend SrcAlpha OneMinusSrcAlpha 
			ZWrite Off 
			Cull Front 
			BindChannels { Bind "Color", color }
			Fog { Mode Off } 
		} 
	} 
}
