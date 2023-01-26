using UnityEngine.Rendering.Universal;
using UnityEngine;
using System.Linq;

public class PortalMask1RenderFeature : ScriptableRendererFeature
{
    public Mesh quad;
    public Material matAllow;

    private PortalMask1RendererPass _PortalPass;

    //AddRenderPasses: Unity calls this method every frame, once for each Camera.
    //This method lets you inject ScriptableRenderPass instances into the scriptable Renderer.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
            renderer.EnqueuePass(_PortalPass);
    }

    //Create: Unity calls this method on the following events:
    //When the Renderer Feature loads the first time.
    //When you enable or disable the Renderer Feature.
    //When you change a property in the inspector of the Renderer Feature.
    public override void Create()
    {
        _PortalPass = new PortalMask1RendererPass(quad, matAllow);

        _PortalPass.renderPassEvent = RenderPassEvent.BeforeRenderingOpaques;
    }

}
