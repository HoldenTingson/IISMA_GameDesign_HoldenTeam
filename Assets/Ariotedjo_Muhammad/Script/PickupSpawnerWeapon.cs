using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawnerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject _bowPickup; // Prefab for the bow item

    public void DropItems()
    {
        // Always spawn a bow pickup
        Debug.Log("Dropping items");
        Instantiate(_bowPickup, transform.position, Quaternion.identity);
    }
}