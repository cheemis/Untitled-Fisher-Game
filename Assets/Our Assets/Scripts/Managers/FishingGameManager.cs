using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FishingGameManager : MonoBehaviour
{

    //collecting A fish
    public static event UnityAction collectFish;
    public static void OnCollectFish() => collectFish?.Invoke();


    //Game over condition
    public static event UnityAction gameOver;
    public static void OnGameOver() => gameOver?.Invoke();


}
