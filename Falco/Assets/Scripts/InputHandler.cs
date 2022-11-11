using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class InputHandler : MonoBehaviour
    {
    public float horizontal;
    public float vertical;
    public float mouseX;
    public float mouseY; 
    public float moveAmount;
    PlayerControls InputActions;
    Vector2 MovementInput;
    Vector2 cameraInput;

    CameraHandler cameraHandler;

    private void Awake()
    {
        cameraHandler = CameraHandler.singlton;
    }
    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        if(cameraHandler != null)
        {
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRot(delta,mouseX,mouseY);
        }
    }
    public void OnEnable()
    {
        if (InputActions == null)
        {
        InputActions = new PlayerControls();
        InputActions.PlayerMovement.Movement.performed += InputActions => MovementInput = InputActions.ReadValue<Vector2>();
        InputActions.PlayerMovement.Camera.performed += I =>cameraInput = I.ReadValue<Vector2>();
        }
        InputActions.Enable();
    }
    private void OnDisable()
    {
        InputActions.Disable();
    }
    public void TickInput(float delta)
    {
        MoveInput(delta);
    }
    private void MoveInput(float delta)
    {
        horizontal = MovementInput.x;
        vertical = MovementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }
 

    }

