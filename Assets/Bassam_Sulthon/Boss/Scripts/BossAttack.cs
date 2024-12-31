using System.Collections;
using UnityEngine;


public class BossAttack : MonoBehaviour
{
    private Vector3 startPos;
    private GameObject target;
    private BossSprites sprites;
    [SerializeField] private float minTimer = 1f;
    [SerializeField] private float maxTimer = 2f;
    [SerializeField] private float attackDelay = 3f;
    [SerializeField] private float attackRange = 20f;
    private IdleFloat idle;
    [SerializeField] private GameObject Crackle;
    [SerializeField] private GameObject[] Projectile;
    private bool smashing = false;
    private bool avalanching = false;

    private Vector3[] Directions = new Vector3[]
    {
        new Vector3(1, 1, 0),
        new Vector3(1, -1, 0),
        new Vector3(-1, -1, 0),
        new Vector3(-1, 1, 0),
    };
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        sprites = gameObject.GetComponent<BossSprites>();
        startPos = transform.position;
        idle = gameObject.GetComponent<IdleFloat>();
        StartCoroutine(BossRoutine());
    }

    IEnumerator BossRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimer, maxTimer));
            float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);

            if (distanceToPlayer <= attackRange)
            {
                int decider = Random.Range(1, 3);
                switch (decider)
                {
                    case int n when n == 1:
                        StartCoroutine(Avalanche());
                        break;
                    case int n when n <= 2:
                        StartCoroutine(Smash());
                        break;
                }
            }
            yield return new WaitForSeconds(6f + Random.Range(1f, attackDelay));
        }
    }

    IEnumerator Smash()//  ~5 second
    {
        smashing = true;
        idle.enabled = false;
        yield return new WaitForSeconds(1f); // 1second
        float time = 0f;
        sprites.ChangeTo(1);
        while (time <1f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (1f * Time.deltaTime));
            time += Time.deltaTime;
            yield return null;
        }// 1 second

        // Save the target's current position
        Vector3 savedPos = target.transform.position;
        Debug.Log($"Moving to target, distance: {Vector3.Distance(transform.position, savedPos)}");
        // Move to the saved position
        while (Vector3.Distance(transform.position, savedPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, savedPos, 30f * Time.deltaTime);
            yield return null;
        }
        GameObject smashed = Instantiate(Crackle, transform.position, Quaternion.identity);
        CheckAndApplyDamage();

        yield return new WaitForSeconds(2f);// 2 second
        sprites.ChangeTo(0);

        // Return to the starting position
        Debug.Log($"Returning to start, distance: {Vector3.Distance(transform.position, startPos)}");
        while (Vector3.Distance(transform.position, startPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, 15f * Time.deltaTime);
            yield return null;
        }
        idle.enabled = true;
        smashing = false;
    }

    IEnumerator Avalanche()
    {
        avalanching = true;
        Quaternion startRotation = transform.rotation;
        idle.enabled = false;
        float time = 0f;
        yield return new WaitForSeconds(1f);// 1second
        sprites.ChangeTo(2);
        while (time <1f)// 1second
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (1f * Time.deltaTime));
            time += Time.deltaTime;
            yield return null;
        }

        time = 0f;
        while (time <0.2f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (5f * Time.deltaTime));
            time += Time.deltaTime;
            yield return null;
        }
        sprites.ChangeTo(3);
        
        GameObject smashed = Instantiate(Crackle, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);// 1second
        int decider = Random.Range(0, 2);
        smashed = Instantiate(Crackle, transform.position, Quaternion.identity);
        switch (decider)
        {
            case int n when n == 0:
                for (int i = 1; i <= 4; i++)
                {
                    // Instantiate the projectile
                    GameObject projectile = Projectile[decider];

                    BossProjectiles1 properties = projectile.GetComponent<BossProjectiles1>();
                    properties.enabled = true;
                   
                    properties.target = target.transform.position + Directions[i-1];
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    BossSprites bs = projectile.GetComponent<BossSprites>();
                    bs.ChangeTo((int) Random.Range(0, 5));
                }
                break;
            case int n when n == 1:
                for (int i = 0; i < 5; i++)
                {
                    var projectile = Projectile[decider];
                    // Instantiate the projectile
                    var peler = Instantiate(projectile, transform.position, Quaternion.identity);
                    peler.TryGetComponent(out BossProjectile2 bossProjectile2);
                    // Get the properties for the projectile
                    // /properties.enabled = true;

                    // Calculate the direction toward the target
                    Vector3 targetDirection = (target.transform.position - transform.position).normalized;

                    // Define the spread angle range (e.g., -30° to +30°)
                    float spreadAngle = -20f + (i * 10f); // Adjust angles to distribute evenly (e.g., -30, -10, 10, 30)

                    // Rotate the direction by the spread angle
                    Vector3 spreadDirection = Quaternion.Euler(0, 0, spreadAngle) * targetDirection;
                    
                    // Set the projectile's target as a point in the spread direction
                    //properties.target = transform.position + spreadDirection * 10f; // Multiply for distant target simulation
                    bossProjectile2.target = transform.position + spreadDirection * 10f;
                    // Change the sprite of the projectile
                    projectile.TryGetComponent(out BossSprites bs);
                    bs.ChangeTo((int)Random.Range(0, 5));
                }
                break;
            
        }
        

        transform.rotation *= Quaternion.Euler(0f, 0f, 180f);
        transform.position = new Vector3(transform.position.x, transform.position.y + 3f);
        sprites.ChangeTo(4);
        
        yield return new WaitForSeconds(0.1f);
        sprites.ChangeTo(0);
        yield return new WaitForSeconds(0.4f);
        time = 0f;// 0.5second
        
        Quaternion currentRotation = transform.rotation;
        while (time < 1f)
        {
            time += Time.deltaTime;
            float t = time / 1;
            transform.rotation = Quaternion.Slerp(currentRotation, startRotation, t);
            transform.position = Vector3.MoveTowards(transform.position, startPos, 3* Time.deltaTime);
            yield return null;
        }// 1second
        idle.enabled = true;
        avalanching = false;
    }

    private void CheckAndApplyDamage()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1f); // Adjust radius as needed
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerHealth.Instance.TakeDamage(3);
            }
        }
    }

    public void StopBossCoroutine()
    {
        if (smashing)
        {
            StopCoroutine(Smash());
        }

        if (avalanching)
        {
            StopCoroutine(Avalanche());
        }
        StopCoroutine(BossRoutine());
        
    }
}
