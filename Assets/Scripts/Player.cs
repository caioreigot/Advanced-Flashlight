using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    FirstPersonCamera playerCamera;
    CharacterController controller;

    Vector3 forward;
    Vector3 strafe;
    Vector3 vertical;

    float forwardSpeed = 5f;
    float strafeSpeed = 5f;

    float gravity;
    float jumpSpeed;
    float maxJumpHeight = 2f;
    float timeToMaxHeight = 0.5f;

    private GameObject pauseCanvas;

    void Awake() {
        pauseCanvas = GameObject.FindGameObjectWithTag("PauseCanvas");
        pauseCanvas.SetActive(false);
    }

    void Start() {
        playerCamera = FindObjectOfType<FirstPersonCamera>();
        controller = GetComponent<CharacterController>();

        gravity = (-2 * maxJumpHeight) / (timeToMaxHeight * timeToMaxHeight);
        jumpSpeed = (2 * maxJumpHeight) / timeToMaxHeight;
    }

    void Update() {
        HandleMovement();
        HandleCanvas();
    }

    void HandleMovement() {
        float forwardInput = Input.GetAxisRaw("Vertical");
        float strafeInput = Input.GetAxisRaw("Horizontal");

        // force = input * speed * direction
        forward = forwardInput * forwardSpeed * transform.forward;
        strafe = strafeInput * strafeSpeed * transform.right;

        vertical += gravity * Time.deltaTime * Vector3.up;

        if (controller.isGrounded)
            vertical = Vector3.down;

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            vertical = jumpSpeed * Vector3.up;

        if (vertical.y > 0 && (controller.collisionFlags & CollisionFlags.Above) != 0)
            vertical = Vector3.zero;

        Vector3 finalVelocity = forward + strafe + vertical;
        
        controller.Move(finalVelocity * Time.deltaTime);
    }

    void HandleCanvas() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseCanvas.SetActive(!pauseCanvas.activeSelf);

            Cursor.visible = pauseCanvas.activeSelf;
            playerCamera.isLocked = pauseCanvas.activeSelf;

            Cursor.lockState = pauseCanvas.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
            Time.timeScale = pauseCanvas.activeSelf ? 0f : 1f;
        }
    }

}
