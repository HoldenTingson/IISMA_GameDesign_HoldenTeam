using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
    public void back()
    {
        Debug.Log("back");
        SceneManager.LoadScene("Anisya_Kirana/Scenes/MainMenu");
    }
    
}