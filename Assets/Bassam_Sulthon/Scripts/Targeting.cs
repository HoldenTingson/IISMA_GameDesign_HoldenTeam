using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    public Transform target;
    public Laser laser;
    public Vector2 distance = Vector2.zero;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (laser.Getanim() >4)
        {
            return;
        }

        if (Vector2.Distance(transform.position, target.position) < 4)
        {
            laser.Go();
        }
        // Calculate the direction to the target
        Vector2 direction = target.position - transform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the GameObject
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 270));
        
    }
}
