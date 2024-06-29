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

    private void Start()
    {
        errorText.gameObject.SetActive(false);
    }
    public void selectName()
    {
        if (inputField != null)
        {

            inputField.text = inputField.text.Trim();

            if (!validateInput(inputField.text))
            {
                errorText.text = "Please use a between 3 and 10 characters long";
                errorText.gameObject.SetActive(true);
                return;
            }

            GlobalReferences.PLAYER.Username = inputField.text;
            Debug.Log("set name to: " + inputField.text);
            SceneManager.UnloadSceneAsync(SceneNames.NAMESELECTOR).completed += (asyncOperation) => 
            {
                if (GlobalEvents.FullPlaythroughInProgress.Invoked())
                {

                    SceneManager.LoadSceneAsync(SceneNames.LEVELCONTROLLER, mode: LoadSceneMode.Additive).completed += (asyncOperation) =>
                    {
                        Debug.Log("loaded level controller");
                        GlobalReferences.LEVELMANAGER.setLevel(1);
                        Debug.Log("set level");

                        SceneManager.UnloadSceneAsync(SceneNames.MAINMENU);
                    };
                    return;

                }
                SceneManager.LoadSceneAsync(SceneNames.LEVELSELECTOR, mode: LoadSceneMode.Additive);
            };
        }
    }

    private bool validateInput(string input)
    {
        if (input.Length > 10 || input.Length < 3) return false;
        return true;
    }

    public void mainMenu() 
    { 
        
    }
}
