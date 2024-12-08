using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;
    [SerializeField] private Dialogue dialogue; // Reference to the Dialogue script
    [SerializeField] private GameObject chestObject;
    private float waitToLoadTime = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            // Check if enemies are still alive or if chest is not destroyed
            if (EnemyManager.Instance.GetEnemyCount() > 0 || (chestObject != null && chestObject.activeInHierarchy))
            {
                // If conditions are not met, show the dialogue
                ResetDialogue();
                dialogue.StartDialogue();
            }
            else
            {
                // All conditions are met, proceed to the next scene
                ProceedToNextScene();
            }
        }
    }

    private void ResetDialogue()
    {
        // Reset the dialogue text if necessary
        if (dialogue != null)
        {
            dialogue.ResetDialogue(); // Call the ResetDialogue method to clear and reset dialogue status
        }
    }

    private void ProceedToNextScene()
    {
        Debug.Log("All conditions met. Proceeding to the next scene...");
        SceneManagement.Instance.SetTransitionName(sceneTransitionName);
        StartCoroutine(LoadSceneRoutine());
    }

    private IEnumerator LoadSceneRoutine()
    {
        yield return new WaitForSeconds(waitToLoadTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
