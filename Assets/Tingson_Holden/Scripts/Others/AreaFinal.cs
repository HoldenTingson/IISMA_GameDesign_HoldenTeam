using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AreaFinal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            // Check if enemies are still alive or if chest is not destroyed
            if (!EnemyManager.Instance.AllEnemiesDefeated())
            {
                // If conditions are not met, show the dialogue
                ResetDialogue();
                Dialogue.Instance.lines = new string[]
                    { "You must kill all the ghost before you can proceed!", "Try to find and defeat the ghosts." };
                Dialogue.Instance.StartDialogue();
            }
            else
            {
              PortalBlock.Instance.OpenPortal();
            }
        }
    }

    private void ResetDialogue()
    {
        // Reset the dialogue text if necessary
        if (Dialogue.Instance != null)
        {
            Dialogue.Instance.ResetDialogue(); // Call the ResetDialogue method to clear and reset dialogue status
        }
    }

 
}
