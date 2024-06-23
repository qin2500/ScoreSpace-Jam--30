using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class credits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reopenMainMenu()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadSceneAsync("MainMenu", mode: LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Credits");
    }
}
