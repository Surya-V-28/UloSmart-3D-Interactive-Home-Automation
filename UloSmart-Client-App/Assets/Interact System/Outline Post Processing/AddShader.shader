Shader "PostProcessing/Add"
{
	Properties
	{
		_MainCameraTexture("Main Camera Texture", 2D) = "grey" {}
		_BlitTexture("Blit Texture", 2DArray) = "grey" {}
	}

	HLSLINCLUDE
	
	#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
	#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
	
	ENDHLSL

	SubShader
	{
		Tags
		{
			"RenderPipeline" = "HDRenderPipeline"
		}

		Pass
		{
			Name "Add"

			ZWrite Off
			ZTest Always
			Blend Off
			Cull Off

			HLSLPROGRAM
			
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
			};

			TEXTURE2D(_MainCameraTexture);
			TEXTURE2D_X(_BlitTexture);

			FragmentData VertexFunction(VertexData vertexData)
			{
				UNITY_SETUP_INSTANCE_ID(vertexData);

				FragmentData r;
				
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(r);

				r.clipSpacePosition = GetFullScreenTriangleVertexPosition(vertexData.vertexID);
				r.uv = GetFullScreenTriangleTexCoord(vertexData.vertexID);

				return r;
			}

			float4 FragmentFunction(FragmentData fragmentData) : SV_Target
			{
				float4 mainCameraTextureColor = SAMPLE_TEXTURE2D(_MainCameraTexture, s_linear_clamp_sampler, fragmentData.uv);
				float4 outlineTextureColor = SAMPLE_TEXTURE2D_X(_BlitTexture, s_linear_clamp_sampler, fragmentData.uv);				

				return mainCameraTextureColor;
			}

			ENDHLSL
		}
	}
}