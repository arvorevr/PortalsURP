using UnityEngine.Rendering.Universal;
using UnityEngine;
using System.Linq;

public class PortalMaskRendererFeature : ScriptableRendererFeature
{
    private MeshFilter[] portalMeshFilter;
    public Material matAllow;
    public Material matDeny;

    private PortalMaskRendererPass _PortalPass;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
            renderer.EnqueuePass(_PortalPass);
    }

    public override void Create()
    {
        if (portalMeshFilter != null && portalMeshFilter.Length == 0)
            portalMeshFilter = GetPortalMeshFilterReference();

        _PortalPass = new PortalMaskRendererPass(matAllow, matDeny, portalMeshFilter);
        _PortalPass.renderPassEvent = RenderPassEvent.BeforeRenderingOpaques;
    }

    private MeshFilter[] GetPortalMeshFilterReference()
    {
        return Resources.FindObjectsOfTypeAll<Portal>()
            .Select(p => p.GetComponent<MeshFilter>())
            .Where(mf => mf != null)
            .ToArray();
    }
}
