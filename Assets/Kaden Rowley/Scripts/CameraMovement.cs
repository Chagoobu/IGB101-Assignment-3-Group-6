using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kaden
{
    public class CameraMovement : MonoBehaviour
    {
        [Header("Camera Nodes")]
        public GameObject[] cameraNodes;

        [Header("Objects To Look At")]
        public GameObject[] objects;

        [Header("Movement Settings")]
        public float moveSpeed = 5.0f;
        public float rotSpeed = 5.0f;

        // Current camera point index
        private int cameraIndex = 0;

        // Distance needed before allowing another move
        private float proximity = 0.1f;

        // Rotation variables
        private Quaternion targetRotation;
        private float adjRotSpeed;

        void Start()
        {
            // Hide cursor at game start
            Cursor.visible = false;

            // Lock cursor to center of screen
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            // Handle camera movement
            Movement();

            // Toggle cursor lock/unlock with ESC
            ToggleCursor();
        }

        private void Movement()
        {
            // Check if camera is close enough to current node
            if (Vector3.Distance(transform.position, cameraNodes[cameraIndex].transform.position) < proximity)
            {
                // Move forward through camera nodes
                if (Keyboard.current.wKey.wasPressedThisFrame)
                {
                    cameraIndex++;

                    // Prevent going past final node
                    if (cameraIndex >= cameraNodes.Length)
                    {
                        cameraIndex = cameraNodes.Length - 1;
                    }
                }

                // Move backward through camera nodes
                else if (Keyboard.current.sKey.wasPressedThisFrame)
                {
                    cameraIndex--;

                    // Prevent going below first node
                    if (cameraIndex < 0)
                    {
                        cameraIndex = 0;
                    }
                }
            }

            // Move camera toward current node
            transform.position = Vector3.MoveTowards(
                transform.position,
                cameraNodes[cameraIndex].transform.position,
                moveSpeed * Time.deltaTime
            );

            // Rotate camera toward target object
            if (objects[cameraIndex] != null)
            {
                targetRotation = Quaternion.LookRotation(
                    objects[cameraIndex].transform.position - transform.position
                );

                adjRotSpeed = Mathf.Min(rotSpeed * Time.deltaTime, 1);

                transform.rotation = Quaternion.Lerp(
                    transform.rotation,
                    targetRotation,
                    adjRotSpeed
                );
            }

            // Play audio attached to current object
            AudioSource audioSource = objects[cameraIndex].GetComponent<AudioSource>();

            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        private void ToggleCursor()
        {
            // Press ESC to toggle cursor
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                // If cursor is locked
                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    // Unlock and show cursor
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    // Lock and hide cursor
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
    }
}