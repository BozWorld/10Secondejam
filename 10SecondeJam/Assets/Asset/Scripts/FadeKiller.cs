using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeKiller : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }
}
