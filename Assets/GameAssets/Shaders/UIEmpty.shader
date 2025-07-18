Shader "UI/Empty"
{
	Properties
	{
		_MainTex ("Main Texture", 2D) = "white" {}
		[PerRendererData] _RendererColor ("Main Color", Color) = (1,1,1,1)
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
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]
		
		Pass
		{
		
		}
	}
}
