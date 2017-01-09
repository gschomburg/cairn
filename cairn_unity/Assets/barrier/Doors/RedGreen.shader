Shader "Debug/RedGreen"
{
	Properties
	{

	}
	SubShader
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True"}
		LOD 100

		CGINCLUDE
			#include "UnityCG.cginc"

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

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col;
				col.r = 1;
				col.gb = 0;
				col.a = 1;
				return col;
			}

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


			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col;
				col.g = 1;
				col.rb = 0;
				col.a = 1;
				return col;
			}
			ENDCG
		}
	}

}
