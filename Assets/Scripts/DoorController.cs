using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isOpen = false;
    public bool isLockedDoor;
    [SerializeField] private float speed;
    [Header("Sliding Configs")]
    [SerializeField] private Vector3 slideDirection = Vector3.back;
    [SerializeField] private float slideAmount = 5f;

    private Vector3 startPosition;
    private Coroutine animationCoroutine;

    private void Awake()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        if(!isLockedDoor)
        {
            if(!isOpen)
            {
                if(animationCoroutine != null)
                    StopCoroutine(animationCoroutine);

                animationCoroutine = StartCoroutine(DoSlidingOpen_Aysnc());
            }
        }
    }
    public void Close()
    {
        if(!isLockedDoor)
        {
            if(isOpen)
            {
                if(animationCoroutine != null)
                    StopCoroutine(animationCoroutine);

                animationCoroutine = StartCoroutine(DoSlidingClose_Aysnc());
            }
        }
    }


    private IEnumerator DoSlidingOpen_Aysnc()
    {
        Vector3 endPos = startPosition + slideAmount * slideDirection;
        Vector3 startPos = transform.position;

        float time = 0;
        isOpen = true;
        while(time < 1)
        {
            transform.position = Vector3.Lerp(startPos, endPos, time);
            yield return null;
            time += Time.deltaTime * speed;
        }

    }
    private IEnumerator DoSlidingClose_Aysnc()
    {
        Vector3 endPos = startPosition;
        Vector3 startPos = transform.position;

        float time = 0;
        isOpen = false;
        while(time < 1)
        {
            transform.position = Vector3.Lerp(startPos, endPos, time);
            yield return null;
            time += Time.deltaTime * speed;
        }

    }
}
