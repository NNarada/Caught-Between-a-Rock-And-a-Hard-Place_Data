using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] int currentHealth = 5;
    public bool isDead = false;
    private float courpsTimer = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0 && !isDead)
        {
            isDead = true;
        }
        if(isDead)
            courpsTimer -= Time.deltaTime;
        if(courpsTimer <= 0)
            Destroy(gameObject);
    }


    public void DamgeEnmey(int value)
    {
        currentHealth -= value;
    }
}
