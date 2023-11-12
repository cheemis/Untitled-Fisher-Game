using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollectable : Collectable
{
    public AudioClip[] sounds;
    private AudioSource audioSource;
    Animator anim;

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
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hook")
        {
            Hook hook = other.gameObject.GetComponent<Hook>();
            if (!hook.HasCaughtFish())
            {
                UpdateLockedTarget(other.gameObject.transform);
                hook.SetCurrentHookTarget(this);
                this.GetComponent<Collider>().enabled = false;

                anim.SetTrigger("caught");
            }
        }

        Debug.Log("child trigger enter");
    }

    public override void UpdateLockedTarget(Transform newLockedTarget)
    {
        if (hooked)
        {
            AudioManager.Instance.Play("CollectFish");
        }
        base.UpdateLockedTarget(newLockedTarget);

    }
}
