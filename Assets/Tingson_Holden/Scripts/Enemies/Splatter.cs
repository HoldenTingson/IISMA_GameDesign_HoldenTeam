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

        StartCoroutine(DisableColliderAfterTime(colliderActiveTime));
    }

    void Update()
    {
        if (spriteRenderer != null)
        {
            timer += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, timer / time);

            spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);

            if (timer >= time)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator DisableColliderAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

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