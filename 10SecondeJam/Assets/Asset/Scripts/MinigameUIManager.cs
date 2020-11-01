using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameUIManager : MonoBehaviour
{
    public static MinigameUIManager Singleton;
    public GameObject[] EndingUI, LosingUI;
    public TextMeshProUGUI[] TimerText;

    private bool LowerTime, GameStarted;
    private float Timer;
    private GameObject[] allDestroyables;
    private int CurrentLevel;

    private void Awake()
    {
        if(Singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Singleton = this;
        }
    }

    private void Start()
    {
        Timer = 10f;
        LowerTime = true;
        allDestroyables = GameObject.FindGameObjectsWithTag("Breakable");
        GameStarted = false;
    }

    private void Update()
    {
        if(LowerTime && GameStarted)
        {
            Timer -= Time.deltaTime;
            TimerText[CurrentLevel].SetText(Mathf.Round(Timer).ToString() + " Seconds");

            if (Timer <= 0)
            {

                LosingUI[CurrentLevel].SetActive(true);
                LowerTime = false;
            }
        }
    }

    public bool Lost()
    {
        return LowerTime;
    }

    public void ShowEnding()
    {
        EndingUI[CurrentLevel].SetActive(true);
        LowerTime = false;
        GameStarted = false;
    }

    public void ResetAll()
    {
        Timer = 10f;
        LowerTime = true;
        LosingUI[CurrentLevel].SetActive(false);
        GameStarted = true;
        foreach (GameObject gm in allDestroyables)
        {
            gm.SetActive(true);
        }
    }

    public void StartGame(int LevelToPlay)
    {
        CurrentLevel = LevelToPlay;
        transform.GetChild(LevelToPlay).gameObject.SetActive(true);
        GameStarted = true;
    }
}
