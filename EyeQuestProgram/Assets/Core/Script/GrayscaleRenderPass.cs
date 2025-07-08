using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GrayscaleRenderPass : ScriptableRendererFeature
{
    class CustomPass : ScriptableRenderPass
    {
        private Material material;
        private RenderTargetIdentifier source;
        private GrayscaleEffect volumeSettings;

        public CustomPass(Material mat)
        {
            material = mat;
            renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
        }

        public void Setup(RenderTargetIdentifier src)
        {
            source = src;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (material == null) return;

            var stack = VolumeManager.instance.stack;
            volumeSettings = stack.GetComponent<GrayscaleEffect>();
            if (volumeSettings == null || !volumeSettings.active || volumeSettings.intensity.value <= 0f) return;


            CommandBuffer cmd = CommandBufferPool.Get("GrayscaleVolumePass");
            cmd.Blit(source, source, material);
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }

    public Material grayscaleMaterial;
    private CustomPass pass;

    public override void Create()
    {
        if (grayscaleMaterial == null)
        {
            Shader shader = Shader.Find("Hidden/Custom/GrayscaleVolume");
            grayscaleMaterial = new Material(shader);
        }

        pass = new CustomPass(grayscaleMaterial);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        pass.Setup(renderer.cameraColorTarget);
        renderer.EnqueuePass(pass);
    }
}
