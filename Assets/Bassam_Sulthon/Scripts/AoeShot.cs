using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeShot : MonoBehaviour
{
    public GameObject projectilePrefab; // The projectile to spawn
    public float shootForce = 5f;       // Force applied to the projectiles
    public float angleOffset = 0f;     // Offset angle in degrees, if needed
    public float min_timer = 1f;
    public float max_timer = 3f;
    private Vector2[] directions = new Vector2[]
    {
        new Vector2(0, 1),   // Up
        new Vector2(0, -1),  // Down
        new Vector2(-1, 0),  // Left
        new Vector2(1, 0),   // Right
        new Vector2(-1, 1),  // Top-left
        new Vector2(1, 1),   // Top-right
        new Vector2(-1, -1), // Bottom-left
        new Vector2(1, -1),   // Bottom-right
        
        new Vector2(1, 0.5f),
        new Vector2(0.5f, 1),
        new Vector2(-0.5f, 1),
        new Vector2(-1, 0.5f),
        new Vector2(-1, -0.5f),
        new Vector2(-0.5f, -1),
        new Vector2(0.5f, -1),
        new Vector2(1, -0.5f),
        
        // new Vector2(-1, 0.25f),
        // new Vector2(-1, -0.25f),
        // new Vector2(-1, -0.75f),
        // new Vector2(-0.75f, -1),
        // new Vector2(-0.25f, -1),
        // new Vector2(0.25f, -1),
        // new Vector2(0.75f, -1),
        // new Vector2(1, -0.75f),
        // new Vector2(1, -0.25f),
        // new Vector2(1, 0.25f),
        // new Vector2(1, 0.75f),
        // new Vector2(0.75f, 1),
        // new Vector2(0.25f, 1),
        // new Vector2(-0.25f, 1),
        // new Vector2(-0.75f, 1),
        // new Vector2(-1, 0.75f),
    };
    void Start()
    {
        StartCoroutine(ShootRoutine());
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    IEnumerator ShootRoutine()
    {
        while (true) // Keep shooting until stopped
        {
            yield return new WaitForSeconds(Random.Range(min_timer, max_timer)); // Wait for a random time
            Shoot(); // Call the Shoot function
        }
    }
    public void Shoot()
    {
        foreach (Vector2 direction in directions)
        {
            // Instantiate a projectile
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // Normalize the direction and rotate it if needed
            Vector2 finalDirection = Quaternion.Euler(0, 0, angleOffset) * direction.normalized;

            // Apply force to the projectile
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(finalDirection * shootForce, ForceMode2D.Impulse);
            }
        }
    }
}
