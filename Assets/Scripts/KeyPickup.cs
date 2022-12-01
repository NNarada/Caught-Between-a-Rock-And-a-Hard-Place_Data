using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            KeyController.keyCounter++;
            Destroy(gameObject);
        }
    }
}
