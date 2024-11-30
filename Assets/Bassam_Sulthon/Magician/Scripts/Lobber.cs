using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobber : MonoBehaviour
{
    public GameObject shadow;
    public GameObject projectile;
    public GameObject splatter;
    public Transform target;
    public float min_timer = 0.5f;
    public float max_timer = 1.5f;
    private Animator animator;
    private MagicianAI _ai;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LobRoutine());
        animator = GetComponent<Animator>();
        _ai = GetComponent<MagicianAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LobRoutine()
    {
        while (true) // Keep shooting until stopped
        {
            yield return new WaitForSeconds(Random.Range(min_timer, max_timer));
            animator.SetBool("Attacking", true);
            _ai.ChangeState();
            yield return new WaitForSeconds(0.35f);
            LobEm(); // Call the Shoot function
            animator.SetBool("Attacking", false);
            _ai.ChangeState();
        }
    }

    void LobEm()
    {
        GameObject shade = Instantiate(shadow, transform.position, transform.rotation);
        Shade shadey = shade.AddComponent<Shade>();
        shadey.target = target;
        
        GameObject proj = Instantiate(projectile, transform.position, transform.rotation);
        CurvedProjectile curvy = proj.AddComponent<CurvedProjectile>();
        curvy.target = shadey.target;
        curvy.splatter = splatter;
    }
}
