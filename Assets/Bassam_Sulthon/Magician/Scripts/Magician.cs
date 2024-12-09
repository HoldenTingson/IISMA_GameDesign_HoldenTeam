using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magician : MonoBehaviour
{
    public GameObject shadow;
    public GameObject projectile;
    public GameObject splatter;

    // Automatically find the player tag at runtime
    private GameObject target;

    public float min_timer = 0.5f;
    public float max_timer = 1.5f;

    private Animator animator;
    private MagicianAI ai;

    void Awake()
    {
        // Find the player by tag, with error handling
        target = GameObject.FindGameObjectWithTag("Player");

        // Add null check to prevent potential null reference exceptions
        if (target == null)
        {
            Debug.LogError("No GameObject with 'Player' tag found in the scene!");
        }

        animator = GetComponent<Animator>();
        ai = GetComponent<MagicianAI>();

        StartCoroutine(LobRoutine());
    }

    void Update()
    {
        // Intentionally left empty
    }

    IEnumerator LobRoutine()
    {
        while (true) // Keep shooting until stopped
        {
            yield return new WaitForSeconds(Random.Range(min_timer, max_timer));

            animator.SetBool("Attacking", true);
            ai.ChangeState();

            yield return new WaitForSeconds(0.35f);

            LobEm(); // Call the Shoot function

            animator.SetBool("Attacking", false);
            ai.ChangeState();
        }
    }

    void LobEm()
    {
        if (target == null) return; // Additional safety check

        Shade shadey = shadow.GetComponent<Shade>();
        shadey.target = target.transform;

        GameObject shade = Instantiate(shadow, transform.position, transform.rotation);

        CurvedProjectile curvy = projectile.GetComponent<CurvedProjectile>();
        curvy.target = shadey.target;
        curvy.splatter = splatter;

        GameObject proj = Instantiate(projectile, transform.position, transform.rotation);
    }
}
