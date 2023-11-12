using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollectable : Collectable
{
    public AudioClip[] sounds;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {/*
        audioSource = GetComponent<AudioSource>();

        // play a random sound when spawned
        audioSource.clip = sounds[Random.Range(0, sounds.Length)];
        
        // randomize pitch and volume
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.volume = Random.Range(0.8f, 1.2f);

        // play the sound when fish spawns
        audioSource.Play();
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
