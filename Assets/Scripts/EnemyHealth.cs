using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float startingHealth = 20;
    private float health;
    

    private void Start()
    {
        health = startingHealth;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        Debug.Log("hit");
        if(health <= 0)
        {
            Destroy(gameObject);
            
        }
    }

    

}
