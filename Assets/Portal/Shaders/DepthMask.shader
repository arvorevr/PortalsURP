Shader "DepthMask"
{
	Properties{
		_Depth("Depth", Range(0,1)) = 1
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		Cull Off
		ZTest Always

		Pass {

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			struct fragOut
			{
				float depth : DEPTH;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			float _Depth;

			fragOut frag(v2f i)
			{
				fragOut o;
				o.depth = 1 -_Depth;
				return o;
			}
			ENDCG
		}
	}
}


