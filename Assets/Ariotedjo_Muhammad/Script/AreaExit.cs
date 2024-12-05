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
            // Cek apakah musuh sudah dikalahkan
            if (EnemyManager.Instance.GetEnemyCount() > 0)
            {
                // Jika masih ada musuh, tampilkan dialog
                dialogue.StartDialogue();
                Debug.Log("There are still enemies. Defeat them to proceed.");
            }
            else if (chestObject != null)
            {
                // Cek apakah chest sudah dihancurkan (misal dengan mengecek apakah chestObject sudah tidak aktif)
                if (!chestObject.activeInHierarchy)
                {
                    Debug.Log("Chest is destroyed. Proceeding to next scene...");// Jika semua musuh sudah dikalahkan, lakukan transisi scene
                    SceneManagement.Instance.SetTransitionName(sceneTransitionName);
                    UIFade.Instance.FadeToBlack();
                    StartCoroutine(LoadSceneRoutine());
                }
                else
                {
                    dialogue.StartDialogue();
                    Debug.Log("You must destroy the chest before proceeding.");
                }
            }
        }
    }

    private IEnumerator LoadSceneRoutine()
    {
        // Tunggu sebentar untuk transisi fade
        while (waitToLoadTime >= 0)
        {
            waitToLoadTime -= Time.deltaTime;
            yield return null;
        }

        // Muat scene berikutnya
        SceneManager.LoadScene(sceneToLoad);
    }

}
