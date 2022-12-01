using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;
    
    public static bool paused = false;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject escapeMenuUI;
    [SerializeField] private GameObject crosshair;

    private InputAction escapeAction;
    private bool escapeToggel = false;

    public static GameObject escapeMenuPanel;
    public static GameObject optionsPanel;
    public static GameObject controlesPanel;
    public static GameObject audioPanel;
    public static GameObject mouseSensPanel;
    void Awake() 
    {
        gameManagerInstance = this;
        escapeAction = playerInput.actions["Escape"];

        escapeMenuPanel = escapeMenuUI.transform.GetChild(0).gameObject;
        optionsPanel = escapeMenuUI.transform.GetChild(1).gameObject;
        audioPanel = escapeMenuUI.transform.GetChild(2).gameObject;
        mouseSensPanel = escapeMenuUI.transform.GetChild(3).gameObject;
        controlesPanel = escapeMenuUI.transform.GetChild(4).gameObject;

    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //escape menu 
        if(escapeAction.triggered && !escapeToggel)
        {
            Time.timeScale = 0;
            escapeToggel= true;
            paused = true;
            Cursor.lockState = CursorLockMode.None;
            escapeMenuPanel.SetActive(true);
            crosshair.SetActive(false);
        }
        else if((escapeAction.triggered && escapeToggel) || EscapeMenu.pressedContinue)
        {
            Time.timeScale = 1;
            escapeToggel = false;
            paused = false;
            Cursor.lockState = CursorLockMode.Locked;
            removeMenus();
            EscapeMenu.pressedContinue = false;
            crosshair.SetActive(true);
        }
    }

    public void PlayerDied()
    {
        KeyController.keyCounter = 0;
        StartCoroutine(PlayerDied_Aysnc());
    }

    public IEnumerator PlayerDied_Aysnc()
    {
        yield return new WaitForSeconds(3f);
        GunAmmo.reloding = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void removeMenus()
    {
        escapeMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        audioPanel.SetActive(false);
        mouseSensPanel.SetActive(false);
        controlesPanel.SetActive(false);
    }
}
