using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private float _knockBackThrustAmount = 10f;
    [SerializeField] private float _damageRecoveryTime = 1f;

    private Slider _healthSlider;
    private int _currentHealth;
    private bool _canTakeDamage = true;
    private KnockBack _knockBack;
    private Flash _flash;

    protected override void Awake()
    {
        base.Awake();
        _flash = GetComponent<Flash>();
        _knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        UpdateHealthSlider();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy && _canTakeDamage)
        {
            TakeDamage(1);
            _knockBack.GetKnockedBack(other.gameObject.transform, _knockBackThrustAmount);
            StartCoroutine(_flash.FlashRoutine());
        }
    }

    public void HealPlayer()
    {
        if (_currentHealth < _maxHealth)
        {
            _currentHealth += 1;
            UpdateHealthSlider();
        }
    }

    private void TakeDamage(int damageAmount)
    {
        if (!_canTakeDamage)
        {
            return;
        }

        _canTakeDamage = false;
        _currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHealthSlider();
        CheckIfPlayerDeath();
    }

    private void UpdateHealthSlider()
    {
        if (_healthSlider == null)
        {
            _healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        }

        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = _currentHealth;
    }

    private void CheckIfPlayerDeath()
    {
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Debug.Log("You're death");
        }
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(_damageRecoveryTime);
        _canTakeDamage = true;
    }


}
