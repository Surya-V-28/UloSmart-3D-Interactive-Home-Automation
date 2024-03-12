//using System;
//using Unity.VisualScripting.Antlr3.Runtime.Tree;
//using UnityEngine;
//using UnityEngine.Rendering;
//using UnityEngine.Rendering.HighDefinition;

//[Serializable, VolumeComponentMenuForRenderPipeline("PostProcessing/Outline", typeof(HDRenderPipeline))]
//public sealed class Outline : CustomPostProcessVolumeComponent, IPostProcessComponent
//{
//    public override void Setup()
//    {
//        pngOutputCount = 0;

//        Shader shader = Shader.Find(SHADERPATH);
//        if (shader == null)
//        {
//            Debug.LogError($"Shader {SHADERPATH} not found for Outline Post Process");
//            return;
//        }

//        material = new Material(shader);

//        shader = Shader.Find(EMPTYPOSTPROCESSINGSHADERPATH);
//        if (shader == null)
//        {
//            Debug.LogError($"Shader {EMPTYPOSTPROCESSINGSHADERPATH} not found for Outline Post Process");
//            return;
//        }

//        emptyMaterial = new Material(shader);

//        shader = Shader.Find(ADDSHADERPATH);
//        if (shader == null)
//        {
//            Debug.LogError($"Shader {ADDSHADERPATH} not found for Outline Post Process");
//            return;
//        }
//        addMaterial = new Material(shader);

//        mainCameraTexture = RTHandles.Alloc(scaleFactor: Vector2.one, filterMode: FilterMode.Point, wrapMode: TextureWrapMode.Clamp, dimension: TextureDimension.Tex2D);
//        outlineTexture = RTHandles.Alloc(scaleFactor: Vector2.one, filterMode: FilterMode.Point, wrapMode: TextureWrapMode.Clamp, dimension: TextureDimension.Tex2D);
//        finalResultTexture = RTHandles.Alloc(scaleFactor: Vector2.one, filterMode: FilterMode.Point, wrapMode: TextureWrapMode.Clamp, dimension: TextureDimension.Tex2D);
//    }

//    public override void Render(CommandBuffer commandBuffer, HDCamera camera, RTHandle source, RTHandle destination)
//    {
//        if (material == null || addMaterial == null || camera.camera.transform.name.Equals("SceneCamera"))
//        {
//            commandBuffer.Blit(source, destination, 0, 0);
//            return;
//        }   

//        //Debug.Log($"Rendering camera {camera.camera.transform.name}");
//        if (!camera.camera.transform.name.Equals("Outline Camera"))
//        {
//            HDUtils.BlitCameraTexture(commandBuffer, source, mainCameraTexture, material, 0);
//            commandBuffer.Blit(mainCameraTexture, destination, 0, 0);            

//            addMaterial.SetTexture("_MainCameraTexture", mainCameraTexture);

//            return;
//        }


//        material.SetColor("_Color", color.value);
//        material.SetFloat("_BlurSize", blurSize.value);        

//        HDUtils.BlitCameraTexture(commandBuffer, source, outlineTexture, material, 0);
                
//        commandBuffer.Blit(mainCameraTexture, destination, 0, 0);
//    }

//    public bool IsActive() => material != null;

//    public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.BeforePostProcess;

//    public override void Cleanup()
//    {
//        base.Cleanup();

//        mainCameraTexture.Release();
//        outlineTexture.Release();
//        finalResultTexture.Release();

//        CoreUtils.Destroy(material);
//        CoreUtils.Destroy(emptyMaterial);        
//    }




//    private int pngOutputCount = 0;
    
//    private RTHandle mainCameraTexture = null;
//    private RTHandle outlineTexture = null;
//    private RTHandle finalResultTexture = null;

//    [SerializeField] private ColorParameter color = new ColorParameter(Color.red);
//    [SerializeField] private FloatParameter blurSize = new FloatParameter(3.0f);

//    private Material material = null;
//    private Material emptyMaterial = null;
//    private Material addMaterial = null;

//    private const string SHADERPATH = "PostProcessing/Outline";
//    private const string EMPTYPOSTPROCESSINGSHADERPATH = "PostProcessing/EmptyPostProcessingShader";
//    private const string ADDSHADERPATH = "PostProcessing/Add";
//}
