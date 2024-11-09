using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _startingHealth = 3;
    [SerializeField] private GameObject _deathVfxPrefab;

    private int _currentHealth;
    private KnockBack _knockBack;
    private Flash _flash;

    private void Awake()
    {
        _flash = GetComponent<Flash>();
        _knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        _currentHealth = _startingHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _knockBack.GetKnockedBack(PlayerController.Instance.transform, 15f);
        StartCoroutine(_flash.FlashRoutine());
    }

    public void DetectHealth()
    {
        if (_currentHealth <= 0)
        {
            Instantiate(_deathVfxPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    
}
