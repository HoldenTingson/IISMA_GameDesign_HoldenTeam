using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator MyAnimator;
    private SpriteRenderer mySpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        MyAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
