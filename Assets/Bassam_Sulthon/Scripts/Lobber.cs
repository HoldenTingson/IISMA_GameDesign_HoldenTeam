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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LobRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LobRoutine()
    {
        while (true) // Keep shooting until stopped
        {
            yield return new WaitForSeconds(Random.Range(min_timer, max_timer)); // Wait for a random time
            LobEm(); // Call the Shoot function
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
