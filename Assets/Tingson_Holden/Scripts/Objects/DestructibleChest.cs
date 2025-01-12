using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleChest : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;
    private bool itemDropped = false;  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (EnemyManager.Instance.AllEnemiesDefeated() && !itemDropped)
        {
            if (other.gameObject.GetComponent<Damage>() || other.gameObject.GetComponent<Projectile>())
            {
                DropItemAndDestroyChest();
            }
        }
        else if (!EnemyManager.Instance.AllEnemiesDefeated())
        {
            if (other.gameObject.GetComponent<Damage>() || other.gameObject.GetComponent<Projectile>())
            {
                ResetDialogue();
                Dialogue.Instance.lines = new string[]
                {
                    "You must defeat all enemies before you can open this chest!", "Make sure no enemy is left behind."
                };
                Dialogue.Instance.StartDialogue();
            }
        }
    }

    private void DropItemAndDestroyChest()
    {
        GetComponent<ChestSpawner>().DropItems();
        Instantiate(destroyVFX, transform.position, Quaternion.identity);
        gameObject.SetActive(false); 
        itemDropped = true;
    }

    private void ResetDialogue()
    {
        if (Dialogue.Instance != null)
        {
            Dialogue.Instance.ResetDialogue();
        }
    }
}
