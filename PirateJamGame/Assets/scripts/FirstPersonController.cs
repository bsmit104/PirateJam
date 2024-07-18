using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class FirstPersonController : NetworkBehaviour
{
    public float speed = 5f;
    public float lookSpeed = 2f;
    private CharacterController characterController;
    private Vector2 rotation = Vector2.zero;
    private Camera playerCamera;

    private bool isCursorLocked = true; // Track cursor state

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>(); // Assuming the camera is a child object
    }

    private void Start()
    {
        // Lock and hide cursor initially
        LockCursor();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isCursorLocked = true;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isCursorLocked = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            if (isCursorLocked)
            {
                UnlockCursor();
            }
            else
            {
                LockCursor();
            }
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (Object.HasInputAuthority && isCursorLocked)
        {
            HandleMovement();
            HandleRotation();
        }
    }

    private void HandleMovement()
    {
        float moveForward = Input.GetAxis("Vertical") * speed;
        float moveRight = Input.GetAxis("Horizontal") * speed;
        Vector3 moveDirection = transform.forward * moveForward + transform.right * moveRight;
        characterController.Move(moveDirection * Runner.DeltaTime);
    }

    private void HandleRotation()
    {
        rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
        rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotation.x = Mathf.Clamp(rotation.x, -90, 90);

        transform.eulerAngles = new Vector2(0, rotation.y);
        playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
    }
}
// using UnityEngine;
// using Fusion;

// public class FirstPersonController : NetworkBehaviour
// {
//     public float speed = 5f;
//     public float lookSpeed = 2f;
//     private CharacterController characterController;
//     private Vector2 rotation = Vector2.zero;
//     private Camera playerCamera;

//     private void Awake()
//     {
//         characterController = GetComponent<CharacterController>();
//         playerCamera = GetComponentInChildren<Camera>(); // Assuming the camera is a child object
//     }

//     private void Start()
//     {
//         // Lock and hide cursor
//         Cursor.lockState = CursorLockMode.Locked;
//         Cursor.visible = false;
//     }

//     public override void FixedUpdateNetwork()
//     {
//         if (Object.HasInputAuthority)
//         {
//             HandleMovement();
//             HandleRotation();
//         }
//     }

//     private void HandleMovement()
//     {
//         float moveForward = Input.GetAxis("Vertical") * speed;
//         float moveRight = Input.GetAxis("Horizontal") * speed;
//         Vector3 moveDirection = transform.forward * moveForward + transform.right * moveRight;
//         characterController.Move(moveDirection * Runner.DeltaTime);
//     }

//     private void HandleRotation()
//     {
//         rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
//         rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
//         rotation.x = Mathf.Clamp(rotation.x, -90, 90);

//         transform.eulerAngles = new Vector2(0, rotation.y);
//         playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
//     }

//     // Optional: Handle unlocking cursor (e.g., for pausing the game)
//     private void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Escape))
//         {
//             Cursor.lockState = CursorLockMode.None;
//             Cursor.visible = true;
//         }
//     }
// }