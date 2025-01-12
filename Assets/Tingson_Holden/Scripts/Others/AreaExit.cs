using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;
    [SerializeField] private GameObject chestObject;
    private float waitToLoadTime = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (EnemyManager.Instance.GetEnemyCount() > 0 || (chestObject != null && chestObject.activeInHierarchy))
            {
                ResetDialogue();
                Dialogue.Instance.lines = new string[]
                    { "You must open the chest before you can enter this portal!", "Try to find and open the chest." };
                Dialogue.Instance.StartDialogue();
            }
            else
            {
                ProceedToNextScene();
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

    private void ProceedToNextScene()
    {
        UIFade.Instance.FadeToBlack();
        SceneManagement.Instance.SetTransitionName(sceneTransitionName);
        StartCoroutine(LoadSceneRoutine());
    }

    private IEnumerator LoadSceneRoutine()
    {
        yield return new WaitForSeconds(waitToLoadTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
