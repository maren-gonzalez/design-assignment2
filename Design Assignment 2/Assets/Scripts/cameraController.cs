using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform target;    // The object that the camera will orbit around
    public float distance = 50f; // Distance from the target
    public float xSpeed = 120f;  // Horizontal rotation speed
    public float ySpeed = 120f;  // Vertical rotation speed

    public float yMinLimit = -20f;  // Minimum vertical angle
    public float yMaxLimit = 80f;   // Maximum vertical angle

    public float distanceMin = 3f;  // Minimum zoom distance
    public float distanceMax = 15f; // Maximum zoom distance

    private float x = 0.0f;
    private float y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Lock the cursor if necessary
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (target)
        {
            // Mouse input for rotation
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            // Clamp the vertical angle to avoid flipping
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

            // Rotate the camera around the target
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;

            // Zoom with the mouse scroll wheel
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
        }
    }
}
