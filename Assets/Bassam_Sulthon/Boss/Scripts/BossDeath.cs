using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class BossDeath : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject Face;
    [SerializeField] private GameObject Body;

    [SerializeField] private GameObject Hand1;

    [SerializeField] private GameObject Hand2;

    [SerializeField] private GameObject BossBar;
    Animator animator;

    private bool running = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = Face.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x <= 0f && !running)
        {
            running = true;
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        SpriteRenderer sbody = Body.GetComponent<SpriteRenderer>();
        SpriteRenderer shand = Hand1.GetComponent<SpriteRenderer>();
        SpriteRenderer shand2 = Hand2.GetComponent<SpriteRenderer>();
        SpriteRenderer sbar = BossBar.GetComponent<SpriteRenderer>();
        BossAttack battack1 = Hand1.GetComponent<BossAttack>();
        battack1.StopBossCoroutine();
        battack1.enabled = false;
        BossAttack battack2 = Hand2.GetComponent<BossAttack>();
        battack2.StopBossCoroutine();
        battack2.enabled = false;
        IdleFloat idle1 = Hand1.GetComponent<IdleFloat>();
        idle1.enabled = false;
        IdleFloat idle2 = Hand2.GetComponent<IdleFloat>();
        idle2.enabled = false;
        animator.enabled = true;
        float time = 0f;
        
        while (time < 1f)
        {
            time += Time.deltaTime;
            if (sbody.color.a > 0f)
            {
                sbody.color += new Color(0f, 0f, 0f, -1f * Time.deltaTime);
                shand.color += new Color(0f, 0f, 0f, -1f * Time.deltaTime);
                shand2.color += new Color(0f, 0f, 0f, -1f * Time.deltaTime);
                sbar.color += new Color(0f, 0f, 0f, -1f * Time.deltaTime);
            }
            yield return null;
        }

        time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            if (sbody.color.a < 1f)
            {
                sbody.color += new Color(0f, 0f, 0f, 1f * Time.deltaTime);
                shand.color += new Color(0f, 0f, 0f, 1f * Time.deltaTime);
                shand2.color += new Color(0f, 0f, 0f, 1f * Time.deltaTime);
                sbar.color += new Color(0f, 0f, 0f, 1f * Time.deltaTime);
            }
            yield return null;
        }
        time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            if (sbody.color.a > 0f)
            {
                sbody.color += new Color(0f, 0f, 0f, -1f * Time.deltaTime);
                shand.color += new Color(0f, 0f, 0f, -1f * Time.deltaTime);
                shand2.color += new Color(0f, 0f, 0f, -1f * Time.deltaTime);
                sbar.color += new Color(0f, 0f, 0f, -1f * Time.deltaTime);
            }
            yield return null;
        }
        time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            if (sbody.color.a < 1f)
            {
                sbody.color += new Color(0f, 0f, 0f, 1f * Time.deltaTime);
                shand.color += new Color(0f, 0f, 0f, 1f * Time.deltaTime);
                shand2.color += new Color(0f, 0f, 0f, 1f * Time.deltaTime);
                sbar.color += new Color(0f, 0f, 0f, 1f * Time.deltaTime);
            }
            yield return null;
        }
        time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            if (sbody.color.a > 0f)
            {
                shand.color += new Color(0f, 0f, 0f, -1f * Time.deltaTime);
                shand2.color += new Color(0f, 0f, 0f, -1f * Time.deltaTime);
                sbar.color += new Color(0f, 0f, 0f, -1f * Time.deltaTime);
                sbody.color += new Color(0f, 0f, 0f, -1f * Time.deltaTime);
            }
            yield return null;
        }
        
        shand.color += new Color(0f, 0f, 0f, 0f);
        shand2.color += new Color(0f, 0f, 0f, 0f );
        sbar.color += new Color(0f, 0f, 0f, 0f );
        sbody.color += new Color(0f, 0f, 0f, 0f);
        Destroy(boss);
    }
}
