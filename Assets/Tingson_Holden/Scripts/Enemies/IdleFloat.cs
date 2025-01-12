using UnityEngine;

public class IdleFloat : MonoBehaviour
{
    [SerializeField] private float floatAmplitude = 0.2f; // How high the object floats
    [SerializeField] private float floatFrequency = 1f; // How fast the object floats

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float floatOffset = Mathf.Sin(Time.time * Mathf.PI * 2f * floatFrequency) * floatAmplitude;

        transform.position = startPos + new Vector3(0, floatOffset, 0);
    }
}