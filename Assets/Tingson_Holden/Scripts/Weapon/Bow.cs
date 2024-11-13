using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo _weaponInfo;
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private Transform _arrowSpawnPoint;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private Animator _myAnimator;

    private void Awake()
    {
        _myAnimator = GetComponent<Animator>();
    }

    public void Attack()
    {
        _myAnimator.SetTrigger(FIRE_HASH);
        GameObject newArrow = Instantiate(_arrowPrefab, _arrowSpawnPoint.position,
            ActiveWeapon.Instance.transform.rotation);
        newArrow.GetComponent<Projectile>().UpdateWeaponInfo(_weaponInfo);
    }

    public WeaponInfo GetWeaponInfo()
    {
        return _weaponInfo;
    }
}
