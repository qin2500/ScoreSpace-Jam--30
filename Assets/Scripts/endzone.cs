using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endzone : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //hooray level complete
        Debug.Log("Collision with endzone detected. Level completed.");
        GlobalEvents.LevelComplete.invoke();
    }
}
