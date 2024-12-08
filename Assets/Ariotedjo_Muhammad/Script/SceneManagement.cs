using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagement : Singleton<SceneManagement>
{
    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }


    public string SceneTransitionName { get; private set; }

    public void SetTransitionName(string sceneTransitionName) {
        this.SceneTransitionName = sceneTransitionName;
    }
}

