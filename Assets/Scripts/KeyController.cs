using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public static int keyCounter = 0;
    public DoorController lockedDoorInstance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UIController.uIControllerInstance.KeyText.text = keyCounter + "/7";

        if(keyCounter >= 7)
        {
            lockedDoorInstance.isLockedDoor = false;
        }
    }
}
