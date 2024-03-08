using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerZone : MonoBehaviour
{
    //create a variable to keep track of
    //whether th trigger zone is active

    bool active = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // if the trigger zone is active...
        if (active && collision.gameObject.tag == "Player")
        {
            //deactivate the trigger zone
            active = false;



            //Add 1 sto the core
            // when the player enters the trigger zone
            ScoreManager.score++;
            gameObject.SetActive(false);

        }
    }

}