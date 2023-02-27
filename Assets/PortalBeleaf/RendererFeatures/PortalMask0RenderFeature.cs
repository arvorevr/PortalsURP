using UnityEngine.Rendering.Universal;
using UnityEngine;
using System.Linq;

public class PortalMask0RenderFeature : ScriptableRendererFeature
{
    private MeshFilter[] portal;
    public Mesh quad;
    public Material matAllow;
    public Material matDeny;

    private PortalMask0RendererPass _PortalPass;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
            renderer.EnqueuePass(_PortalPass);
    }

    public override void Create()
    {
        if (portal != null && portal.Length == 0)
        {
            portal =
                //transform.root.GetComponentsInChildren<Portal>(true)
                Resources.FindObjectsOfTypeAll<Portal>()
                .Select(p => p.GetComponent<MeshFilter>())
                .Where(mf => mf != null)
                .ToArray();
        }

        _PortalPass = new PortalMask0RendererPass(quad, matAllow, matDeny, portal);
        _PortalPass.renderPassEvent = RenderPassEvent.BeforeRenderingOpaques;
    }

}
