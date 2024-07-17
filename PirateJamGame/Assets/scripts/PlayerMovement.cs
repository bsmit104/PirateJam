using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerMovement : NetworkBehaviour
{
    private NetworkCharacterController _characterController;

    public override void Spawned()
    {
        _characterController = GetComponent<NetworkCharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            Vector3 moveDirection = Vector3.zero;

            if ((data.buttons & NetworkInputData.BUTTON_FORWARD) != 0)
                moveDirection += transform.forward;

            if ((data.buttons & NetworkInputData.BUTTON_BACKWARD) != 0)
                moveDirection -= transform.forward;

            if ((data.buttons & NetworkInputData.BUTTON_LEFT) != 0)
                moveDirection -= transform.right;

            if ((data.buttons & NetworkInputData.BUTTON_RIGHT) != 0)
                moveDirection += transform.right;

            _characterController.Move(moveDirection);
        }
    }
}