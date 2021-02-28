using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCollider : MonoBehaviour
{

    public enum ColliderType
    {
        Body,
        Head
    }

    public ColliderType thisColliderType;
    private EnemyHealth enemyHealth;
    public void takeDamage(float damage)
    {
        if(thisColliderType == ColliderType.Head)
        {
            enemyHealth.takeDamage(damage * 2);
        }
        else
        {
            enemyHealth.takeDamage(damage);
        }
    }

    private void Start()
    {
        enemyHealth = transform.parent.GetComponent<EnemyHealth>();
    }
}
