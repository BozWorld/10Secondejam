using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;

    private bool HasHelp;

    private void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Singleton = this;
        }
    }

    public void SetHelp(bool newSet, int level)
    {
        HasHelp = newSet;
        MinigameUIManager.Singleton.StartGame(level);
    }

    public bool GetHelp()
    {
        return HasHelp;
    }
}
