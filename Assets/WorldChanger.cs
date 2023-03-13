using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WorldChanger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FPSController fpsController = other.GetComponentInParent<FPSController>();
        if (fpsController == null) return;
        ChangeWorld();
    }

    public void ChangeWorld()
    {

    }
}
