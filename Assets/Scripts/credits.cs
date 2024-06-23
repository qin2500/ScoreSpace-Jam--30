using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class credits : MonoBehaviour
{

    public void reopenMainMenu()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadSceneAsync("MainMenu", mode: LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Credits");
    }
}
