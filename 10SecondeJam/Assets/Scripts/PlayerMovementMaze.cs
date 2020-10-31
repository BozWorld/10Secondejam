using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementMaze : MonoBehaviour
{
    public float WalkSpeed;
    private float HorWalk, VertWalk;
    private GameObject[] NearbyWalls;
    private bool MinigameDone;
    private Vector3 Position;

    private void Start()
    {
        NearbyWalls = new GameObject[] { gameObject };
        //Sets the first in the list as its own game object, to not have an empty array

        MinigameDone = false;
        //Makes sure we can play the minigame

        Position = transform.position;
    }
    
    void Update()
    {
        if (MinigameDone || !MinigameUIManager.Singleton.Lost() && !MinigameDone)
        {
            return;
        }
        //Stops us from doing anything other than clicking continue

        //Movement
        HorWalk = Input.GetAxis("Horizontal") * WalkSpeed * Time.deltaTime;
        VertWalk = Input.GetAxis("Vertical") * WalkSpeed * Time.deltaTime;
        transform.Translate(HorWalk, VertWalk, 0);

        //Bomb
        if (Input.GetButtonDown("Fire1") && NearbyWalls[0] != gameObject)
            //Checks if there is a wall in the array
        {
            int i = 0;
            //Will help us check which point on the list we are on
            foreach (GameObject g in NearbyWalls)
            {
                g.SetActive(false);
                //Destroy the wall
                if (i == 0)
                {

                    NearbyWalls[0] = gameObject;
                    //Resets 0 the same as the start
                }
                else
                {
                    NearbyWalls[i] = null;
                    //Removes it from the list
                }
                i++;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Breakable"))
            //Check if the wall is breakable
        {
            if (NearbyWalls[0] == gameObject)
                //AKA when there is no walls in the array
            {
                NearbyWalls[0] = collision.transform.gameObject;
                //Replaces itself with the first wall
            }
            else
            {
                NearbyWalls[NearbyWalls.Length] = collision.transform.gameObject;
                //Adds a wall in the array
            }
        }
        if(collision.transform.gameObject.layer == 8)
            //Check if we grab some sand for the time
        {
            MinigameUIManager.Singleton.ShowEnding();
            MinigameDone = true;
            Destroy(collision.transform.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Breakable"))
        {
            if (NearbyWalls[0] != gameObject && NearbyWalls.Length == 1)
                //AKA if we have a wall in the 0 of the array
            {
                NearbyWalls[0] = gameObject;
                //Resets 0 the same as the start
            }
            else
            {
                NearbyWalls[NearbyWalls.Length - 1] = null;
                //Removes from the array
            }
        }
    }

    public void ResetPos()
    {
        transform.position = Position;
        MinigameDone = false;
        MinigameUIManager.Singleton.ResetAll();
    }
}
