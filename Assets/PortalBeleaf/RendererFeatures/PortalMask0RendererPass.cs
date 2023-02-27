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

        if (cam.isActiveAndEnabled) {
            CommandBuffer cmd = CommandBufferPool.Get(name: "Portal0RendererPass");

            if (_portal == null || _portal.Length <= 0) return;

            Transform portalT = _portal[0].transform.parent;

            Vector3[] vertices = new Vector3[4];
            vertices[0] = new Vector3(-100, 100, 0);
            vertices[1] = new Vector3(100, 100, 0);
            vertices[2] = new Vector3(100, -100, 0);
            vertices[3] = new Vector3(-100, -100, 0);

            int[] triangles = new int[6];
            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;
            triangles[3] = 0;
            triangles[4] = 2;
            triangles[5] = 3;

            Mesh quad = new Mesh();
            quad.vertices = vertices;
            quad.triangles = triangles;

            quad.RecalculateNormals();

            Plane pl = new Plane(-portalT.forward, portalT.position);
            if (pl.GetSide(cam.transform.position)) {
                cmd.DrawMesh(quad,
                    cam.transform.localToWorldMatrix * Matrix4x4.Translate(Vector3.forward * 2)
                 , _matDeny, 0, 0);
            }
            else {

                Matrix4x4 matrix = portalT.localToWorldMatrix * Matrix4x4.Translate(-Vector3.forward * 0.2f);
                cmd.DrawMesh(quad, matrix, _matDeny, 0, 0);


                // carve portal
                for (int i = 0; i < _portal.Length; i++) {
                    MeshFilter p = _portal[i];
                    if (p && p.gameObject.activeInHierarchy) {
                        cmd.DrawMesh(p.mesh, p.transform.localToWorldMatrix, _matAllow);
                    }
                }
            }
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }
}
