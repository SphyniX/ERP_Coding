// NO utf8 BOM here!

fixed4 _Color;
fixed4 _SColor;
sampler2D _Ramp;

half4 LightingToonRamp (SurfaceOutput s, half3 lightDir, half atten)
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

half4 LightingToonColor(SurfaceOutput s, fixed3 lightDir, fixed atten)
{
#ifndef USING_DIRECTIONAL_LIGHT
	lightDir = normalize(lightDir);
#endif

	half d = dot (s.Normal, lightDir)*0.5 + 0.5;
	half3 ramp = half3 (d, d, d);
	ramp = lerp(_SColor, _Color, ramp);

	half4 c;
	c.rgb = s.Albedo * _LightColor0.rgb * (ramp * atten * 2);
	c.a = 0;
	return c;
}

half4 LightingToonRampColor(SurfaceOutput s, fixed3 lightDir, fixed atten)
{
#ifndef USING_DIRECTIONAL_LIGHT
	lightDir = normalize(lightDir);
#endif

	half d = dot (s.Normal, lightDir)*0.5 + 0.5;
	half3 ramp = tex2D (_Ramp, float2(d,d)).rgb;
	ramp = lerp(_SColor, _Color, ramp);

	half4 c;
	c.rgb = s.Albedo * _LightColor0.rgb * (ramp * atten * 2);
	c.a = 0;
	return c;
}

