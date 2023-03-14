using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WorldChanger : MonoBehaviour
{
    [SerializeField] private WorldManager manager;

    public float cooldownTime = 2f;

    private bool isCooldown = false;
    private float cooldownTimer = 0f;

    private void Update()
    {
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isCooldown = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCooldown) return;
        FPSController fpsController = other.GetComponentInParent<FPSController>();
        if (fpsController == null) return;
        ChangeWorld();
        isCooldown = true;
        cooldownTimer = cooldownTime;
    }

    public void ChangeWorld()
    {
        Debug.Log("call change world");
        manager.NextWorld();
        gameObject.layer = LayerMask.NameToLayer(manager.PlayerLayersByWorld[manager.WorldIndex]);
    }
}
