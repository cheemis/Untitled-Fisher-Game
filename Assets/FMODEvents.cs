using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class FMODEvents : UnitySingleton<FMODEvents>
{
    [Header("Player SFX")]
    public EventReference shootPole;
    public EventReference motorBoat;

    [Header("House SFX")]
    public EventReference houseBouncing;

    [Header("Ambience")]
    public EventReference ambience;

}

