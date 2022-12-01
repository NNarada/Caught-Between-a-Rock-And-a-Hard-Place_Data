using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController playerControllerInstance;

    [SerializeField] private float walkSpeed = 2.0f, runSpeed = 8.0f, runTimer = 5f;
    private bool canRun;
    [SerializeField] GameObject camTransform;
    [SerializeField] private bool invertX;
    [SerializeField] private bool invertY;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GunAmmo gunAmmo;

    public static float mouseSensitivity = 1;
    private CharacterController controller;

    private float jumpHeight = 1.5f;
    private float gravityValue = -9.8f;
    private bool doubleJump;
    private Vector3 moveInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction lookAction;
    private InputAction sprintAction;
    private InputAction shootAction;
    private InputAction relodAction;
    


    void Start() 
    {
        mouseSensitivity = PlayerPrefs.GetFloat("currentSensitivity", 1);
        UIController.uIControllerInstance.sprintSlider.maxValue = 5;
        UIController.uIControllerInstance.sprintSlider.value = runTimer;
    }
    void OnEnable()
    {
        shootAction.performed += _ => Shoot();
    }
    void OnDisable()
    {
        shootAction.performed -= _ => Shoot();
    }

    void Awake()
    {
        firePoint = GameObject.Find("/Player/CameraPoint/FirePoint");
        canRun = true;
        controller = gameObject.GetComponent<CharacterController>();
        playerInput = gameObject.GetComponent<PlayerInput>();
        jumpAction = playerInput.actions["Jump"];
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        sprintAction = playerInput.actions["Sprint"];
        shootAction = playerInput.actions["Shoot"];
        relodAction = playerInput.actions["Relod"];
        playerControllerInstance = this;

    }
    
    void Update()
    {
        PlayerPrefs.SetFloat("currentSensitivity", mouseSensitivity);
        Movement();
        if(!GameManager.paused)
            CameraControl();
        
        //Relod if r is pressed
        if(relodAction.triggered && gunAmmo.currentAmmo < GunAmmo.maxAmmo && !GunAmmo.reloding)
            GunAmmo.relod = true;

    }

    void Movement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector2 input = moveAction.ReadValue<Vector2>();
        //set the forward dirction of the player to the direction that they are looking at
        Vector3 moveVert = transform.forward * input.y;
        //Move from side to side;
        Vector3 moveHori = transform.right * input.x;
        moveVert.y = 0;
        
        moveInput = moveVert + moveHori;

        
        anim.SetBool("onGround", groundedPlayer);
        moveInput = moveInput.normalized;

        if(sprintAction.IsPressed() && runTimer > 0 && canRun)
        {
            runTimer -= Time.deltaTime;
            controller.Move(moveInput * Time.deltaTime * runSpeed);
            //aniumate the player sprint by setting the moveSpeed in the animator
            anim.SetFloat("moveSpeed", 3 * moveInput.magnitude);
        }
        else
        {
            runTimer +=  Time.deltaTime;
            runTimer = Mathf.Clamp(runTimer, 0, 5f);
            //Make sure that we can only run after we have 1 second of stamana
            if(runTimer >= 2.5f)
                canRun = true;
            else
                canRun = false;

            controller.Move(moveInput * Time.deltaTime * walkSpeed);
            //aniumate the player walk by setting the moveSpeed in the animator
            anim.SetFloat("moveSpeed", moveInput.magnitude);
        }
        //If We are not moving gain stamana at a higher rate
        if(moveInput.magnitude <= 0)
        {
            runTimer += 2 * Time.deltaTime;
            runTimer = Mathf.Clamp(runTimer, 0, 5f);
        }
        //Update the UI sprint Bar 
        UIController.uIControllerInstance.sprintSlider.maxValue = 5;
        UIController.uIControllerInstance.sprintSlider.value = runTimer;
        
        // Changes the height position of the player..        
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += 1.6f * gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        
    }

    void CameraControl()
    {
        Vector2 mouseInput = lookAction.ReadValue<Vector2>() * mouseSensitivity;    
        if(camTransform == null)
            camTransform = GameObject.Find("/Player/CameraPoint");
        //inverting x-axis
        if(invertX)
            mouseInput.x = -mouseInput.x;
        
        //invertung y-axis
        if(invertY)
            mouseInput.y= -mouseInput.y;

        //look from side to side
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x , transform.rotation.eulerAngles.y + mouseInput.x,
        transform.rotation.eulerAngles.z);
        
        //look up down
        //clamp the value of looking up and down
        Vector3 rotationX = new Vector3(camTransform.transform.rotation.eulerAngles.x - mouseInput.y, 0, 0);
        if(rotationX.x > 90 && rotationX.x < 180)
            rotationX.x = 90;
        if(rotationX.x > 180 && rotationX.x < 270)
            rotationX.x = 270;
        
        camTransform.transform.localRotation = Quaternion.AngleAxis(rotationX.x, Vector3.right);

    }

    void Shoot()
    {
        //Debug.Log("Gun Reldogin"  + GunAmmo.reloding);
        if(GunAmmo.reloding || GameManager.paused)
        {
            //Debug.Log("retunred from Shoot");
            return;
        }
        if(firePoint == null || camTransform == null)
        {
            camTransform = GameObject.Find("/Player/CameraPoint");
            firePoint = GameObject.Find("/Player/CameraPoint/FirePoint");
        }
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        RaycastHit hit;       

        BulletController bulletController = bullet.GetComponent<BulletController>();
        if(Physics.Raycast(camTransform.transform.position, camTransform.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            bulletController.target = hit.point;
            bulletController.hit = true;
        }
        else
        {
            bulletController.target = camTransform.transform.position + camTransform.transform.forward * 30;
            bulletController.hit = false; 
        }
        //Subtract from the current ammo count
        gunAmmo.currentAmmo --;
    }

    void OnTriggerEnter(Collider other) {
        
       if(other.gameObject.tag == "EnemyFist")
       {
            PlayerHealthController.playerHealthControllerInstance.DamgePlayer(10);
       } 
    }
}
