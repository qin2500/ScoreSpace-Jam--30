using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setStartPosition : MonoBehaviour
{

    void Start()
    {
        GlobalReferences.PLAYER.startPosition = transform.position;
    }

}
