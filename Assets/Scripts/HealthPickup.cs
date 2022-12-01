using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 10;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerHealthController.playerHealthControllerInstance.HealPlayer(healAmount);
            Destroy(gameObject);
        }
    }
}
