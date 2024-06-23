using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GlobalEvents
{
    private static Event _playerDeath = new("player death", "indicates if player has died on current level");
    private static Event _levelComplete = new("level complete", "indicates if player has completed current level");
    private static Event _playerPause = new("pause", "indicates if player has paused the game");
    private static Event _playerStartedMoving = new("player started moving", "indicates if player has started moving");

    public static Event PlayerDeath { get { return _playerDeath; }}
    public static Event PlayerPause { get { return _playerPause; } }
    public static Event LevelComplete { get { return _levelComplete; }}
    public static Event PlayerStartedMoving { get { return _playerStartedMoving; } }

}

public class Event
{
    private bool invoked = false;

    private string name;


    private string description;

    public Event(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public string Name() { return name; }

    public string Description() { return description; }

    public bool Invoked() { return invoked; }
    public override string ToString() { return name; }

    public void invoke() { this.invoked = true; }

    public void uninvoke() { this.invoked = false; }

}
