using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{

    private AudioSource finishSound;

    // Start is called before the first frame update
    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        finishSound.Play();

    }
}
