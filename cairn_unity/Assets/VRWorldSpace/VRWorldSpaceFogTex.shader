﻿Shader "Hidden/VRWorldSpaceFogTex"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 worldDirection : TEXCOORD1;
			};


			sampler2D _MainTex;
			sampler2D _CameraDepthTexture;
			float _flip;
			float4x4 _ClipToWorld;


			sampler2D m_fog_texture;
			float m_texture_scale;

			v2f vert (appdata v)
			{
				v2f o;


				// http://gamedev.stackexchange.com/questions/131978/shader-reconstructing-position-from-depth-in-vr-through-projection-matrix
				// No need for a matrix multiply here when a FMADD will do.
				o.vertex = v.vertex * float4(2, 2, 1, 1) - float4(1, 1, 0, 0);

				// Construct a vector on the Z = 0 plane corresponding to our screenspace location.
				float4 clip = float4((v.uv.xy * 2.0f - 1.0f) * float2(1, _flip), 0.0f, 1.0f);
				// Use matrix computed in script to convert to worldspace.
				o.worldDirection = mul(_ClipToWorld, clip) -_WorldSpaceCameraPos;

				// UV passthrough.
				// Flipped Y may be a platform-specific difference - check OpenGL version.
				o.uv = v.uv;
				o.uv.y = (_flip < 0) ? 1 - o.uv.y : o.uv.y;


				return o;
			}

			float map(float v, float fromMin, float fromMax, float toMin, float toMax) {
				float vN = (v - fromMin) / (fromMax - fromMin);
				return toMin + vN * (toMax - toMin);
			}


			fixed4 frag (v2f i) : SV_Target
			{




				// Read depth, linearizing into worldspace units.
				float depth = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, i.uv)));
				// Multiply by worldspace direction (no perspective divide needed).
				float3 wsPos = i.worldDirection * depth + _WorldSpaceCameraPos;






				/*float ground_fog = saturate(map(wsPos.y, m_height_min, m_height_max, 0, 1));
				float dist_fog = saturate(map(length(wsPos.xz), m_distance_min, m_distance_max, 0, 1));
				float fog = ground_fog * dist_fog;*/


				fixed4 orig_col = tex2D(_MainTex, i.uv);

				float2 offset;
				offset.xy = .5 / m_texture_scale;

				fixed4 fog_col = tex2D(m_fog_texture, (wsPos.xy + offset) * m_texture_scale);


				float4 c;
				c.a = 1.0;
				c.rgb = lerp(orig_col.rgb, fog_col.rgb, fog_col.a);
				/*c.rgb = orig_col.rgb * fog_col.rgb;*/
				return c;




			}




			ENDCG
		}
	}
}


/*fixed4 orig_col = tex2D(_MainTex, i.uv);


float2 lV = wsPos.xz;

float ground_fog = saturate(map(wsPos.y, 5, -.5, 0, 1));
float dist_fog = saturate(map(length(lV), 5, 10, 0, 1));
float fog = ground_fog * dist_fog;

// noise
 fog -= frac(sin(wsPos.x) * 100000) * .05;




fixed4 fogColor;
fogColor.r = 0;
fogColor.g = 0;
fogColor.b = 0.5;
fogColor.a = 1.0;

//fixed4 noise;
//noise.rgba = frac(sin(wsPos.x) * 100000);
//return noise;

return lerp(orig_col, fogColor, fog);// (col + orig_col) * .5;*/
