using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WorldChanger : MonoBehaviour
{
    public UniversalRendererData mask0;
    private void OnTriggerEnter(Collider other)
    {
        FPSController fpsController = other.GetComponentInParent<FPSController>();
        if (fpsController == null) return;

        ChangeWorld();
    }

    public void ChangeWorld()
    {
        Debug.Log("Change World");
        mask0.opaqueLayerMask &= ~(1 << LayerMask.NameToLayer("World1"));
        mask0.opaqueLayerMask |= (1 << LayerMask.NameToLayer("World2"));
    }
}
