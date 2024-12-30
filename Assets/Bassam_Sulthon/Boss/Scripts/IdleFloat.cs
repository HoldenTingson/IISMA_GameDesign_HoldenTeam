using UnityEngine;

public class IdleFloat : MonoBehaviour
{
    [SerializeField] private float floatAmplitude = 0.2f; // How high the object floats
    [SerializeField] private float floatFrequency = 1f; // How fast the object floats

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // Save the initial position
    }

    void Update()
    {
        // Calculate the floating offset using a sine wave
        float floatOffset = Mathf.Sin(Time.time * Mathf.PI * 2f * floatFrequency) * floatAmplitude;

        // Apply the floating effect to the Y position
        transform.position = startPos + new Vector3(0, floatOffset, 0);
    }
}