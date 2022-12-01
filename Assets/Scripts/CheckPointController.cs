using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointController : MonoBehaviour
{
    public static CheckPointController checkpointInstance;
    public string checkPointName; 
    public Transform cpTransform;

    void Start()
    {
        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_cp"))
        {
            if(PlayerPrefs.GetString(SceneManager.GetActiveScene().name + "_cp") == checkPointName)
            {
                PlayerController.playerControllerInstance.GetComponent<CharacterController>().enabled = false;
                PlayerController.playerControllerInstance.transform.position = transform.position;
                PlayerController.playerControllerInstance.GetComponent<CharacterController>().enabled = true;
                //Debug.Log("Player respawn at:" + checkPointName);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", checkPointName);
            //Debug.Log("Player hit :" + checkPointName);
        }
    }
}
