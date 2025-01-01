using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int _startingHealth = 3;
    [SerializeField] private GameObject _deathVfxPrefab;
    [SerializeField] private float _knockBackThrust;

    public bool TestingMode { get; set; } = false;
    public bool isBos;
    public int _currentHealth;
    private KnockBack _knockBack;
    private Flash _flash;

    private void Awake()
    {
        if (isBos) return;
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

        if (!TestingMode)
        {
            if (isBos) return;
            _knockBack.GetKnockedBack(PlayerController.Instance.transform, _knockBackThrust);
            StartCoroutine(_flash.FlashRoutine());
        }

        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        if (!TestingMode)
        {
            yield return new WaitForSeconds(_flash.GetRestoreMatTime());
        }
        else
        {
            yield return null;
        }

        DetectHealth();
    }

    public void DetectHealth()
    {
        if (isBos) return;
        if (_currentHealth > 0) return;
        if (!TestingMode)
        {
            Instantiate(_deathVfxPrefab, transform.position, Quaternion.identity);
            GetComponent<PickupSpawner>().DropItems();
        }

        Destroy(gameObject);
    }
}