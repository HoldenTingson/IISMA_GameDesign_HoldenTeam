using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pickup : MonoBehaviour
{
    private enum PickUpType
    {
        StaminaGlobe,
        HealthGlobe,
        DashAbility,
        BowItem,
        StaffItem,
        Trophy

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
            case PickUpType.HealthGlobe:
                PlayerHealth.Instance.HealPlayer();
                break;
            case PickUpType.StaminaGlobe:
                Stamina.Instance.RefreshStamina();
                break;
            case PickUpType.DashAbility:
                PlayerController.Instance.UnlockDashAbility();
                break;
            case PickUpType.BowItem:
                ActiveInventory.Instance.AddWeapon("Bow");
                break;
            case PickUpType.StaffItem:
                ActiveInventory.Instance.AddWeapon("Staff");
                break;
            case PickUpType.Trophy:
                SceneManager.LoadScene("Credit");
                break;
        }
    }
}
