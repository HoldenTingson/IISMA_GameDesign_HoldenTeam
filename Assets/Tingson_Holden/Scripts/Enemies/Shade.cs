using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shade : MonoBehaviour
{
    public Vector3 target; // The destination
    private Vector3 from;    // Starting point
    private Vector3 to;      // Ending point (target position)
    private float travelTime = 2f; // Time it takes to reach the target
    private Vector3 speed; // Movement speed
    private float counter; // Tracks progress

    void Awake()
    {
        from = transform.position;
        to = target;
        speed = (to - from) / travelTime;
    }

    void FixedUpdate()
    {
        transform.position += speed * Time.fixedDeltaTime;

        counter += Time.fixedDeltaTime;

        if (counter >= travelTime || Vector3.Distance(transform.position, to) <= 0.1f)
        {
            Destroy(gameObject);
        }
    }

}