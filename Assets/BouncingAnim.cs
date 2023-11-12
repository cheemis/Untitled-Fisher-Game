using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingAnim : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        FishingGameManager.collectFish += StopBoundingHouse;
        FishingGameManager.collectTrash += StopBoundingHouse;
        FishingGameManager.gameOver += StopBoundingHouse;
        FishingGameManager.caughSomething += BouceHouse;
    }

    private void OnDisable()
    {
        FishingGameManager.collectFish -= StopBoundingHouse;
        FishingGameManager.collectTrash -= StopBoundingHouse;
        FishingGameManager.gameOver += StopBoundingHouse;
        FishingGameManager.caughSomething += BouceHouse;
    }


    public void BouceHouse()
    {
        anim.SetBool("hasFish", true);
    }

    public void StopBoundingHouse()
    {
        anim.SetBool("hasFish", false);
    }


}
