// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/FirstMultiPassShader"
{
	Properties{
		_Outline("Outline Thickness", Range(0.0, 5)) = 0.002
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		[NoScaleOffset] _MainTex("Texture", 2D) = "white" {}
	}
		CGINCLUDE
		#include "UnityCG.cginc"

		half _Outline;
		half4 _OutlineColor;

	struct appdata {
		half4 vertex : POSITION;
		half4 uv : TEXCOORD0;
		half3 normal : NORMAL;
		fixed4 color : COLOR;
	};

	struct v2f {
		half4 pos : POSITION;
		half2 uv : TEXCOORD0;
		fixed4 color : COLOR;
	};

	struct vsOut {
		float4 position : SV_POSITION;
		float3 normal : NORMAL;
	};

	ENDCG

		SubShader
		{
			Tags{
			"RenderType" = "Opaque"
			"Queue" = "Transparent"
			}

		Pass{
			Name "OUTLINE"

			Cull Front

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				half3 norm = mul((half3x3)UNITY_MATRIX_IT_MV, v.normal);
				half2 offset = TransformViewToProjection(norm.xy);
				o.pos.xy += offset * o.pos.z * _Outline;
				o.color = _OutlineColor;
				return o;
			}

			fixed4 frag(v2f i) : COLOR
			{
				fixed4 o;
				o = i.color;
				return o;
			}
			ENDCG
		}

		Pass
		{
			// indicate that our pass is the "base" pass in forward
			// rendering pipeline. It gets ambient and main directional
			// light data set up; light direction in _WorldSpaceLightPos0
			// and color in _LightColor0
			Tags{ "LightMode" = "ForwardBase" }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc" // for UnityObjectToWorldNormal
			#include "UnityLightingCommon.cginc" // for _LightColor0

			struct v2f2
			{
				float2 uv : TEXCOORD0;
				fixed4 diff : COLOR0; // diffuse lighting color
				float4 vertex : SV_POSITION;
			};

			v2f2 vert(appdata_base v)
			{
				v2f2 o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				// get vertex normal in world space
				half3 worldNormal = UnityObjectToWorldNormal(v.normal);
				// dot product between normal and light direction for
				// standard diffuse (Lambert) lighting
				half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
				// factor in the light color
				o.diff = nl * _LightColor0;
				return o;
			}

			sampler2D _MainTex;

			fixed4 frag(v2f2 i) : SV_Target
			{
				// sample texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// multiply by lighting
				col *= i.diff;
				return col;
			}
				ENDCG
		}
	}
}
