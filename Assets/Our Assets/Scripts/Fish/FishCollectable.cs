using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class FishCollectable : Collectable
{
    Animator anim;

    StudioEventEmitter emitter;

    public GameObject[] effects;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //emitter = FMODAudioManager.Instance.InitializeFMODEventEmitter(FMODEvents.Instance.fishSwimming, gameObject);
        //emitter.OverrideAttenuation = true;
        //emitter.OverrideMaxDistance = 2;
        //emitter.Play();
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

                // hide the effects
                foreach (GameObject effect in effects)
                {
                    effect.SetActive(false);
                }
            }
        }
        //emitter.Stop();
        Debug.Log("child trigger enter");
    }

    public override void UpdateLockedTarget(Transform newLockedTarget)
    {
        if (hooked)
        {
            //AudioManager.Instance.Play("CollectFish");
        }
        base.UpdateLockedTarget(newLockedTarget);

    }
}
