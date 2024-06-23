using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject main;
    //[SerializeField] GameObject levelSelect;
    //[SerializeField] GameObject credits;

    GameObject currentlyActive;

    private void Awake()
    {
        currentlyActive = main;
    }
    public void Play()
    {
        GlobalEvents.FullPlaythroughInProgress.invoke();
        SceneManager.LoadSceneAsync("NameSelector", mode: LoadSceneMode.Additive);
        currentlyActive.SetActive(false);
        
    }

    public void levelSelector()
    {
        GlobalEvents.FullPlaythroughInProgress.uninvoke();
        SceneManager.LoadSceneAsync("NameSelector", mode: LoadSceneMode.Additive);
        currentlyActive.SetActive(false);

    }

    public void loadCredits()
    {
        currentlyActive.SetActive(false);
        SceneManager.LoadSceneAsync("Credits", mode: LoadSceneMode.Additive);
    }
}
