using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedProjectile : MonoBehaviour
{
    public Transform target; // The destination
    private Vector3 from;    // Starting point
    private Vector3 to;      // Ending point (target position)
    private Vector3 controlPoint; // Control point for the curve
    private float travelTime = 2f; // Total time to reach the target
    private float counter; // Tracks progress along the curve (0 to 1)
    public GameObject splatter;

    void Start()
    {
        from = transform.position;
        to = target.position;

        Vector3 midPoint = (from + to) / 2;
        controlPoint = midPoint + new Vector3(0, Vector3.Distance(from, to) / 2, 0);
    }

    void FixedUpdate()
    {
        counter += Time.fixedDeltaTime / travelTime;

        counter = Mathf.Clamp01(counter);

        Vector3 currentPosition = CalculateBezierPoint(counter, from, controlPoint, to);

        transform.position = currentPosition;

        Vector3 direction = (to - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (counter >= 1f)
        {
            GameObject splat = Instantiate(splatter, transform.position, transform.rotation);
            Splatter ssplat = splat.AddComponent<Splatter>();
            splat.transform.position = transform.position;
            Debug.Log($"Final projectile position: {transform.position}");
            Debug.Log($"Splatter spawned at: {splat.transform.position}");
            Destroy(gameObject);
        }
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        return (u * u * p0) + (2 * u * t * p1) + (t * t * p2);
    }
}
