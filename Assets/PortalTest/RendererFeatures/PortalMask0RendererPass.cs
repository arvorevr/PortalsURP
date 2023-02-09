using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;

class PortalMask0RendererPass : ScriptableRenderPass
{
    private Mesh _quad;
    private Material _matDeny;
    private MeshFilter[] _portal;
    Material _matAllow;
    public PortalMask0RendererPass(Mesh quad, Material matAllow, Material matDeny, MeshFilter[] portal)
    {
        _quad = quad;
        _matDeny = matDeny;
        _portal = portal;
        _matAllow = matAllow;
    }

    //Unity runs the Execute method every frame.
    //In this method, you can implement your custom rendering functionality.
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        Camera cam = renderingData.cameraData.camera;

        cam.RemoveAllCommandBuffers();

        if (cam.isActiveAndEnabled)
        {
            CommandBuffer cmd = CommandBufferPool.Get(name: "Portal0RendererPass");

            if (_portal == null) return;


            cmd.DrawMesh(_quad,
               //cam.transform.localToWorldMatrix
               cam.cameraToWorldMatrix
               * Matrix4x4.Translate(Vector3.forward * (0.3f * 2))
                , _matDeny, 0, 0); ;

            

            // carve portal
            for (int i = 0; i < _portal.Length; i++)
            {
                MeshFilter p = _portal[i];
                if (p && p.gameObject.activeInHierarchy)
                {
                    cmd.DrawMesh(p.mesh, p.transform.localToWorldMatrix, _matAllow);
                }
            }

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }
}
