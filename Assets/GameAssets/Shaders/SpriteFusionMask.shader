Shader "Sprites/FusionMask"
{
	Properties
	{
		_MainTex ("Main Texture", 2D) = "white" {}
		[PerRendererData] _RendererColor ("Main Color", Color) = (1,1,1,1)
		_MaskTex ("Mask", 2D) = "white" {}
		_MaskColor ("Mask Color", Color) = (1,1,1,1)
		_FusionMode ("Fusion Mode", int) = 0
	}
	 
	SubShader
	{
		Tags
		{
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{

			CGPROGRAM

			#pragma vertex vertFunc
			#pragma fragment fragFunc
			#pragma target 2.0
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ SECONDARY_TEXTURE

			#include "UnityCG.cginc"

			CBUFFER_START(UnityPerDrawSprite)
				fixed4 _RendererColor;
				float4 _Flip;
				float _EnableExternalAlpha;
			CBUFFER_END

			struct inputData
			{
				float4 vertex   : POSITION;
				float4 color	: COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct vertex
			{
				float4 vertex   : SV_POSITION;
				fixed4 color	: COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			vertex vertFunc ( inputData IN )
			{
				vertex OUT;

				UNITY_SETUP_INSTANCE_ID (IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

				OUT.vertex = UnityObjectToClipPos(IN.vertex);

				#ifdef PIXELSNAP_ON
					OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				OUT.texcoord = IN.texcoord;

				OUT.color = IN.color;

				return (OUT);
			}

			sampler2D	_MainTex;
			float4		_MainTex_ST;
			sampler2D	_MaskTex;
			float4		_MaskTex_ST;
			fixed4		_MaskColor;
			int			_FusionMode;

			sampler2D	_AlphaTex;

			fixed4 BOverA ( fixed4 primary, fixed4 secondary );
			fixed4 AOverB ( fixed4 primary, fixed4 secondary );
			fixed4 AInB ( fixed4 primary, fixed4 secondary );
			fixed4 AOutB ( fixed4 primary, fixed4 secondary );
			fixed4 BInA ( fixed4 primary, fixed4 secondary );
			fixed4 BOutA ( fixed4 primary, fixed4 secondary );
			fixed4 Additive ( fixed4 primary, fixed4 secondary );
			fixed4 AdditiveInA ( fixed4 primary, fixed4 secondary );

			fixed4 fragFunc ( vertex IN ) : SV_Target
			{
				fixed4 primaryColor = tex2D(_MainTex, TRANSFORM_TEX(IN.texcoord, _MainTex)) * IN.color;
				fixed4 secondaryColor = tex2D(_MaskTex, TRANSFORM_TEX(IN.texcoord, _MaskTex)) * _MaskColor;
				fixed4 c = primaryColor;

				if (_FusionMode == 0) // B OVER A
					c = BOverA(primaryColor, secondaryColor);

				else if (_FusionMode == 1) // A OVER B
					c = AOverB(primaryColor, secondaryColor);

				else if (_FusionMode == 2) // A IN B
					c = AInB(primaryColor, secondaryColor);

				else if (_FusionMode == 3) // A OUT B
					c = AOutB(primaryColor, secondaryColor);

				else if (_FusionMode == 4) // B IN A
					c = BInA(primaryColor, secondaryColor);

				else if (_FusionMode == 5) // B OUT A
					c = BOutA(primaryColor, secondaryColor);

				else if (_FusionMode == 6) // ADDITIVE
					c = Additive(primaryColor, secondaryColor);

				else if (_FusionMode == 7) // ADDITIVE In A
					c = AdditiveInA(primaryColor, secondaryColor);

				//#else

				//	c = primaryColor;
				//	c.rgb *= primaryColor.a;

				//#endif

				return c;
			}

			fixed4 BOverA ( fixed4 primary, fixed4 secondary )
			{
				fixed4	color;
				fixed3	AColor;
				fixed3	BColor;

				AColor = primary.rgb * (primary.a * (1.0 - secondary.a));
				BColor = secondary.rgb * secondary.a;

				color.rgb = AColor + BColor;
				color.a = max(primary.a, secondary.a);
				return (color);
			}

			fixed4 AOverB ( fixed4 primary, fixed4 secondary )
			{
				fixed4	color;
				fixed3	AColor;
				fixed3	BColor;


				AColor = primary.rgb * primary.a;
				BColor = secondary.rgb * (secondary.a * (1.0 - primary.a));

				color.rgb = AColor + BColor;
				color.a = max(secondary.a, primary.a);
				return (color);
			}

			fixed4 Additive ( fixed4 primary, fixed4 secondary )
			{
				fixed4 color;

				color.rgb = primary.rgb * primary.a + secondary.rgb * secondary.a;
				color.a = clamp (primary.a + secondary.a, 0.0, 1.0);
				return (color);
			}

			fixed4 AdditiveInA ( fixed4 primary, fixed4 secondary )
			{
				fixed4 color;

				secondary.a *= primary.a;

				color.rgb = primary.rgb * primary.a + secondary.rgb * secondary.a;
				color.a = clamp (primary.a + secondary.a, 0.0, 1.0);
				return (color);
			}

			fixed4 AInB ( fixed4 primary, fixed4 secondary )
			{
				fixed4 color;

				primary.a *= secondary.a;
				primary.rgb *= primary.a;

				color = primary;
				return (color);
			}

			fixed4 AOutB ( fixed4 primary, fixed4 secondary )
			{
				fixed4 color;
				
				primary.a *= 1.0 - secondary.a;
				primary.rgb *= primary.a;

				color = primary;
				return (color);
			}

			fixed4 BInA ( fixed4 primary, fixed4 secondary )
			{
				fixed4 color;

				secondary.a *= primary.a;
				secondary.rgb *= secondary.a;

				color = secondary;
				return (color);
			}

			fixed4 BOutA ( fixed4 primary, fixed4 secondary )
			{
				fixed4 color;
				
				secondary.a *= 1.0 - primary.a;
				secondary.rgb *= secondary.a;

				color = secondary;
				return (color);
			}

			ENDCG
		}

	}
 
	Fallback off
}



