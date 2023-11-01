using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class CameraWobble : MonoBehaviour
{
    public float amplitude = 0.1f;      // Amount of wobble
    public float frequency = 1.0f;      // Speed of wobble
    public float duration = 1.0f;       // Length of wobble animation

    private Vector3 initialPosition;    // Starting position of camera
    private Quaternion initialRotation; // Starting rotation of camera
    private float timeElapsed;          // Amount of time elapsed in the animation

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        // Calculate new position and rotation based on sine wave
        float x = amplitude * Mathf.Sin(2 * Mathf.PI * frequency * timeElapsed);
        float y = amplitude * Mathf.Cos(2 * Mathf.PI * frequency * timeElapsed);
        Vector3 newPosition = initialPosition + new Vector3(x, y, 0);
        Quaternion newRotation = Quaternion.Euler(0, 0, 5 * Mathf.Sin(2 * Mathf.PI * frequency * timeElapsed));

        // Lerp between initial and new position and rotation over time
        float t = timeElapsed / duration;
        transform.position = Vector3.Lerp(initialPosition, newPosition, t);
        //transform.rotation = Quaternion.Lerp(initialRotation, newRotation, t);

        // Reset time elapsed and initial position/rotation at end of animation
        if (timeElapsed >= duration)
        {
            timeElapsed = 0;
            initialPosition = transform.position;
            initialRotation = transform.rotation;
        }
    }
}
