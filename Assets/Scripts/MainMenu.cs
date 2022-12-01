using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject creditsPanel;
    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        KeyController.keyCounter = 0;
    }

    public void ControlsButton()
    {
        Debug.Log("Controles Button pressed");
        controlsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void CreditsButton()
    {
        creditsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ControlsBackButton()
    {
        controlsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }



    public void CreditsBackButton()
    {
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
