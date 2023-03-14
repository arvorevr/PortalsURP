using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class WorldManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera portalCamera;

    [SerializeField] private UniversalRendererData portalRendererData;
    [SerializeField] private UniversalRendererData eyeRendererData;

    [SerializeField] private LayerMask[] portalLayers;
    [SerializeField] private LayerMask[] eyeLayers;
    [SerializeField] private LayerMask nothing;

    [SerializeField] private string[] playerLayersByWorld;
    [SerializeField] private int startWorldIndex;
    private int worldIndex;

    public int WorldIndex { get => worldIndex; set => worldIndex = value; }
    public string[] PlayerLayersByWorld { get => playerLayersByWorld; set => playerLayersByWorld = value; }

    private void Start()
    {   
        SetWorld(startWorldIndex);
    }

    private void SetWorld(int index)
    {
        worldIndex = index;
        portalRendererData.opaqueLayerMask = portalLayers[worldIndex];
        eyeRendererData.opaqueLayerMask = eyeLayers[index];
        player.layer = LayerMask.NameToLayer(playerLayersByWorld[index]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            NextWorld();
        }
    }

    public void NextWorld()
    {
        worldIndex += 1;
        if (portalLayers.Length - 1 < worldIndex)
        {
            SetWorld(startWorldIndex);
            return;
        }
        SetWorld(worldIndex);
    }
}
