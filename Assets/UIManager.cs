using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public GameObject creditsPanel;
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
}
