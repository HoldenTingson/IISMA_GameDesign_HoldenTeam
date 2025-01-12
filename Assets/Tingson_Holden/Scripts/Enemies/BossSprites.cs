using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSprites : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    public void ChangeTo(int sprite)
    {
        spriteRenderer.sprite = sprites[sprite];
    }
}
