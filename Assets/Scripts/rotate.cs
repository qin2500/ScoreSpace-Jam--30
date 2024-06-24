using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{

    public float rotationSpeed = 90f;

    void Update()
    {
        if(!GlobalEvents.PlayerPause.Invoked())
            // Rotate the object around its Z axis at a constant speed
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
