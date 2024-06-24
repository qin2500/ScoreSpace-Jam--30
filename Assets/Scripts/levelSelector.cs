using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class levelSelector : MonoBehaviour
{

    [SerializeField] private TMP_Text username;

    private void Start()
    {
        username.text = GlobalReferences.PLAYER.Username;
    }
    public void selectLevel(int levelNumber) 
    {
        
        if (levelNumber == 0)
        {
            throw new System.Exception("Tried to select level 0");
        }
        Debug.Log(levelNumber);
        SceneManager.LoadSceneAsync(SceneNames.LEVELCONTROLLER, mode: LoadSceneMode.Additive).completed += (asyncOperation) =>
        {
            GlobalReferences.LEVELMANAGER.setLevel(levelNumber);
            SceneManager.UnloadSceneAsync("LevelSelector");
            SceneManager.UnloadSceneAsync("MainMenu");
        };
        
    }

    public void mainMenu()
    {
        SceneManager.UnloadSceneAsync(SceneNames.MAINMENU);
        SceneManager.UnloadSceneAsync(SceneNames.LEVELSELECTOR);
        SceneManager.LoadSceneAsync("MainMenu", mode: LoadSceneMode.Additive);
    }
}
