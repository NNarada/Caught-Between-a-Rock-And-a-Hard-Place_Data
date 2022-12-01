using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController playerHealthControllerInstance;

    [SerializeField] private int maxHealth = 50, currentHealth;

    void Awake()
    {
        playerHealthControllerInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UIController.uIControllerInstance.healthSlider.maxValue = maxHealth;
        UIController.uIControllerInstance.healthSlider.value = currentHealth;
        UIController.uIControllerInstance.healthText.text = "HP: " + currentHealth + "/" + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamgePlayer(int dmg)
    {
        currentHealth -= dmg;
        UIController.uIControllerInstance.ShowDamge();
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
            currentHealth = 0;
            GameManager.gameManagerInstance.PlayerDied();
        }

        UIController.uIControllerInstance.healthSlider.value = currentHealth;
        UIController.uIControllerInstance.healthText.text = "HP: " + currentHealth + "/" + maxHealth;
    }

    public void HealPlayer(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UIController.uIControllerInstance.healthSlider.value = currentHealth;
        UIController.uIControllerInstance.healthText.text = "HP: " + currentHealth + "/" + maxHealth;
    }
    
}
