using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{

    public float speed = 2f;
    public float idleTime = 5f;

    private Vector2 direction;
    private Vector2 scale;
    private Animator _animator;
    private float attackTime = 2f;
    private bool isAttacking;

    void Start()
    {
        _animator = GetComponent<Animator>();
        direction = new Vector2(0, 0);
        isAttacking = false;
    }

    void Update()
    {
        //LongIdle Animation trigger
        if (idleTime > 0)
            idleTime -= Time.deltaTime;
        else
            _animator.SetTrigger("LongIdle");

        //Default Animation Idle
        _animator.SetBool("Idle", true);
        
        //Call Walk Function
        if ((Input.GetKey("d") || Input.GetKey("a")) && isAttacking == false )
            Walk();

        if (Input.GetKeyDown("mouse 0"))
        {
            isAttacking = true;
            Attack();
        }

        if (isAttacking == true)
        {
            attackTime -= Time.deltaTime;
            Debug.Log("attackTime: " + attackTime);
            if (attackTime <= 0)
                isAttacking = false;
        }
    }

    void Walk()
    {
        //Reset IdleTimer
        idleTime = 5;

        //Update Animation Booleans
        _animator.SetBool("Idle", false);
        _animator.SetBool("IsWalking", true);

        //Update Transform and Scale
        if (Input.GetKey("d"))
        {
            scale = new Vector3(1, 1, 1);
            direction = new Vector2(1, 0);
        }
        else if (Input.GetKey("a"))
        {
            scale = new Vector3(-1, 1, 1);
            direction = new Vector2(-1, 0);
        }
        
        Vector2 movement = direction.normalized * speed * Time.deltaTime;
        transform.Translate(movement);
        transform.localScale = scale;
    }

    void Attack()
    {
        attackTime = 2f;
        Debug.Log("Attack");
        idleTime = 5;
        _animator.SetBool("Idle", false);
        _animator.SetTrigger("Attack");
    }
}
