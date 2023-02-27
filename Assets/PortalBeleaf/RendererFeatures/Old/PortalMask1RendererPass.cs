using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;

class PortalMask1RendererPass : ScriptableRenderPass
{
    private Mesh _quad;
    Material _matAllow;
    public PortalMask1RendererPass(Mesh quad, Material matAllow)
    {
        _quad = quad;
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
            CommandBuffer cmd = CommandBufferPool.Get(name: "Portal1RendererPass");

            cmd.DrawMesh(_quad,
                cam.transform.localToWorldMatrix
                * Matrix4x4.Translate(Vector3.forward * (cam.nearClipPlane * 2))
                , _matAllow, 0, 0);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }
}
