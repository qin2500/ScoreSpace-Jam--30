using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    private SoundMixerManager soundMixerManager = GlobalReferences.SOUNDMIXERMANAGER;


    public void changeMasterVolume(float val){
        soundMixerManager.SetMasterVolume(val);
    }
    public void changeSFXVolume(float val){
        soundMixerManager.SetMasterVolume(val);
    }
    public void changeMusicVolume(float val){
        soundMixerManager.SetMasterVolume(val);
    }


    public void resume()
    {
        SceneManager.UnloadSceneAsync("PauseMenu");
        GlobalEvents.PlayerPause.uninvoke();
    }

    public void quit()
    {
        SceneManager.UnloadSceneAsync("PauseMenu");
        GlobalEvents.PlayerPause.uninvoke();
        GlobalReferences.LEVELMANAGER.loadMainMenu();
    }
}
