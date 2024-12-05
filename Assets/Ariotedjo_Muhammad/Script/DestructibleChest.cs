using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleChest : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;
    [SerializeField] private GameObject bowPickup;  // Bow prefab
    private bool itemDropped = false;  // Status untuk cek apakah item sudah ter-drop

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Jika musuh sudah mati dan item belum di-drop
        if (EnemyManager.Instance.AllEnemiesDefeated() && !itemDropped)
        {
            if (other.gameObject.GetComponent<Damage>() || other.gameObject.GetComponent<Projectile>())
            {
                // Drop item (Bow) jika musuh sudah semua mati dan item belum di-drop
                GetComponent<PickupSpawnerWeapon>().DropItems();
                Instantiate(destroyVFX, transform.position, Quaternion.identity);
                //Destroy(gameObject);
                //itemDropped = true;  // Tandai item sudah di-drop
                gameObject.SetActive(false);
                Debug.Log("Chest destroyed!");
            }
        }
        else if (!EnemyManager.Instance.AllEnemiesDefeated())
        {
            // Jika masih ada musuh, tampilkan dialog
            Debug.Log("There are still enemies left! Defeat them first.");
        }
    }
}