using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUI : MonoBehaviour
{
    //draining amount
    [SerializeField]
    private float DrainAmount = .1f;

    //score variables
    private int score;

    //different UI elements
    private Image timeCircle;
    private TextMeshProUGUI tmp;




    // Start is called before the first frame update
    void Start()
    {
        timeCircle.GetComponentInChildren<Image>().color = Color.white;
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseTime();
    }

    private void DecreaseTime()
    {
        float currentTimeAmount = timeCircle.fillAmount;

        currentTimeAmount = currentTimeAmount - DrainAmount * Time.deltaTime;
        currentTimeAmount = currentTimeAmount < 0 ? 0: currentTimeAmount; // clamp value

        timeCircle.fillAmount = currentTimeAmount;

    }

    private void IncreaseScore()
    {
        score++;
        tmp.text = "X " + score;
    }
}
