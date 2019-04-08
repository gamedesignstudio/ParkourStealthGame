using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    // put this in  playerContoller
    //create variable for respawn location
    //Vector3 respawnPoint;

    public void OnTriggerEnter(Collider collider)
    {
        //check if player collides with a checkpoint
        if(collider.tag.Equals("checkpoint"))
        {
            //get player's location when they pass the checkpoint and store it as the respawn location
            //respawnPoint = this.Vector3;

        }

        if(collider.tag.Equals("finish"))
        {
            //stop timer
            //store time on leader board
            //reset respawn to first location
            //reset timer
            //go to end screen 
        }
    }
}
