using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class spiketrigger : MonoBehaviour
{
    //set this reference in the inspector
    public TMP_Text output;

    //enter text you want to display
    public string textToDisplay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Triggered by " + collision.gameObject.name);

        //set the player tag on the player in the inspector
        if (collision.gameObject.tag == "Player")
        {

            ScoreManager.gameOver = true;

            //display textToDisplay on screen
            output.text = textToDisplay;

        }

    }

}
