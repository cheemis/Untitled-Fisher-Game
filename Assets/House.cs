using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class House : MonoBehaviour
{
    StudioEventEmitter emitter;
    // Start is called before the first frame update
    void Start()
    {
        emitter = FMODAudioManager.Instance.InitializeFMODEventEmitter(FMODEvents.Instance.houseBouncing, gameObject);
        emitter.OverrideAttenuation = true;
        emitter.OverrideMaxDistance = 0.5f;
        emitter.OverrideMinDistance = 0.1f;
        emitter.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
