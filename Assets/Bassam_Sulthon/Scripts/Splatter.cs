using System.Collections;
using UnityEngine;

public class Splatter : E_attack
{
    public float time = 3f; // Total time for the splatter to disappear
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    private Color initialColor; // Initial color of the sprite
    private float timer; // Tracks time elapsed

    void Start()
    {
        damage = 20f;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            initialColor = spriteRenderer.color;
        }

        // Start a coroutine to disable the collider after the first frame
        StartCoroutine(DisableColliderNextFrame());
    }

    void Update()
    {
        if (spriteRenderer != null)
        {
            timer += Time.deltaTime;

            // Calculate the alpha value based on elapsed time
            float alpha = Mathf.Lerp(1f, 0f, timer / time);

            // Apply the new color with updated alpha
            spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

            // Destroy the object when fully faded
            if (timer >= time)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator DisableColliderNextFrame()
    {
        // Wait for one frame
        yield return null;

        // Disable any collider on the object
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Alternatively, if using 3D colliders:
        Collider collider3D = GetComponent<Collider>();
        if (collider3D != null)
        {
            collider3D.enabled = false;
        }
    }
}