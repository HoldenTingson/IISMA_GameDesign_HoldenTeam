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
            if (!EnemyManager.Instance.AllEnemiesDefeated())
            {
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
        if (Dialogue.Instance != null)
        {
            Dialogue.Instance.ResetDialogue(); 
        }
    }

 
}
