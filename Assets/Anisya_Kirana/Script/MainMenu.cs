using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }

        GameObject ui = GameObject.FindGameObjectWithTag("UI");
        if (ui != null)
        {
            Destroy(ui);
        }
    }
    public void startGame()
    {
        Debug.Log("startGame");
        SceneManager.LoadScene("Tingson_Holden/Scenes/Level 1");
    }

    public void exit()
    {
        Application.Quit();
    }
}