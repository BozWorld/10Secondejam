using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameUIManager : MonoBehaviour
{
    public static MinigameUIManager Singleton;
    public GameObject EndingUI, LosingUI;
    public TextMeshProUGUI TimerText;

    private bool LowerTime;
    private float Timer;
    private GameObject[] allDestroyables;

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
    }

    private void Update()
    {
        if(LowerTime)
        {
            Timer -= Time.deltaTime;
            TimerText.SetText(Mathf.Round(Timer).ToString() + " Seconds");
            if (Timer <= 0)
            {
                LosingUI.SetActive(true);
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
        EndingUI.SetActive(true);
        LowerTime = false;
    }

    public void ResetAll()
    {
        Timer = 10f;
        LowerTime = true;
        LosingUI.SetActive(false);
        foreach (GameObject gm in allDestroyables)
        {
            gm.SetActive(true);
        }
    }
}
