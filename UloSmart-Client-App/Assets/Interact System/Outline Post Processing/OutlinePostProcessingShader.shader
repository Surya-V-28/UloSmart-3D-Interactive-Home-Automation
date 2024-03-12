Shader "PostProcessing/Outline"
{
	Properties
	{
		_MainTex("Main Texture", 2DArray) = "grey" {}
		_BlitTexture("Blit Texture", 2DArray) = "grey" {}
	}

	HLSLINCLUDE

	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
	#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"

	#pragma vertex VertexFunction
	#pragma fragment FragmentFunction

	struct VertexData
	{
		uint vertexID : SV_VertexID;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	struct FragmentData
	{
		float4 clipSpacePosition : SV_POSITION;
		float2 uv : TEXCOORD0;
		UNITY_VERTEX_OUTPUT_STEREO
	};

	float4 _Color;
	float _BlurSize;

	float4 blur(Texture2DArray toBlur, FragmentData fragmentData)
	{
		float4 r = SAMPLE_TEXTURE2D_X(toBlur, s_linear_clamp_sampler, fragmentData.uv) * 0.38774;
		r += SAMPLE_TEXTURE2D_X(toBlur, s_linear_clamp_sampler, fragmentData.uv + float2(0, _BlurSize * 2)) * 0.06136;
		r += SAMPLE_TEXTURE2D_X(toBlur, s_linear_clamp_sampler, fragmentData.uv + float2(0, _BlurSize)) * 0.24477;
		r += SAMPLE_TEXTURE2D_X(toBlur, s_linear_clamp_sampler, fragmentData.uv + float2(0, _BlurSize * -1)) * 0.24477;
		r += SAMPLE_TEXTURE2D_X(toBlur, s_linear_clamp_sampler, fragmentData.uv + float2(0, _BlurSize * -2)) * 0.06136;

		r += SAMPLE_TEXTURE2D_X(toBlur, s_linear_clamp_sampler, fragmentData.uv) * 0.38774;
		r += SAMPLE_TEXTURE2D_X(toBlur, s_linear_clamp_sampler, fragmentData.uv + float2(_BlurSize * 2.0, 0.0)) * 0.06136;
		r += SAMPLE_TEXTURE2D_X(toBlur, s_linear_clamp_sampler, fragmentData.uv + float2(_BlurSize, 0.0)) * 0.24477;
		r += SAMPLE_TEXTURE2D_X(toBlur, s_linear_clamp_sampler, fragmentData.uv + float2(_BlurSize * -1.0, 0.0)) * 0.24477;
		r += SAMPLE_TEXTURE2D_X(toBlur, s_linear_clamp_sampler, fragmentData.uv + float2(_BlurSize * -2.0, 0.0)) * 0.06136;

		r.a = 1.0;

		return r;
	}

	FragmentData VertexFunction(VertexData vertexData)
	{
		UNITY_SETUP_INSTANCE_ID(vertexData);

		FragmentData r;

		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(r);

		r.clipSpacePosition = GetFullScreenTriangleVertexPosition(vertexData.vertexID);
		r.uv = GetFullScreenTriangleTexCoord(vertexData.vertexID);

		return r;
	}
	
	ENDHLSL

	SubShader
	{
		Tags
		{
			"RenderPipeline" = "HDRenderPipeline"
		}

		Name "Outline"

		ZWrite Off
		ZTest Always
		Cull Off

		Pass
		{
			HLSLPROGRAM

			TEXTURE2D_X(_BlitTexture);						

			float4 FragmentFunction(FragmentData fragmentData) : SV_Target
			{
				return blur(_BlitTexture, fragmentData);
			}

			ENDHLSL			
		}

		Pass
		{
			HLSLPROGRAM

			TEXTURE2D_X(_MainTex);

			float4 FragmentFunction(FragmentData fragmentData) : SV_Target
			{
				return blur(_MainTex, fragmentData);
			}

			ENDHLSL
		}
	}
}