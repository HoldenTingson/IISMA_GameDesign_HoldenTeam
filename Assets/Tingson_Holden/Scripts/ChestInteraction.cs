using UnityEngine;
using UnityEngine.UI; // For accessing UI Text if needed

public class ChestInteraction : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue; // Reference to the Dialogue script
    private bool isChestUnlocked = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ensure that only the player's weapon or projectile triggers the interaction
        if (!isChestUnlocked && (other.gameObject.GetComponent<Damage>() || other.gameObject.GetComponent<Projectile>()))
        {
            // If there are still enemies alive, show dialogue. If no enemies are left, unlock the chest
            if (!EnemyManager.Instance.AllEnemiesDefeated())
            {
                StartDialogue();
            }
            else
            {
                UnlockChest(); // Unlock chest immediately if no enemies are left
            }
        }
    }

    private void StartDialogue()
    {
        if (!isChestUnlocked)
        {
            ResetDialogue();  // Reset any existing dialogue
            dialogue.StartDialogue(null); // Start the dialogue sequence
        }
    }

    private void UnlockChest()
    {
        if (isChestUnlocked) return; // Don't do anything if already unlocked


        isChestUnlocked = true; // Mark the chest as unlocked
        Debug.Log("The chest is now unlocked!");

        // You can add additional logic here, such as animating the chest opening or spawning items
    }

    // Function to reset dialogue status
    private void ResetDialogue()
    {
        // Reset the dialogue text if you're using Unity's UI Text
        if (dialogue != null)
        {
            Text dialogueText = dialogue.GetComponentInChildren<Text>(); // Get the Text component from inside the dialogue
            if (dialogueText != null)
            {
                dialogueText.text = ""; // Clear the text
            }

            dialogue.ResetDialogue(); // Call ResetDialogue on the Dialogue object to reset its status
        }
    }
}
