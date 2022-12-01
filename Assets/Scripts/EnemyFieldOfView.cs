using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldOfView : MonoBehaviour
{
    [SerializeField] private float radius, angle;

    [SerializeField] private GameObject player;

    [SerializeField] private LayerMask playerMask, environmentMask;

    public bool playerInSight;

    private void Start() 
    {
            player = GameObject.FindGameObjectWithTag("Player");
            StartCoroutine(FOV_Aysnc());
    }

    private IEnumerator FOV_Aysnc()
    {
        WaitForSeconds delay = new WaitForSeconds(0.2f);

        while(true)
        {
            yield return delay;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeCehcks = Physics.OverlapSphere(transform.position, radius, playerMask);
        if(rangeCehcks.Length > 0)
        {
            Transform target = rangeCehcks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, environmentMask))
                    playerInSight = true;
                else
                    playerInSight = false;
            }
            else
            {
                playerInSight = false;
            }
        }
        else if(playerInSight)
            playerInSight = false;
    }
}
