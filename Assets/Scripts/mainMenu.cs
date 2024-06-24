using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject canvas;

    GameObject currentlyActive;

    public void Play()
    {
        GlobalEvents.FullPlaythroughInProgress.invoke();
        SceneManager.LoadSceneAsync("NameSelector", mode: LoadSceneMode.Additive);
        
        hideAssets();
    }

    public void levelSelector()
    {
        GlobalEvents.FullPlaythroughInProgress.uninvoke();
        SceneManager.LoadSceneAsync("NameSelector", mode: LoadSceneMode.Additive);

        hideAssets();
    }

    public void loadCredits()
    {
        hideAssets();
        SceneManager.LoadSceneAsync(SceneNames.CREDITS, mode: LoadSceneMode.Additive);
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
