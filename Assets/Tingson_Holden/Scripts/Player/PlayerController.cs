using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _dashSpeed = 4f;
    [SerializeField] private TrailRenderer _myTrailRenderer;
    [SerializeField] private Transform _weaponCollider;

    public bool TestingMode { get; set; } = false;
    public bool canAttack = true;
    private PlayerControls _playerControls;
    private Vector2 _movement;
    private bool _moving;
    private float _moveX;
    private float _moveY;
    private Rigidbody2D _rb;
    private Animator _myAnimator;
    private SpriteRenderer _mySpriteRenderer;
    private float _startingMoveSpeed;
    private KnockBack _knockBack;
    private bool _facingLeft = false;
    private bool _isDashing = false;
    public bool _unlockDash = false;
    public bool FacingLeft { get { return _facingLeft; } }

    protected override void Awake()
    {
        base.Awake();
        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _myAnimator = GetComponent<Animator>();
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
        _knockBack = GetComponent<KnockBack>();
    }

    private void Start()
    {
        _playerControls.Combat.Dash.performed += _ => Dash();
        _startingMoveSpeed = _moveSpeed;

        if (TestingMode)
        {
            return;
        }
        ActiveInventory.Instance.EquipStartingWeapon();
        _unlockDash = PlayerHealth.GetPreviousDashState();
    }

    private void OnEnable()
    {
        _playerControls?.Enable();
    }

    private void OnDisable()
    {
        _playerControls?.Disable();
    }

    private void Update()
    {
        if (canAttack)
        {
            PlayerInput();
            if (TestingMode)
            {
                return;
            }
            Animate();
        }
    
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    public Transform GetWeaponCollider()
    {
        return _weaponCollider;
    }

    private void Animate()
    {
        if (_movement.magnitude > 0.1f || _movement.magnitude < -0.1f)
        {
            _moving = true;
        }
        else
        {
            _moving = false;
        }

        if (_moving)
        {
            _myAnimator.SetFloat("moveX", _moveX);
            _myAnimator.SetFloat("moveY", _moveY);
        }

        _myAnimator.SetBool("Moving", _moving);
    }

    public void PlayerInput()
    {
        _moveX = Input.GetAxisRaw("Horizontal");
        _moveY = Input.GetAxisRaw("Vertical");

        _movement = new Vector2(_moveX, _moveY).normalized;

    }

    private void Move()
    {
        if (TestingMode)
        {
            return;
        }
        if (_knockBack.GettingKnockedBack || PlayerHealth.Instance.isDead)
        {
            return;
        }
        _rb.MovePosition(_rb.position + _movement * (_moveSpeed * Time.fixedDeltaTime));
    }


    private void AdjustPlayerFacingDirection()
    {
        var horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput < 0)
        {
            _mySpriteRenderer.flipX = true;
            _facingLeft = true;
        }
        else if (horizontalInput > 0)
        {
            _mySpriteRenderer.flipX = false;
            _facingLeft = false;
        }
    }

    public void UnlockDashAbility()
    {
        _unlockDash = true;
    }

    private void Dash()
    {
        if (!_isDashing && Stamina.Instance.CurrentStamina > 0 && _unlockDash)
        {
            Stamina.Instance.UseStamina();
            _isDashing = true;
            _moveSpeed *= _dashSpeed;
            _myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        _moveSpeed = _startingMoveSpeed;
        _myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        _isDashing = false;
    }

}


