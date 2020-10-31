using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameUIManager : MonoBehaviour
{
    public static MinigameUIManager Singleton;
    public GameObject EndingUI;

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

    public void ShowEnding()
    {
        EndingUI.SetActive(true);
    }
}
