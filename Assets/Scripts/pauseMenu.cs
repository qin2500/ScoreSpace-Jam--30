using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    private SoundMixerManager soundMixerManager = GlobalReferences.SOUNDMIXERMANAGER;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resume()
    {
        SceneManager.UnloadSceneAsync("PauseMenu");
        GlobalEvents.PlayerPause.uninvoke();
    }
}
