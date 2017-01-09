Shader "Cairn/Door1"
{
	SubShader
	{

		Tags { "RenderType"="Transparent" "Queue"="Transparent-1" "IgnoreProjector"="True"}
		ZWrite Off

		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			Stencil {
				Ref 1
				Comp always
				Pass replace
	    	}


			CGPROGRAM
			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				return half4(1,0,0,0);
			}

			ENDCG
		}
	}
}
