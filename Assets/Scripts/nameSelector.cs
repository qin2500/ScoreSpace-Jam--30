using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nameSelector : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text errorText;
    // Start is called before the first frame update


    public void selectName() 
    {
        if (inputField != null)
        {

            if (validateInput(inputField.text))
            {
                errorText.text = "Please use a shorter name";
                return;
            }

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
            SceneManager.LoadSceneAsync(SceneNames.LEVELSELECTOR, mode:LoadSceneMode.Additive);

        }
    }

    private bool validateInput(string input)
    {
        if (input.Length > 10) return false;
        return true;
    }
}
