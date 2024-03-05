using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    //draining amount
    private float timeLeft = 1;
    [SerializeField]
    private float timeToDrain = 45f;
    [SerializeField]
    private float minTimeToDrain = 45f;
    [SerializeField]
    private float timeLossPerFish = .05f;
    [SerializeField]
    private float timeLossPerTrash = .1f;
    [SerializeField]
    private float timeForTrashCollecting = .5f;
    [SerializeField]
    private Gradient gradient = new Gradient();

    //lerping variables
    [SerializeField]
    private float dialLerpAmount = .5f;
    [SerializeField]
    private float fillKillAmount = .05f;

    //score variables
    private int score;

    //different UI elements
    private Image timeCircle;
    private TextMeshProUGUI tmp;
    [SerializeField]
    private TextMeshProUGUI scoreTmp;

    //management variables
    private bool gameOver = false;




    // Start is called before the first frame update
    void Start()
    {
        timeCircle = GetComponentInChildren<Image>();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(PlayAmbientSound());
        //set intial time
        timeCircle.fillAmount = timeLeft;
        timeCircle.color = gradient.Evaluate(timeLeft);
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            DecreaseTime();
        }
    }

    IEnumerator PlayAmbientSound()
    {
        //AudioManager.Instance.Play("Forest");
        yield return new WaitForSecondsRealtime(30);
        //AudioManager.Instance.Stop("Forest", true);
        //AudioManager.Instance.Play("City");
    }
    // ===================================== //
    // ===== LISTENING/EVENT FUNCTIONS ===== //
    // ===================================== //

    private void OnEnable()
    {
        FishingGameManager.collectFish += IncreaseScore;
        FishingGameManager.collectTrash += AddTrashTime;
    }

    private void OnDisable()
    {
        FishingGameManager.collectFish -= IncreaseScore;
        FishingGameManager.collectTrash -= AddTrashTime;
    }

    private void EndGame()
    {
        gameOver = true;
        timeCircle.fillAmount = 0;
        FishingGameManager.OnGameOver();
        //AudioManager.Instance.Play("GameOver");
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);

        scoreTmp.text = "You caught " + score + " fish!";
    }


    // ===================================== //
    // =========== DIAL FUNCTIONS ========== //
    // ===================================== //

    private void DecreaseTime()
    {
        timeLeft -= 1/timeToDrain * Time.deltaTime;
        timeLeft = timeLeft < 0 ? 0: timeLeft; // clamp value

        timeCircle.fillAmount = Mathf.Lerp(timeCircle.fillAmount,
                                           Mathf.Clamp(timeLeft, 0, 1),
                                           dialLerpAmount * Time.deltaTime);
        timeCircle.color = gradient.Evaluate(timeCircle.fillAmount);

        if(timeCircle.fillAmount < fillKillAmount)
        {
            EndGame();
        }
    }

    private void ChangeDrainAmount(float timeLoss)
    {
        timeToDrain -= timeLoss;
        timeToDrain = Mathf.Clamp(timeToDrain, minTimeToDrain, timeToDrain + 1);
    }

    public void AddTrashTime()
    {
        timeLeft += timeForTrashCollecting;
        ChangeDrainAmount(timeLossPerTrash);
    }



    // ===================================== //
    // ========= SCORING FUNCTIONS ========= //
    // ===================================== //

    public void IncreaseScore()
    {
        //increase score text
        score++;
        tmp.text = "x" + score;

        //reset time
        timeLeft = 1 + 1/timeToDrain; // over shoot on purpose

        ChangeDrainAmount(timeLossPerFish);
    }

    // ===================================== //
    // ========= BUTTONS FUNCTIONS ========= //
    // ===================================== //

    public void ResetGame()
    {
        Debug.Log("clicked reset game");
        //AudioManager.Instance.Stop("GameOver");
        SceneManager.LoadSceneAsync(1);
    }

    public void GoBackToMainMenu()
    {
        Debug.Log("clicked main menun");
        SceneManager.LoadSceneAsync(0);
    }

}
