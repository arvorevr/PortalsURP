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
    }
}

