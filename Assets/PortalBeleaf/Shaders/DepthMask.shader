Shader "DepthMask"
{
	Properties{
		_Depth("Depth", Range(0,1)) = 1
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100
		Cull Off
		ZTest Always
		ColorMask 0

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
#if SHADER_API_D3D11 || SHADER_API_D3D9 || SHADER_API_D3D11_9X
				o.depth = 1 - _Depth;
#else
				o.depth = _Depth;
#endif
				return o;
			}
			ENDCG
		}
	}
}


