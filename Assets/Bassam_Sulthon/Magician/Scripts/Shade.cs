using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shade : MonoBehaviour
{
    public Transform target; // The destination
    private Vector3 from;    // Starting point
    private Vector3 to;      // Ending point (target position)
    private float travelTime = 3f; // Time it takes to reach the target
    private Vector3 speed; // Movement speed
    private float counter; // Tracks progress

    void Awake()
    {
        from = transform.position;
        to = target.position;
        speed = (to - from) / travelTime; // Calculate speed based on travelTime
    }

    void FixedUpdate()
    {
        // Move towards the target
        transform.position += speed * Time.fixedDeltaTime;

        // Increment the counter
        counter += Time.fixedDeltaTime;

        // Destroy the object if it reaches or surpasses the target
        if (counter >= travelTime || Vector3.Distance(transform.position, to) <= 0.1f)
        {
            Destroy(gameObject);
        }
    }
}