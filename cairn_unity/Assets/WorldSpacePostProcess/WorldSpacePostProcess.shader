Shader "PostProcess/WorldSpace"
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
				float2 uv_depth : TEXCOORD1;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _CameraDepthTexture;
			float4x4 _InverseViewMatrix;

			float map(float v, float fromMin, float fromMax, float toMin, float toMax) {
				float vN = (v - fromMin) / (fromMax - fromMin);
				return toMin + vN * (toMax - toMin);
			}

			float normalizeInRange(float v, float fromMin, float fromMax) {
				return (v - fromMin) / (fromMax - fromMin);
			}


			fixed4 frag (v2f i) : SV_Target
			{
				// get world space coords of fragment by reversing the projection
				// https://git.allions.net/Alex/UnityShaders/src/master/PostProcessing/TopoSweep/IE_TopoSweep.shader
				float linDepth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv));

				// Get a vector that holds the FOV multipliers for our uv's
				float2 projectionMultipliers = float2(unity_CameraProjection._11, unity_CameraProjection._22);

				// convert from screenSpace to viewSpace by applying a reverse projection procedure
				float3 vpos = float3(
						// convert UV's so they represent a coordinate system with its origin in the middle
						(i.uv * 2 - 1) 
						// translate uv's back from our screens aspect ratio to a quadratic space
						/ projectionMultipliers, -1) // -1 denotes a depth of -1, so in the next step we translate AWAY from the origin
						// slide the whole coordinates by the depth in a reverese projection
						* linDepth;

				// convert from viewSpace to worldSpace
				float4 wsPos = mul(_InverseViewMatrix, float4(vpos, 1));



				// at this point we have linDepth = the distance to the fragment in unity units
				// wsPos the position in world space in unity units.


				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 black;
				black.rgb = 0;
				black.a = 1;
				// visualize wsPos

				// a vectorized map would win here.

				col.r = map(wsPos.x, -.2, .2, 0, 1);
				col.g = map(wsPos.y, -.2, .2, 0, 1);
				col.b = map(wsPos.z, -.2, .2, 0, 1);

				//float groundFog = saturate(normalizeInRange(wsPos.y, 0, - 4));
//				groundFog = clamp(groundFog, 0, 1);

				//fixed4 fogColor;
				//fogColor.r = 1;
				//fogColor.g = 0;
				//fogColor.b = 0;
				//fogColor.a = 1;

				//col = lerp(col, fogColor, groundFog);
				/*if (linDepth > 500) {
					return black;
				}*/
				/*if (unity_CameraProjection[0][2] > 0 ) {
					return black;
				}*/
				col.rgb = _WorldSpaceCameraPos.rgb;
				return col;
				//return _WorldSpaceCameraPos;
			}
			ENDCG
		}
	}
}
