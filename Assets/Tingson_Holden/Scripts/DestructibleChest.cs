using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleChest : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;
    private bool itemDropped = false;  // Status to check if the item has been dropped

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if all enemies are defeated and the item hasn't been dropped yet
        if (EnemyManager.Instance.AllEnemiesDefeated() && !itemDropped)
        {
            if (other.gameObject.GetComponent<Damage>() || other.gameObject.GetComponent<Projectile>())
            {
                // Drop the item (Bow) if all enemies are defeated and the item hasn't been dropped yet
                GetComponent<ChestSpawner>().DropItems();
                Instantiate(destroyVFX, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                itemDropped = true;  // Mark the item as dropped
                Debug.Log("Chest destroyed!");
            }
        }
        else if (!EnemyManager.Instance.AllEnemiesDefeated())
        {
            // Show dialog if there are still enemies left
            Debug.Log("There are still enemies left! Defeat them first.");
        }
    }
}