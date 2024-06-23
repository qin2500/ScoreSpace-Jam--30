using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GlobalReferences
{
    private static GameManager gameManager;
    private static LevelManager levelManager;
    private static GameObject startIndicator;

    public static GameManager GAMEMANAGER {  get { return gameManager; } set { gameManager = value; } }
    public static LevelManager LEVELMANAGER { get { return levelManager; } set { levelManager = value; } }

    public static GameObject STARTINDICATOR { get { return startIndicator; } set { startIndicator = value; } }
}
