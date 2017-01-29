Shader "Hidden/VRWorldSpaceFogNoise"
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

			float m_distance_min, m_distance_max, m_height_min, m_height_max;
			float4 m_fog_color;
			samplerCUBE m_noise_texture;
			samplerCUBE m_noise_texture_mask;


			float m_noise_intensity;
			float m_noise_speed;

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


				// calculate intensity of fog
				float ground_fog = saturate(map(wsPos.y, m_height_min, m_height_max, 0, 1));
				float dist_fog = saturate(map(length(wsPos.xz), m_distance_min, m_distance_max, 0, 1));
				float fog = ground_fog * dist_fog;


				// animate noise texture
				float r = _Time.y * m_noise_speed;
				// stepped twist, makes noise move at same linear speed far away and near
				/* r /=  floor(length(wsPos.xz) / 10) ;*/

				// first noise texture
				float3x3 _noise_matrix = float3x3(
					cos(r), 0, sin(r),
					0, 1, 0,
					-sin(r), 0, cos(r)
					);


				// second noise texture
				float3x3 _noise_mask_matrix = float3x3(
					cos(r* .5), 0, sin(r* .5),
					0, 1, 0,
					-sin(r* .5), 0, cos(r* .5)
					);

				// sample the textures
				fixed4 orig_col = tex2D(_MainTex, i.uv);
				fixed4 noise_color = texCUBE(m_noise_texture, mul(_noise_matrix, wsPos).xyz);
				fixed4 noise_mask_color = texCUBE(m_noise_texture_mask, mul(_noise_mask_matrix, wsPos).xyz);

				// blend fog onto original color
				noise_color += noise_mask_color;
				/*float dither = frac(sin(wsPos.x + _Time.x) * 100000)  * step(length(wsPos.xz), m_distance_max) * step(m_distance_min, length(wsPos.xz)) * .01;*/
				fixed4 out_c = lerp(orig_col, m_fog_color, saturate(fog * m_fog_color.a + noise_color * dist_fog * m_noise_intensity)) ;
				
				return out_c;



			}




			ENDCG
		}
	}
}
