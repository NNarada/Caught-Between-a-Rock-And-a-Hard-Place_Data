using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController uIControllerInstance;

    public Slider healthSlider;
    public Slider sprintSlider;
    public Text healthText;
    public Text bulletText;
    public Text KeyText;
    public Image damgeScreen;
    public float damgeAlpha = 0.25f, damgeFadeSpeed = 2f;


    void Awake()
    {
        uIControllerInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(damgeScreen.color.a != 0)
        {
            damgeScreen.color = new Color(damgeScreen.color.r, damgeScreen.color.g, damgeScreen.color.b,
             Mathf.MoveTowards(damgeScreen.color.a, 0f, damgeFadeSpeed * Time.deltaTime));
        }
    }

    public void ShowDamge()
    {
        damgeScreen.color = new Color(damgeScreen.color.r, damgeScreen.color.g, damgeScreen.color.b, 0.25f);
    }
}
