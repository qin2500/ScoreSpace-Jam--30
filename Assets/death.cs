using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death : MonoBehaviour
{
    [SerializeField]
    private float timeToDestroy = 5.0f;

    void Start()
    {
        // Call the Destroy method after timeToDestroy seconds
        Destroy(gameObject, timeToDestroy);
    }
}
