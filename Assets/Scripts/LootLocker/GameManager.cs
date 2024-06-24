using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        LootLockerSDKManager.Init("dev_170057ab23ba40ea9470fae2d724b339", "1.0", "furzi6jl");
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session:\n" + response.errorData);
                Debug.Log(response.errorData.code);

                return;
            }

            Debug.Log("successfully started LootLocker session");
        });

        SceneManager.LoadSceneAsync("MainMenu", mode: LoadSceneMode.Additive);
    }
}
