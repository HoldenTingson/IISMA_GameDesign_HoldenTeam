using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private enum PickupType
    {
        HealthGlobe,
        StaminaGlobe,
    }

    [SerializeField] private PickupType _pickupType;
    [SerializeField] private float _pickUpDistance = 5f;
    [SerializeField] private float _accelerationRate = .2f;
    [SerializeField] private float _moveSpeed = 3f;

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
        switch (_pickupType)
        {
            case PickupType.HealthGlobe:
                PlayerHealth.Instance.HealPlayer();
                Debug.Log("HealthGlobe");
                break;
            case PickupType.StaminaGlobe:
                Debug.Log("StaminaGlobe");
                break;
        }
    }
}
