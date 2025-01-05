using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{
    [SerializeField] private float _creditDuration = 20f;

    private void Start()
    {
        StartCoroutine(LoadMainMenuAfterCredits());
    }

    private IEnumerator LoadMainMenuAfterCredits()
    {
        yield return new WaitForSeconds(_creditDuration);
        SceneManager.LoadScene("MainMenu");
    }
}