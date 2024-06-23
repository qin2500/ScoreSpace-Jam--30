using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject playButton;
    [SerializeField] GameObject creditsButton;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        GlobalEvents.FullPlaythroughInProgress.invoke();
        SceneManager.LoadSceneAsync("NameSelector", mode: LoadSceneMode.Additive);
        playButton.SetActive(false);
        creditsButton.SetActive(false);
    }

    public void levelSelector()
    {
        GlobalEvents.FullPlaythroughInProgress.uninvoke();
        SceneManager.LoadSceneAsync("NameSelector", mode: LoadSceneMode.Additive);
        playButton.SetActive(false);
        creditsButton.SetActive(false);
    }

    public void loadCredits()
    {
        playButton.SetActive(false);
        creditsButton.SetActive(false);
        SceneManager.LoadSceneAsync("Credits", mode: LoadSceneMode.Additive);
    }
}
