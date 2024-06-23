using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GlobalReferences
{
    private static GameManager gameManager;
    private static LevelManager levelManager;

    public static GameManager GAMEMANAGER {  get { return gameManager; } set { gameManager = value; } }
    public static LevelManager LEVELMANAGER { get { return levelManager; } set { levelManager = value; } }

}
