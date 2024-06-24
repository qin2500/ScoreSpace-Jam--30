using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nameSelector : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    // Start is called before the first frame update


    public void selectName() 
    {
        if (inputField != null)
        {
            GlobalReferences.PLAYER.Username = inputField.text;
            Debug.Log("set name to: " + inputField.text);
            SceneManager.UnloadSceneAsync(SceneNames.NAMESELECTOR);

            if (GlobalEvents.FullPlaythroughInProgress.Invoked())
            {

                SceneManager.LoadSceneAsync(SceneNames.LEVELCONTROLLER, mode: LoadSceneMode.Additive).completed += (asyncOperation) =>
                {
                    GlobalReferences.LEVELMANAGER.setLevel(1);
                };
                SceneManager.UnloadSceneAsync(SceneNames.MAINMENU);
                return;
            }
<<<<<<< HEAD
            //SceneManager.UnloadSceneAsync(SceneNames.MAINMENU);
=======
>>>>>>> cb1fb91b87be85ae9616a449223ff5d896e7a540
            SceneManager.LoadSceneAsync(SceneNames.LEVELSELECTOR, mode:LoadSceneMode.Additive);

        }
    }
}
