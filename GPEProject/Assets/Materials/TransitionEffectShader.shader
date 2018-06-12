Shader "Transitions/TransitionEffectsShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_TransitionPattern("Transition Pattern", 2D) = "white" {}
		_TransitionAmount("Transition Amount", Range(0, 1)) = 1
		_Color("Color", Color) = (0, 0, 0, 0)
		_FadeValue("Fade Value", Range(0,1)) = 1
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
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _TransitionPattern;
			float _TransitionAmount;
			float _FadeValue;
			fixed4 _Color;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 samplePoint = tex2D(_TransitionPattern, i.uv);
				fixed4 mainTexPoint = tex2D(_MainTex, i.uv);

				if (samplePoint.r < _TransitionAmount)
					return mainTexPoint = lerp(mainTexPoint, _Color, _FadeValue);

				return mainTexPoint;
			}
			ENDCG
		}
	}
}
