﻿Shader "Custom/HalfLambert" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Texture (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque"  "Queue" = "Geometry"}
		LOD 200
		ZWrite On
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		//#pragma surface surf Standard fullforwardshadows
		#pragma surface surf LambertCustom

		half4 LightingLambertCustom( SurfaceOutput s, half3 lightDir, half atten )
		{
			half NdotL = dot( lightDir, s.Normal ) * 0.5f + 0.5f;
			
			half4  c;
			c.rgb = (s.Albedo * _LightColor0.rgb * NdotL) * atten;
			c.a = s.Alpha;
			return c;
		}


		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;			
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
