Shader "Cairn/Barrier"
{
	Properties
	{

		[Header(Texture Part)]

		_Texture1 ("Texture 1", 2D) = "white" {}
		_Texture2 ("Texture 2", 2D) = "white" {}
		_TextureColor ("Texture Color", color) = (1.0, 1.0, 1.0, 1.0)
		_TexturePower ("Texture Power", float) = 1.0
		_TextureContribution ("Texture Contribution", float) = 1.0
		_Texture1Speed ("Texture 1 Scroll Speed", Vector) = (-1.0, 0.0, 0.0, 0.0)
		_Texture2Speed ("Texture 2 Scroll Speed", Vector) = (-1.0, 0.0, 0.0, 0.0)

		[Header(Rim Part)]
		_RimColor ("Rim Color", color) = (1.0, 1.0, 1.0, 1.0)
		_RimPower ("Rim Power", float) = 1.0
		_RimContribution ("Rim Contribution", float) = 1.0

		[Header(Fade Part)]
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


		CGINCLUDE
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
				float2 uv1 : TEXCOORD3;
				float2 uv2 : TEXCOORD4;
				float4 vertex : SV_POSITION;
				float3 viewDir : TEXCOORD2;
				float3 normal : NORMAL;
			};

			sampler2D _Texture1;
			sampler2D _Texture2;
			float4 _Texture2_ST;
			float4 _Texture1_ST;
			float4 _TextureColor;
			float _TexturePower;
			float _TextureContribution;
			float2 _Texture1Speed;
			float2 _Texture2Speed;

			float4 _RimColor;
			float _RimPower;
			float _RimContribution;

			float _FadeStart;
			float _FadeEnd;

			v2f vert (appdata v)
			{
				v2f o;

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv1 = TRANSFORM_TEX(v.uv, _Texture1);
				o.uv2 = TRANSFORM_TEX(v.uv, _Texture2);
				o.uv = v.uv;
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = normalize(UnityWorldSpaceViewDir(mul(unity_ObjectToWorld, v.vertex)));

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				
				fixed4 color_out;
				color_out.rgba = 1;

				////  Texture Part

				// sample the base textures
				float2 t1 = _Time.y * _Texture1Speed;
				float2 t2 = _Time.y * _Texture2Speed;
				fixed4 tex1 = tex2D(_Texture1, i.uv1 + t1);
				fixed4 tex2 = tex2D(_Texture2, i.uv2 + t2);
				
				// combine/blend them
				color_out.rgb = min(tex1, tex2);

				// adjust contrast, intensity, and color
				color_out.rgb = pow(color_out.rgb, _TexturePower);
				color_out.rgb *= _TextureContribution * _TextureColor;
				

				//// Rim Part

				float rim = 1 - abs(dot(i.normal, i.viewDir));
				rim = pow(rim, _RimPower);
				color_out.rgb += rim * _RimColor * _RimContribution;


				//// Fade Part

				float fade = 1 - i.uv.y;
				fade = smoothstep(_FadeStart, _FadeEnd, fade);
				color_out.rgb  *= fade;
				
				//// Return
				return saturate(color_out);
			}
		ENDCG

		Pass
		{
			Cull Back
			Stencil {
				Ref 2
				Comp NotEqual
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}

		Pass
		{
			Cull Front
			Stencil {
				Ref 1
				Comp NotEqual
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}


	}
}
