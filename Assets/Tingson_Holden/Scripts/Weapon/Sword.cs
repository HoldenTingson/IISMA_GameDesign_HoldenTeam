using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject _slashAnimPrefab;
    [SerializeField] private Transform _slashAnimSpawnPoint;
    [SerializeField] private Transform _weaponCollider;
    [SerializeField] private float _attackCooldown = .5f;

    private PlayerControls _playerControls;
    private PlayerController _playerController;
    private Animator _myAnimator;
    private ActiveWeapon _activeWeapon;
    private GameObject _slashAnim;
    private bool _attackButtonDown, _isAttacking = false;

    private void Awake()
    {
        _activeWeapon = GetComponentInParent<ActiveWeapon>();
        _myAnimator = GetComponent<Animator>();
        _playerControls = new PlayerControls();
        _playerController = GetComponentInParent<PlayerController>();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    void Start()
    {
        _playerControls.Combat.Attack.started += _ => StartAttacking();
        _playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        FollowPlayerDirection();
        Attack();
    }

    private void StartAttacking()
    {
        _attackButtonDown = true;
    }

    private void StopAttacking()
    {
        _attackButtonDown = false;
    }

    private void Attack()
    {
        if (_attackButtonDown && !_isAttacking)
        {
            _isAttacking = true;
            _myAnimator.SetTrigger("Attack");
            _weaponCollider.gameObject.SetActive(true);
            _slashAnim = Instantiate(_slashAnimPrefab, _slashAnimSpawnPoint.position, Quaternion.identity);
            _slashAnim.transform.parent = this.transform.parent;
            StartCoroutine(AttackCooldownRoutine());
        }
        
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(_attackCooldown);
        _isAttacking = false;
    }

    public void DoneAttackingAnimEvent()
    {
        _weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimEvent()
    {
        _slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (_playerController.FacingLeft)
        {
            _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent()
    {
        _slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (_playerController.FacingLeft)
        {
            _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void FollowPlayerDirection()
    {
        var horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput < 0)
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, -180, 0);
            _weaponCollider.transform.rotation = Quaternion.Euler(0,-180,0);
            
        } else if (horizontalInput > 0)
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            _weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
    }
}
