using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator MyAnimator;
    private SpriteRenderer _mySpriteRenderer;

    void Start()
    {
        MyAnimator = GetComponent<Animator>();
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        
    }

}
