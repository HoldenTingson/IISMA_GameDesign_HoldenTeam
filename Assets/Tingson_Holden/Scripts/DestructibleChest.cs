using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleChest : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;
    [SerializeField] private Dialogue dialogue; 
    private bool itemDropped = false;  

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if all enemies are defeated and the item hasn't been dropped yet
        if (EnemyManager.Instance.AllEnemiesDefeated() && !itemDropped)
        {
            // Ensure it's the player's weapon or projectile that triggered the chest interaction
            if (other.gameObject.GetComponent<Damage>() || other.gameObject.GetComponent<Projectile>())
            {
                // Drop the item (Bow) if all enemies are defeated and the item hasn't been dropped yet
                DropItemAndDestroyChest();
            }
        }
        else if (!EnemyManager.Instance.AllEnemiesDefeated())
        {
            // If enemies are still alive, show dialogue and stop game time
            ResetDialogue();
            dialogue.StartDialogue();
        }
    }

    private void DropItemAndDestroyChest()
    {
        // Drop the item (Bow) and destroy the chest
        GetComponent<ChestSpawner>().DropItems();
        Instantiate(destroyVFX, transform.position, Quaternion.identity); // VFX for chest destruction
        gameObject.SetActive(false); // Disable chest object

        // Mark the item as dropped to prevent further drops
        itemDropped = true;
    }

    private void ResetDialogue()
    {
        // Reset the dialogue text if necessary
        if (dialogue != null)
        {
            dialogue.ResetDialogue(); // Call the ResetDialogue method to clear and reset dialogue status
        }
    }
}
