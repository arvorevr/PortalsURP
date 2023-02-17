using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNearClipping : MonoBehaviour
{
    public Camera mainCamera;
    public Camera portalCamera;
    public Transform portalPlane;

    private void Update()
    {
        UpdateProjectionMatrix();
    }

    private void UpdateProjectionMatrix()
    {
        Plane p = new Plane(-portalPlane.forward, portalPlane.position);

        Vector4 clipPlane = new Vector4(p.normal.x, p.normal.y, p.normal.z, p.distance);

        Vector4 clipPlaneCameraSpace = Matrix4x4.Transpose(Matrix4x4.Inverse(portalCamera.worldToCameraMatrix)) * clipPlane;

        var newMatrix = mainCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);

        portalCamera.projectionMatrix = newMatrix;
        //OnDrawGizmos();
    }

    private void OnDrawGizmos()
    {
        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(-10, 10, 0);
        vertices[1] = new Vector3(10, 10, 0);
        vertices[2] = new Vector3(10, -10, 0);
        vertices[3] = new Vector3(-10, -10, 0);

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

        Matrix4x4 matrix = portalPlane.localToWorldMatrix * Matrix4x4.Translate(-Vector3.forward * 0.2f);

        Vector3 pos = matrix.GetColumn(3);
        Quaternion rotation = Quaternion.LookRotation(matrix.GetColumn(2), matrix.GetColumn(1));
        Gizmos.color = Color.blue;

        Gizmos.DrawWireMesh(quad, pos, rotation);
    }


}

