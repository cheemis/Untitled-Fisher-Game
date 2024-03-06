using FMODUnity;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FMODAudioManager : UnitySingleton<FMODAudioManager>
{
    public EventInstance ambienceEventInstance;
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateFMODEventInstance(EventReference eventRef)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventRef);
        return eventInstance;
    }

    public StudioEventEmitter InitializeFMODEventEmitter(EventReference eventRef, GameObject emitterObj)
    {
        StudioEventEmitter eventEmitter = emitterObj.GetComponent<StudioEventEmitter>();
        eventEmitter.EventReference = eventRef;
        return eventEmitter;

    }

    public void InitializeAmbience(EventReference eventRef)
    {
        ambienceEventInstance = RuntimeManager.CreateInstance(eventRef);
        ambienceEventInstance.start();
    }

    public void SetAmbienceParameter(string parameterName,float parameterValue)
    {
        ambienceEventInstance.setParameterByName(parameterName, parameterValue);
    }
}
