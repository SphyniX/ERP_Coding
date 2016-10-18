Shader "FX/Lightmap" {
        Properties {
            _MainTex ("Texture", 2D) = "white" {}
			_CutOff("Cut Off", float) = 0.5
        }
        SubShader {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
            CGPROGRAM
			
            #pragma surface surf LM
            #pragma debug
            
			sampler2D _MainTex;
			
			//No Lightmap
            half4 LightingLM (SurfaceOutput s, half3 lightDir, half atten) {
                half NdotL = dot (s.Normal, lightDir);
                half4 c; 
                c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten * 2);
                c.a = s.Alpha;
                return c;
            }
            
            inline fixed4 Enlight(half3 clr, half alpha)
            {
            	clr = clr * clr + clr;
            	return fixed4(clr, alpha);
            }
            
            inline fixed4 LightingLM_SingleLightmap (SurfaceOutput s, fixed4 color) {
                half3 lm = DecodeLightmap (color);
                return Enlight(lm, s.Alpha);
            }

            inline fixed4 LightingLM_DualLightmap (SurfaceOutput s, fixed4 totalColor, fixed4 indirectOnlyColor, half indirectFade) {
                half3 lm = lerp (DecodeLightmap (indirectOnlyColor), DecodeLightmap (totalColor), indirectFade);
                return Enlight(lm, 0);
            }

            inline fixed4 LightingLM_DirLightmap (SurfaceOutput s, fixed4 color, fixed4 scale, bool surfFuncWritesNormal) {
                UNITY_DIRBASIS

                half3 lm = DecodeLightmap (color);
                half3 scalePerBasisVector = DecodeLightmap (scale);

                if (surfFuncWritesNormal)
                {
                    half3 normalInRnmBasis = saturate (mul (unity_DirBasis, s.Normal));
                    lm *= dot (normalInRnmBasis, scalePerBasisVector);
                }
                
                return Enlight(lm, 0);
            }

            struct Input {
                float2 uv_MainTex;
            };
            
			float _CutOff;
			
            void surf (Input IN, inout SurfaceOutput o) {
            	half4 c = tex2D (_MainTex, IN.uv_MainTex);
            	clip(c.a - _CutOff);
                o.Albedo = c.rgb;
                o.Alpha = c.a;
            }
            ENDCG
        }
        Fallback "Diffuse"
    }