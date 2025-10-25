using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

public class DistanceFogRendererFeature : ScriptableRendererFeature
{
    class DistanceFogPass : ScriptableRenderPass
    {
        private readonly Material fogMaterial;

        public DistanceFogPass(Material mat)
        {
            fogMaterial = mat;
            renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
        }

        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            var cameraData = frameData.Get<UniversalCameraData>();
            var resourceData = frameData.Get<UniversalResourceData>();
            var builder = renderGraph.AddRenderPass<PassData>("Distance Fog", out var passData);

            //  正しいカラーバッファ取得
            var colorBuffer = resourceData.cameraColor;
            builder.UseColorBuffer(colorBuffer, 0);
            passData.colorBuffer = colorBuffer;

            builder.SetRenderFunc((PassData data, RenderGraphContext ctx) =>
            {
                if (fogMaterial == null) return;
                Blitter.BlitCameraTexture(ctx.cmd, data.colorBuffer, data.colorBuffer, fogMaterial, 0);
            });
        }
    }

    [SerializeField] private Shader fogShader;
    private Material fogMaterial;
    private DistanceFogPass fogPass;

    public override void Create()
    {
        if (fogShader != null)
        {
            fogMaterial = CoreUtils.CreateEngineMaterial(fogShader);
            fogPass = new DistanceFogPass(fogMaterial);
        }
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (fogPass != null)
        {
            renderer.EnqueuePass(fogPass);
        }
    }

    class PassData
    {
        public TextureHandle colorBuffer;
    }
}