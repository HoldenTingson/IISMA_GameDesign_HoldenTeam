using UnityEngine;
using UnityEngine.UI;
public class ChestInteraction : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue; 
    private bool isChestUnlocked = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isChestUnlocked && (other.gameObject.GetComponent<Damage>() || other.gameObject.GetComponent<Projectile>()))
        {
            if (!EnemyManager.Instance.AllEnemiesDefeated())
            {
                StartDialogue();
            }
            else
            {
                UnlockChest();
            }
        }
    }

    private void StartDialogue()
    {
        if (!isChestUnlocked)
        {
            ResetDialogue();  
            dialogue.StartDialogue(null); 
        }
    }

    private void UnlockChest()
    {
        if (isChestUnlocked) return;

        isChestUnlocked = true; 
    }

    private void ResetDialogue()
    {
        if (dialogue != null)
        {
            Text dialogueText = dialogue.GetComponentInChildren<Text>();
            if (dialogueText != null)
            {
                dialogueText.text = "";
            }

            dialogue.ResetDialogue();
        }
    }
}
