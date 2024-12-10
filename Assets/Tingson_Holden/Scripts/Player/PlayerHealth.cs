using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool isDead { get; private set; }
    public bool isDeadFinal { get; private set; }

    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private float _knockBackThrustAmount = 10f;
    [SerializeField] private float _damageRecoveryTime = 1f;

    private Slider _healthSlider;
    private int _currentHealth;
    private bool _canTakeDamage = true;
    private KnockBack _knockBack;
    private Flash _flash;

    private const string FIRST_STAGE = "Level 1";
    private const string FINAL_STAGE = "Level 4";
    private readonly int DEATH_HASH = Animator.StringToHash("Death");

    protected override void Awake()
    {
        base.Awake();
        _flash = GetComponent<Flash>();
        _knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        isDead = false;
        _currentHealth = _maxHealth;
        UpdateHealthSlider();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        MagicianAI magician = other.gameObject.GetComponent<MagicianAI>();

        AoeShot ghost = other.gameObject.GetComponent<AoeShot>();

        if ((enemy || magician || ghost) && _canTakeDamage )
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

    public void TakeDamage(int damageAmount)
    {
        if (!_canTakeDamage)
        {
            return;
        }

        _canTakeDamage = false;
        _currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
        StartCoroutine(_flash.FlashRoutine());
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
        if (_currentHealth > 0 || isDead) return;

        isDead = true;

        isDeadFinal = SceneManagement.Instance.GetCurrentSceneName() == FINAL_STAGE;
        _currentHealth = 0;
        Destroy(ActiveWeapon.Instance?.gameObject);
        GetComponent<Animator>().SetTrigger(DEATH_HASH);

        string sceneToLoad = isDeadFinal ? FINAL_STAGE : FIRST_STAGE;

        StartCoroutine(LoadSceneAfterDeath(sceneToLoad));
    }

    private IEnumerator LoadSceneAfterDeath(string sceneName)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        SceneManager.LoadScene(sceneName);

        if (sceneName == FIRST_STAGE)
        {
            ActiveInventory.Instance?.RestartWeapon();
        }
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(_damageRecoveryTime);
        _canTakeDamage = true;
    }


}
