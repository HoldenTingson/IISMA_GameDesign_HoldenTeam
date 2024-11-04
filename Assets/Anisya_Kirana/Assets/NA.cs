using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NA : MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {
    // }

public void gotToLevel1()
     {
        Debug.Log("gotToLevel1");
        SceneManager.LoadScene("Level 1_Test");
     }

}
