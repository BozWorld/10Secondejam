using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfTheWorld : MonoBehaviour
{ 
    void Start()
    {
        StartCoroutine(KillTheWorld());
    }

    private IEnumerator KillTheWorld()
    {
        yield return new WaitForSeconds(6);
        Application.Quit();
    }
    
}
