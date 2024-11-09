
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material _whiteFlashMat;
    [SerializeField] private float _restoreDefaultMatTime = .2f;

    private Material _defaultMat;
    private SpriteRenderer _spriteRenderer;
    private EnemyHealth _enemyHealth;

    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMat = _spriteRenderer.material;
    }

    public IEnumerator FlashRoutine()
    {
        _spriteRenderer.material = _whiteFlashMat;
        yield return new WaitForSeconds(_restoreDefaultMatTime);
        _spriteRenderer.material = _defaultMat;
        _enemyHealth.DetectHealth();
    }
}
