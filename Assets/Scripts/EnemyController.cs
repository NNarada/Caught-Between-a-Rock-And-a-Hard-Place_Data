using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private float distanceToChase, distanceToLose, distanceToStop;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float keepChasingTime = 10f, chaseCounter;
    
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask groundLayerMask, playerLayerMask;

    private bool chase, shooting, playerInSight;
    private Vector3 targetPoint, startPoint;

    EnemyFieldOfView enemyFieldOfView;
    EnemyHealthController enemyHealthController;


    void Awake()
    {
        distanceToChase = 20;
        distanceToLose = 20; 
        distanceToStop = 1.3f;
    }
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        enemyFieldOfView = gameObject.GetComponent<EnemyFieldOfView>();
        enemyHealthController = gameObject.GetComponent<EnemyHealthController>();
    }

    void Update()
    {
        
        //Check for sight
        Movement();
        //Check if we are moving and set the animation parameter isMoving to true
        if(agent.velocity.magnitude > 0.5f)
            anim.SetBool("isMoving", true);
        else
            anim.SetBool("isMoving", false);

        //Check if we are close enough to attack and if the player is still active in th scene
        if(!enemyHealthController.isDead)
            if(Vector3.Distance(transform.position, targetPoint) < 1.5f && PlayerController.playerControllerInstance.gameObject.activeInHierarchy)
                anim.SetTrigger("attack");
        if(enemyHealthController.isDead)
        {
            anim.SetTrigger("isDead");
        }
    }



    void Movement()
    {
        if(enemyHealthController.isDead)
        {
            agent.destination = transform.position;
            return;
        }
        targetPoint = PlayerController.playerControllerInstance.transform.position;
        if(!chase)
        {
            if(Vector3.Distance(transform.position, targetPoint) < distanceToChase)
            {
                chase = true;
            }

            if(chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;

                if(chaseCounter <= 0)
                    agent.destination = startPoint;
            }
        }
        else
        {
            if(enemyFieldOfView.playerInSight)
            {
                if(Vector3.Distance(transform.position, targetPoint) > distanceToStop)
                {
                    agent.destination = targetPoint;
                    agent.updateRotation = true;
                }
                else
                {
                    agent.updateRotation = false;
                    agent.destination = transform.position;
                    transform.LookAt(targetPoint);
                }
            }
            if(Vector3.Distance(transform.position, targetPoint) > distanceToLose)
            {
                chase = false;
                chaseCounter = keepChasingTime;
            }
        }
    }
}
