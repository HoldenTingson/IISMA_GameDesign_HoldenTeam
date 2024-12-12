using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject _slashAnimPrefab;
    [SerializeField] private WeaponInfo _weaponInfo;

    private Animator _myAnimator;
    private GameObject _slashAnim;
    private Transform _weaponCollider;
    private Transform _slashAnimSpawnPoint;

    private void Awake()
    {
        _myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        _weaponCollider = PlayerController.Instance.GetWeaponCollider();
        _slashAnimSpawnPoint = GameObject.Find("SlashSpawnPoint").transform;
    }

    private void Update()
    {
        FollowPlayerDirection();
    }

    public WeaponInfo GetWeaponInfo()
    {
        return _weaponInfo;
    }

    public void Attack()
    { 
        _myAnimator.SetTrigger("Attack"); 
        _weaponCollider.gameObject.SetActive(true); 
        _slashAnim = Instantiate(_slashAnimPrefab, _slashAnimSpawnPoint.position, Quaternion.identity); 
        _slashAnim.transform.parent = this.transform.parent; 
    }

    public void DoneAttackingAnimEvent()
    {
        _weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimEvent()
    {
        _slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimEvent()
    {
        _slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void FollowPlayerDirection()
    {
        var horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput < 0)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, 0);
            _weaponCollider.transform.rotation = Quaternion.Euler(0,-180,0);
            
        } else if (horizontalInput > 0)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
            _weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
       
    }
}
