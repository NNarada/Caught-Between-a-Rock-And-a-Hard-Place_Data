using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndeSceneTrigger : MonoBehaviour
{
  
  private void OnTriggerEnter(Collider other) {
    if(other.gameObject.tag == "Player")
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.lockState = CursorLockMode.None;
        PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_cp", "cp1");
    }
  }
}
