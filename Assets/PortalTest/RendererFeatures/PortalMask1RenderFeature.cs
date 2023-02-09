using UnityEngine.Rendering.Universal;
using UnityEngine;
using System.Linq;

public class PortalMask1RenderFeature : ScriptableRendererFeature
{
    public Mesh quad;
    public Material matAllow;

    private PortalMask1RendererPass _PortalPass;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
            renderer.EnqueuePass(_PortalPass);
    } 
    public override void Create()
    {
        _PortalPass = new PortalMask1RendererPass(quad, matAllow);
        _PortalPass.renderPassEvent = RenderPassEvent.BeforeRenderingOpaques;
    }

}
