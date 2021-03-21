using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    float moveVerticle = 0.0f;
    float moveHorizontal = 0.0f;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    bool canMove = true;
    bool isJumping = false;
    bool isRunning = false;
    bool isClimbing = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        InputManager.instance.Controls.Player.Move.performed+= ctx => Move(ctx.ReadValue<Vector2>());
        InputManager.instance.Controls.Player.Move.canceled += ctx => Stop();
        InputManager.instance.Controls.Player.Jump.performed += ctx => OnJump();
        InputManager.instance.Controls.Player.Run.performed += ctx => ToggleRun();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * moveVerticle : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * moveHorizontal : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if(isJumping && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else if(isClimbing)
        {
            moveDirection.y += moveDirection.x;
            moveDirection.x = 0.0f;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if(!characterController.isGrounded && !isClimbing)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        float yCameraRotation = InputManager.instance.Controls.Camera.Pitch.ReadValue<float>();
        float xCameraRotation = InputManager.instance.Controls.Camera.Yaw.ReadValue<float>();

        if(canMove)
        {
            rotationX += -yCameraRotation * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, xCameraRotation * lookSpeed, 0);
        }
        if(characterController.isGrounded)
        {
            isJumping = false;
        }
    }

    void OnJump()
    {
        isJumping = true;
    }

    void Move(Vector2 direction)
    {
        moveVerticle = direction.y;
        moveHorizontal = direction.x;
    }

    void Stop()
    {
        moveVerticle = 0f;
        moveHorizontal = 0f;
    }

    void ToggleRun()
    {
        isRunning = !isRunning;
    }

    public bool IsClimbing
    {
        get{ return isClimbing; }
        set{ isClimbing = value; }
    }
}
