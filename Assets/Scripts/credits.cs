using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Credits : MonoBehaviour
{
    public void reopenMainMenu()
    {
        SceneManager.UnloadSceneAsync("MainMenu").completed += (asyncOperations) =>
        {
            SceneManager.LoadSceneAsync("MainMenu", mode: LoadSceneMode.Additive).completed += (asyncOperation) =>
            {
                SceneManager.UnloadSceneAsync("Credits");
            };
        };
        
    }
}
