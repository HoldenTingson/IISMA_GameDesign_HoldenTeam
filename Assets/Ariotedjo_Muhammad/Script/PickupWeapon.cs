using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    private enum PickUpType
    {
        BowGlobe,
        LaserGlobe,
    }

    [SerializeField] private PickUpType _pickUpType;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            DetectPickupType();
            Destroy(gameObject);
        }
    }

    private void DetectPickupType()
    {
        switch (_pickUpType)
        {
            case PickUpType.BowGlobe:
                PlayerHealth.Instance.HealPlayer();
                break;
            case PickUpType.LaserGlobe:
                Stamina.Instance.RefreshStamina();
                break;
        }
    }
}
