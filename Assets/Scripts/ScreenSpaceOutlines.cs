using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RenderGraphModule;

public class ScreenSpaceOutlines : ScriptableRendererFeature
{
    [System.Serializable]
    private class ViewSpaceNormalsTextureSettings
    {

    }

    private class ViewSpaceNormalsTexturePass : ScriptableRenderPass
    {
        //private readonly RenderTargetHandle _normals;

        public ViewSpaceNormalsTexturePass(RenderPassEvent renderPassEvent)
        {
            this.renderPassEvent = renderPassEvent;
            //_normals.Init("_SceneViewSpaceNormals");
        }

        [Obsolete]
        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            //cmd.GetTemporaryRT(_normals.id, cameraTextureDescriptor, FilterMode.Point);
        }

        [Obsolete]
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            
        }
    }
    private class ScreenSpaceOutlinePass : ScriptableRenderPass
    {

    }

    [SerializeField] private RenderPassEvent _renderPassEvent;
    [SerializeField] private ViewSpaceNormalsTextureSettings _viewSpaceNormalsTextureSettings;

    private ViewSpaceNormalsTexturePass  _viewSpaceNormalsTecturePass;
    private ScreenSpaceOutlinePass _screenSpaceOutlinePass;

    public override void Create()
    {
        _viewSpaceNormalsTecturePass = new(_renderPassEvent);
        //_screenSpaceOutlinePass = new(_renderPassEvent);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(_viewSpaceNormalsTecturePass);
        renderer.EnqueuePass(_screenSpaceOutlinePass);
    }
}
