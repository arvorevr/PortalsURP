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
