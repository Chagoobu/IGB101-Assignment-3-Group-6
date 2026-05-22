using UnityEngine;
using UnityEngine.InputSystem;


namespace Kaden
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        [Header("Target")]
        [Tooltip("Drag the Player's Camera Target child object here.")]
        public Transform cameraTarget;

        [Header("Follow Settings")]
        [Tooltip("How far behind the player the camera sits.")]
        public float distance = 4f;

        [Tooltip("How high above the target the camera sits.")]
        public float height = 2f;

        [Tooltip("How smoothly the camera follows the player.")]
        public float followSmoothness = 10f;

        [Header("Mouse Look Settings")]
        [Tooltip("How quickly the mouse rotates the camera.")]
        public float mouseSensitivity = 120f;

        [Tooltip("Lowest vertical camera angle.")]
        public float minPitch = -25f;

        [Tooltip("Highest vertical camera angle.")]
        public float maxPitch = 60f;

        private float yaw;
        private float pitch = 15f;

        [Header("Camera Collision")]
        [Tooltip("Which layers the camera should not clip through, like walls and floors.")]
        public LayerMask collisionLayers;

        [Tooltip("How wide the camera collision check is.")]
        public float collisionRadius = 0.3f;

        [Tooltip("Small gap between the camera and the wall.")]
        public float collisionOffset = 0.2f;

        void Start()
        {
            // Lock and hide the cursor when the game starts
            LockCursor();

            // Start camera rotation based on current camera angle
            yaw = transform.eulerAngles.y;
        }

        void Update()
        {
            // Press ESC to toggle the cursor on/off
            ToggleCursor();

            // Only rotate the camera when the cursor is locked
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                RotateCamera();
            }
        }

        void LateUpdate()
        {
            // Stop errors if the Camera Target has not been assigned
            if (cameraTarget == null)
            {
                return;
            }

            FollowPlayer();
        }

        private void RotateCamera()
        {
            // Read mouse movement using the new Input System
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            // Horizontal mouse movement rotates around the player
            yaw += mouseDelta.x * mouseSensitivity * Time.deltaTime;

            // Vertical mouse movement tilts camera up/down
            pitch -= mouseDelta.y * mouseSensitivity * Time.deltaTime;

            // Clamp pitch so the camera cannot flip upside down
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }

        private void FollowPlayer()
        {
            // Create rotation from mouse look values
            Quaternion cameraRotation = Quaternion.Euler(pitch, yaw, 0f);

            // The point the camera looks at
            Vector3 targetPosition = cameraTarget.position + Vector3.up * 1f;

            // The normal camera position if there are no walls
            Vector3 desiredPosition =
                cameraTarget.position
                - cameraRotation * Vector3.forward * distance
                + Vector3.up * height;

            // Direction from target to camera
            Vector3 directionToCamera = desiredPosition - targetPosition;

            // Distance from target to desired camera position
            float desiredDistance = directionToCamera.magnitude;

            // Check if there is a wall between the player and the camera
            if (Physics.SphereCast(
                targetPosition,
                collisionRadius,
                directionToCamera.normalized,
                out RaycastHit hit,
                desiredDistance,
                collisionLayers
            ))
            {
                // Move camera in front of the wall instead of through it
                desiredPosition = hit.point - directionToCamera.normalized * collisionOffset;
            }

            // Smoothly move camera to final position
            transform.position = Vector3.Lerp(
                transform.position,
                desiredPosition,
                followSmoothness * Time.deltaTime
            );

            // Look at the player target
            transform.LookAt(targetPosition);
        }

        private void ToggleCursor()
        {
            // Press ESC to switch between locked cursor and visible cursor
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    UnlockCursor();
                }
                else
                {
                    LockCursor();
                }
            }
        }

        private void LockCursor()
        {
            // Cursor is hidden and locked to the centre of the screen
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void UnlockCursor()
        {
            // Cursor is visible and free to move
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}