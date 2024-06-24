using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    // Start is called before the first frame update

<<<<<<< HEAD
    [SerializeField] GameObject main;
    //[SerializeField] GameObject levelSelect;
    //[SerializeField] GameObject credits;
=======
    [SerializeField] GameObject canvas;
>>>>>>> cb1fb91b87be85ae9616a449223ff5d896e7a540

    GameObject currentlyActive;

    private void Awake()
    {
        currentlyActive = main;
    }
    public void Play()
    {
        GlobalEvents.FullPlaythroughInProgress.invoke();
        SceneManager.LoadSceneAsync("NameSelector", mode: LoadSceneMode.Additive);
<<<<<<< HEAD
        currentlyActive.SetActive(false);
        
=======
        hideAssets();
>>>>>>> cb1fb91b87be85ae9616a449223ff5d896e7a540
    }

    public void levelSelector()
    {
        GlobalEvents.FullPlaythroughInProgress.uninvoke();
        SceneManager.LoadSceneAsync("NameSelector", mode: LoadSceneMode.Additive);
<<<<<<< HEAD
        currentlyActive.SetActive(false);

=======
        hideAssets();
>>>>>>> cb1fb91b87be85ae9616a449223ff5d896e7a540
    }

    public void loadCredits()
    {
<<<<<<< HEAD
        currentlyActive.SetActive(false);
=======
        hideAssets();
>>>>>>> cb1fb91b87be85ae9616a449223ff5d896e7a540
        SceneManager.LoadSceneAsync("Credits", mode: LoadSceneMode.Additive);
    }

    public void loadLeaderBoard()
    {
        hideAssets();
        SceneManager.LoadSceneAsync(SceneNames.LEADERBOARD, mode: LoadSceneMode.Additive);

    }

    private void hideAssets()
    {
        canvas.SetActive(false);
    }
}
