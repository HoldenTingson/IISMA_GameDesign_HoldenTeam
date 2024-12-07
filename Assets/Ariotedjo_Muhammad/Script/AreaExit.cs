using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;
    [SerializeField] private Dialogue dialogue; // Reference to the Dialogue script
    [SerializeField] private GameObject chestObject;
    private float waitToLoadTime = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Pastikan player yang memasuki trigger
        if (other.gameObject.GetComponent<PlayerController>())
        {
            // Cek apakah musuh masih ada
            if (EnemyManager.Instance.GetEnemyCount() > 0)
            {
                dialogue.StartDialogue(PauseGameAndDisplay("There are still enemies. Defeat them to proceed."));
                Debug.Log("There are still enemies. Defeat them to proceed.");
            }
            else if (chestObject != null)
            {
                // Cek apakah chest sudah dihancurkan
                if (!chestObject.activeInHierarchy)
                {
                    Debug.Log("Chest is destroyed. Proceeding to next scene...");
                    SceneManagement.Instance.SetTransitionName(sceneTransitionName);
                    UIFade.Instance.FadeToBlack();
                    StartCoroutine(LoadSceneRoutine());
                }
                else
                {
                    dialogue.StartDialogue(PauseGameAndDisplay("You must destroy the chest before proceeding."));
                    Debug.Log("You must destroy the chest before proceeding.");
                }
            }
            else
            {
                Debug.Log("All conditions met. Proceeding to the next scene...");
                SceneManagement.Instance.SetTransitionName(sceneTransitionName);
                UIFade.Instance.FadeToBlack();
                StartCoroutine(LoadSceneRoutine());
            }
        }
    }

    private IEnumerator LoadSceneRoutine()
    {
        // Tunggu sebentar untuk transisi fade
        while (waitToLoadTime > 0)
        {
            waitToLoadTime -= Time.deltaTime;
            yield return null;
        }

        // Muat scene berikutnya
        SceneManager.LoadScene(sceneToLoad);
    }

    private System.Action PauseGameAndDisplay(string message)
    {
        return () =>
        {
            Debug.Log(message);
            Time.timeScale = 1f; // Resume time after dialog finishes
        };
    }
}
