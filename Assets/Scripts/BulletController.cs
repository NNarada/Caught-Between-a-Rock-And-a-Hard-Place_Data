using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Rigidbody bulletRB;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private int damge = 1;
    private float lifeTime = 5f;
   
    public Vector3 target {get; set; }
    public bool hit {get; set;}

    //OnEnable is called once the bullet is instantiated. thus we distory the bullet after (liftime seconds)

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
            Destroy(gameObject);

        transform.position = Vector3.MoveTowards(transform.position, target, bulletSpeed * Time.deltaTime);
        if(!hit && Vector3.Distance(transform.position, target) < 0.01f)
        {
            Destroy(gameObject);
            Instantiate(impactEffect, transform.position + transform.up * (-bulletSpeed * Time.deltaTime), transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealthController>().DamgeEnmey(damge);
        }
        Destroy(gameObject);
        Instantiate(impactEffect, transform.position + transform.up * (-bulletSpeed * Time.deltaTime), transform.rotation);
    }
}
