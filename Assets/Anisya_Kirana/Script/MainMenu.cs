using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void startGame()
    {
        Debug.Log("startGame");
        SceneManager.LoadScene("Ibnu_Wiratomo/Levels/Level1Test");
    }

    public void setting()
    {
        Debug.Log("Almanac");
        SceneManager.LoadScene("Anisya_Kirana/Scenes/Almanac");
    }

    public void exit()
    {
        Application.Quit();
    }
}