using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public GameObject creditsPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnOpenCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void OnCloseCredits()
    {
        creditsPanel.SetActive(false);
    }
}
