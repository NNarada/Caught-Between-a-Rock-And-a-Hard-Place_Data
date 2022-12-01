using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EscapeMenu : MonoBehaviour
{
    public static bool pressedContinue = false;
    public Slider mouseSensSlider;
    public Slider volumeSlider;


    public void SetMouseSens()
    {
        //Debug.Log("MouseSnese Before: " + PlayerController.playerControllerInstance.mouseSensitivity);
        PlayerController.mouseSensitivity = mouseSensSlider.value;
        //Debug.Log("MouseSnese After: " + PlayerController.playerControllerInstance.mouseSensitivity);
        
    }
    public void updateVolume()
    {
        MusicController.musicControllerInstance.musicVolume = volumeSlider.value;
    }
    public void OptionsButton()
    {
        GameManager.optionsPanel.SetActive(true);
        GameManager.escapeMenuPanel.SetActive(false);
    }

    public void ContinueButton()
    {
        GameManager.escapeMenuPanel.SetActive(false);
        pressedContinue = true;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ControlesButton()
    {
        GameManager.controlesPanel.SetActive(true);
        GameManager.optionsPanel.SetActive(false);
    }

    public void AudioButton()
    {
        GameManager.audioPanel.SetActive(true);
        GameManager.optionsPanel.SetActive(false);
    }

    public void OptionsBackButton()
    {
        GameManager.optionsPanel.SetActive(false);
        GameManager.escapeMenuPanel.SetActive(true);
    }

    public void MouseSensButton()
    {
        GameManager.mouseSensPanel.SetActive(true);
        GameManager.optionsPanel.SetActive(false);
    }

    public void AudioBackButton()
    {
        GameManager.audioPanel.SetActive(false);
        GameManager.optionsPanel.SetActive(true);
    }

    public void MouseSensBackButton()
    {
        GameManager.mouseSensPanel.SetActive(false);
        GameManager.optionsPanel.SetActive(true);
    }

    public void ControlBackButton()
    {
        GameManager.controlesPanel.SetActive(false);
        GameManager.optionsPanel.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
