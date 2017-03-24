Shader "Unlit/SwitchEmissive"
{
	Properties
	{
		_EmissiveTEx ("Emissive Texture", 2D) = "white" {}
		_NoiseTex ("Noise Texture", 2D) = "white" {}
		_DistanceTex ("Distance Texture", 2D) = "white" {}

		[HDR]
		_EmissiveColor ("Emissive Color", color) = (1.0, 1.0, 1.0, 1.0)

		_RedPos ("Red Pos", Range (0.0, 1.0)) = .5
		_GreenPos ("Green Pos", Range (0.0, 1.0)) = .5
		_BluePos ("Blue Pos", Range (0.0, 1.0)) = .5


	}
	SubShader
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True"}
    	Blend One One
    	ZWrite Off
		LOD 100

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
				float2 noise_uv : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _EmissiveTEx;
			sampler2D _NoiseTex;
			sampler2D _DistanceTex;
			float4 _EmissiveTEx_ST;
			float4 _NoiseTex_ST;
			float4 _EmissiveColor;

			float _RedPos;
			float _GreenPos;
			float _BluePos;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _EmissiveTEx);
				o.noise_uv = TRANSFORM_TEX(v.uv, _NoiseTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the textures
				fixed4 emissive = tex2D(_EmissiveTEx, i.uv);
				fixed4 noise = tex2D(_NoiseTex, i.noise_uv);
				fixed4 distance = tex2D(_DistanceTex, i.uv);

				// adjust noise
				noise = sin(noise * 2 * 6.28 + _Time.y) * .5 + .5;
				noise = noise * 1.5 + .25;

				
				// calculate distance mask, ignore channel == 0 pixels
				float4 mask = smoothstep(distance, distance + .05, float4(_RedPos, _GreenPos, _BluePos, 1));// * (1 - step(distance, 0));
				mask = mask * 1 - step(distance, 0);
				

				mask.rgb = max(max(mask.r, mask.g), mask.b);

				return  mask * (emissive * (_EmissiveColor*noise));

			}
			ENDCG
		}
	}
}
