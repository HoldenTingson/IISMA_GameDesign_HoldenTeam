using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
    public void back()
    {
        Debug.Log("Returning to Main Menu...");
        SceneManager.LoadScene("MainMenu"); // Use the scene's name as listed in Build Settings
    }
}