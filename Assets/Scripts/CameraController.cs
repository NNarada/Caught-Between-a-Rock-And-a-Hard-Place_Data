using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject player;
    void Start()
    {
    }
    private void Awake() {
        target = player.transform.GetChild(1).gameObject.transform;
    }

    //Late update is called after all other update functions are called
    void LateUpdate()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}
