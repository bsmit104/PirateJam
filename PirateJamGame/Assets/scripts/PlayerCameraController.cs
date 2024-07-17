using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerCameraController : NetworkBehaviour
{
    public Camera playerCamera;

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            playerCamera.gameObject.SetActive(true);
        }
        else
        {
            playerCamera.gameObject.SetActive(false);
        }
    }
}