using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Movement speed
    [SerializeField] private Transform finishZone; // Finish zone position
    [SerializeField] private GameObject winUI; // UI to show when player wins

    private float moveX = 0f; // Horizontal movement
    private float moveY = 0f; // Vertical movement

    private void Start()
    {
        if (winUI != null)
        {
            winUI.SetActive(false); // Hide the Win UI initially
        }
    }

    private void Update()
    {
        HandleMovement();
        CheckFinishZone();
    }

    private void HandleMovement()
    {
        // Reset movement inputs
        moveX = 0f;
        moveY = 0f;

        // Get input for movement
        if (Input.GetKey(KeyCode.W)) moveY = 1f;
        if (Input.GetKey(KeyCode.S)) moveY = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        // Normalize movement
        float magnitude = Mathf.Sqrt((moveX * moveX) + (moveY * moveY));
        if (magnitude > 1f)
        {
            moveX /= magnitude;
            moveY /= magnitude;
        }

        // Apply movement
        transform.position += new Vector3(moveX, moveY, 0) * speed * Time.deltaTime;
    }

    private void CheckFinishZone()
    {
        if (finishZone == null) return;

        // Calculate distance to the finish zone
        float distance = Vector3.Distance(transform.position, finishZone.position);

        if (distance <= 1f)
        {
            // Show win UI
            if (winUI != null)
            {
                winUI.SetActive(true);
            }

            // Stop player movement
            enabled = false;
        }
    }

    internal void RestartScene()
    {
        throw new NotImplementedException();
    }
}
