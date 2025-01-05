using System.Collections;
using UnityEngine;

public class Splatter : E_attack
{
    public float time = 3f; // Total time for the splatter to disappear
    public float colliderActiveTime = 0.1f; // Time for which collider remains active
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    private Color initialColor; // Initial color of the sprite
    private float timer; // Tracks time elapsed


    void Awake()
    {
        damage = 20f;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            initialColor = spriteRenderer.color;
        }

        // Start a coroutine to disable the collider after the defined time
        StartCoroutine(DisableColliderAfterTime(colliderActiveTime));
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

    private IEnumerator DisableColliderAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage(2);
        }
    }
}