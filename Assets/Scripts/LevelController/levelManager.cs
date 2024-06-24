using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private int _level = 0;
    private bool _timerActive = false;
    private float _currentTime;
    [SerializeField] private TMP_Text _text;


    private void Awake()
    {
        GlobalReferences.LEVELMANAGER = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //reset globals

        GlobalEvents.PlayerPause.uninvoke();
        GlobalEvents.PlayerDeath.uninvoke();
        GlobalEvents.LevelComplete.uninvoke();
        GlobalEvents.PlayerStartedMoving.uninvoke();
        //GlobalEvents.FullPlaythroughInProgress.uninvoke();
    }

    // Update is called once per frame
    void Update()
    {

        if (GlobalEvents.PlayerStartedMoving.Invoked() && !GlobalEvents.PlayerPause.Invoked())
        {
            _timerActive = true;
        }


        if (GlobalEvents.PlayerDeath.Invoked())
        {
            restartLevel();
            GlobalEvents.PlayerDeath.uninvoke();
            GlobalEvents.LevelComplete.uninvoke();
            return;
        }

        if (GlobalEvents.LevelComplete.Invoked())
        {
            completeLevel();
            GlobalEvents.LevelComplete.uninvoke();
            return;
        }


        if (Input.GetKeyDown(KeyCode.R) && !GlobalEvents.PlayerPause.Invoked())
        {
            restartLevel();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (GlobalEvents.PlayerPause.Invoked()) togglePauseMenu();
            loadMainMenu();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            //pause button
            togglePauseMenu();

        }

        if (_timerActive)
        {
            _currentTime += Time.deltaTime;
        }

        TimeSpan time = TimeSpan.FromSeconds(_currentTime);
        _text.text = time.Minutes + ":" + time.Seconds + ":" + time.Milliseconds;

        
    }

    public void setLevel(int level)
    {

        if (level == 0)
        {
            Debug.Log("Some bozo tried to set the level to 0");
            throw new Exception("level cannot be set to 0");
        }

        unloadLevel();

        this._level = level;
        Debug.Log("Loading level: " + this._level);
        SceneManager.LoadSceneAsync("Level " + this._level, mode: LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Player Controller", mode: LoadSceneMode.Additive);
        Debug.Log("Loaded level: " + this._level);

        resetTimer();

    }

    private void unloadLevel()
    {
        Debug.Log("unload level called for level: " + this._level);
        if (this._level > 0)
        {
            Debug.Log("Unloading level: " + this._level);
            SceneManager.UnloadSceneAsync("Level " +this._level);
            SceneManager.UnloadSceneAsync("Player Controller");
        }

    }

    public void incrementLevel() { setLevel(this._level + 1); }

    public void restartLevel()
    {
        setLevel(this._level);
    }

    public void completeLevel() 
    {
        //stop counting time and turn it into score then add to leaderboard

        
        
        pauseTimer();
        int score = LeaderBoardGateway.convertTimestampToScore(_currentTime);

        Debug.Log("Level completed, score is: " + score);

        // if single level then add to leaderboard for level
        if (!GlobalEvents.FullPlaythroughInProgress.Invoked())
        {
            Debug.Log("Currnet Level: " + this._level);
            LeaderBoardGateway.SubmitScore(levelString(), GlobalReferences.PLAYER.Username, score);
            SceneManager.UnloadSceneAsync("LevelController");
            loadMainMenu();
            return;
        }

        //if full playthrough add to overall leaderboard
        LeaderBoardGateway.SubmitScore(levelString(), GlobalReferences.PLAYER.Username, score);
        GlobalReferences.PLAYER.Score += score;

        if (this._level == GlobalReferences.NUMBEROFLEVELS)
        {
            LeaderBoardGateway.SubmitScore("fullPlaythrough", GlobalReferences.PLAYER.Username, GlobalReferences.PLAYER.Score);
            loadMainMenu();
            Debug.Log("Currnet Level: " + this._level);
            return;
        }
        incrementLevel(); 
    }

    public void pauseTimer() { _timerActive = false; }
    public void unpauseTimer() {  _timerActive = true; }

    public void resetTimer()
    {
        _currentTime = 0;
        GlobalEvents.PlayerStartedMoving.uninvoke();
    }

    public bool isTimerPaused() { return !_timerActive; }

    public void loadMainMenu()
    {
        unloadLevel();
        SceneManager.LoadSceneAsync("MainMenu", mode: LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("LevelController");
        
    }

    public void togglePauseMenu()
    {
        if (GlobalEvents.PlayerPause.Invoked())
        {
            if (GlobalEvents.PlayerStartedMoving.Invoked())
                unpauseTimer();
            SceneManager.UnloadSceneAsync("PauseMenu");
            GlobalEvents.PlayerPause.uninvoke();
        }
        else
        {
            pauseTimer();
            SceneManager.LoadSceneAsync("PauseMenu", mode:LoadSceneMode.Additive);
            GlobalEvents.PlayerPause.invoke();
        }
    }

    private string levelString()
    {
        return "level" + this._level;
    }
}
