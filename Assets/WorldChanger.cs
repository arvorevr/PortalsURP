using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WorldChanger : MonoBehaviour
{
    public UniversalRendererData mask0;
    public UniversalRendererData mask1;

    public FPSController player;

    int mask0Index;
    int mask1Index;

    float delay = 2f;
    float elapsed = 0;

    private void Start()
    {
        mask1Index = 1;
        mask0Index = 2;
        elapsed = 999;
        ChangeWorld();
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        FPSController fpsController = other.GetComponentInParent<FPSController>();
        if (fpsController == null) return;

        ChangeWorld();
    }

    public void ChangeWorld()
    {
        if (elapsed < delay) return;
        Debug.Log("Change World");

        if (mask0Index > 3)
            mask0Index = 1;
        if (mask1Index > 3)
            mask1Index = 1;

        mask0.opaqueLayerMask &= ~(1 << LayerMask.NameToLayer("World1"));
        mask0.opaqueLayerMask &= ~(1 << LayerMask.NameToLayer("World2"));
        mask0.opaqueLayerMask &= ~(1 << LayerMask.NameToLayer("World3"));
        mask1.opaqueLayerMask &= ~(1 << LayerMask.NameToLayer("World1"));
        mask1.opaqueLayerMask &= ~(1 << LayerMask.NameToLayer("World2"));
        mask1.opaqueLayerMask &= ~(1 << LayerMask.NameToLayer("World3"));

        mask0.opaqueLayerMask |= (1 << LayerMask.NameToLayer("World" + mask0Index));
        mask1.opaqueLayerMask |= (1 << LayerMask.NameToLayer("World" + mask1Index));

        player.gameObject.layer = LayerMask.NameToLayer("World" + mask1Index);

        mask0Index++;
        mask1Index++;

        elapsed = 0;
    }
}
