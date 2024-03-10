using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour

 {

    [SerializeField] private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collectionSoundEffect.Play();
        

    }
}
