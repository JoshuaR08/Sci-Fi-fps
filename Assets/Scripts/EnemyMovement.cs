using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public Transform Target;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //float step = speed * Time.deltaTime;
        //float distance = (transform.position- Target.position).magnitude;
        //if(distance <= 10)
        //{
        //    Vector3 flatDirection = new Vector3(Target.position.x - transform.position.x, 0, Target.position.z - transform.position.z);
        //    transform.position += flatDirection.normalized * step;
        //    animator.SetFloat("speed", 1);
        //    Debug.Log(step);
        //    transform.rotation = Quaternion.LookRotation(Target.position - transform.position);

        //}
        //else
        //{
        //    animator.SetFloat("speed", 0);
        //}
        
 
        
    }
}
