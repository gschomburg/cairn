﻿Shader "Cairn/Barrier"
{
	Properties
	{
		_Texture1 ("Texture 1", 2D) = "white" {}
		_Texture2 ("Texture 2", 2D) = "white" {}
		_ScrollSpeed1 ("Scroll Speed 1", Vector) = (-1.0, 0.0, 0.0, 0.0)
		_ScrollSpeed2 ("Scroll Speed 2", Vector) = (-1.0, 0.0, 0.0, 0.0)
		_TexturePower ("Texture Power", float) = 1.0
		_TextureContribution ("Texture Contribution", float) = 1.0


		_RimColor ("Rim Color", color) = (1.0, 1.0, 1.0, 1.0)
		_RimPower ("Rim Power", float) = 1.0
		_RimContribution ("Rim Contribution", float) = 1.0


		_FadeStart ("Fade Start", float) = .1
		_FadeEnd ("Fade End", float) = .2
	}

	SubShader
	{
		LOD 100
		Tags {"Queue"="Transparent" "IgnoreProjector"="True"}
    	Blend One One
    	ZWrite Off
    	Cull Off

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
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 viewDir : TEXCOORD2;
				float3 normal : NORMAL;
			};

			sampler2D _Texture1;
			sampler2D _Texture2;
			float4 _Texture1_ST;
			float4 _Texture2_ST;
			float2 _ScrollSpeed1;
			float2 _ScrollSpeed2;
			float _TexturePower;
			float _TextureContribution;
			float4 _RimColor;
			float _RimPower;
			float _RimContribution;

			float _FadeStart;
			float _FadeEnd;

			v2f vert (appdata v)
			{
				v2f o;

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _Texture1);
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = normalize(UnityWorldSpaceViewDir(mul(unity_ObjectToWorld, v.vertex)));

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				
				fixed4 color_out;
				color_out.rgba = 1;

				float2 t1 = _Time.y * _ScrollSpeed1;
				float2 t2 = _Time.y * _ScrollSpeed2;
				
				fixed4 tex1 = tex2D(_Texture1, i.uv + t1);
				fixed4 tex2 = tex2D(_Texture2, i.uv + t2);
				
				color_out.rgb = min(tex1, tex2);
				color_out.rgb = pow(color_out.rgb, _TexturePower);
				color_out.rgb *= _TextureContribution;
				
				// calc/apply rim
				float rim = 1 - abs(dot(i.normal, normalize(i.viewDir)));
				rim = pow(rim, _RimPower);
			
				color_out.rgb += rim * _RimColor * _RimContribution;

				// calc/apply fade
				float fade = 1 - i.uv.y;
				fade = smoothstep(_FadeStart, _FadeEnd, fade);
				color_out.rgb  *= fade;
				
				// color_out.g = 1;
				return color_out;
			}
			ENDCG
		}
	}
}
