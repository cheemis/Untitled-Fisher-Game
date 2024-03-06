using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public GameObject creditsPanel;
    public GameObject tutorialPanel;
    public GameObject titleScreen;
    // Start is called before the first frame update
    void Start()
    {
        OnCloseCredits();
    }

    public void OnPlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void OnOpenCredits()
    {
        creditsPanel.SetActive(true);
        titleScreen.SetActive(false);
    }

    public void OnCloseCredits()
    {
        creditsPanel.SetActive(false);
        titleScreen.SetActive(true);
    }

    public void OnOpenTutorial()
    {
        tutorialPanel.SetActive(true);
    }
    public void OnCloseTutorial()
    {
        tutorialPanel.SetActive(false);
    }
}
