using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Enemy_Movement : MonoBehaviour
{
    private EnemyState enemyState;
    public float attackRange = 2;
    private Rigidbody2D rb;
    private Transform player;

    public Animator anim;

    // private bool isChasing;

    public float speed = 4f;

    private int faceingDirection = -1;

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(EnemyState.Idle);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState == EnemyState.Chasing)
        {
            Chase();
        }
        else if (enemyState == EnemyState.Idle)
        {
            rb.velocity = Vector2.zero;
        }
        else if (enemyState == EnemyState.Attacking)
        {
            //do attack
            
        }
    }

    void Chase()
    {
                    // Check if the player is to the left or right of the enemy
            if (player != null)
            {
                if ((player.position.x > transform.position.x && faceingDirection == -1) || (player.position.x < transform.position.x && faceingDirection == 1))
                {
                    Flip();
                }
                Vector2 direction = (player.position - transform.position).normalized;
                rb.velocity = direction * speed;
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player detected");
            if (player == null)
            {
                player = collision.transform;
            }
            //isChasing = true;
            ChangeState(EnemyState.Chasing);
        }
        //isChasing = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player out of range");
            rb.velocity = Vector2.zero;
           // isChasing = false;
            ChangeState(EnemyState.Idle);
        }
       
    }
    void Flip()
    {
        faceingDirection *= -1;
        // Vector3 localScale = transform.localScale;
        // localScale.x *= -1;
        // transform.localScale = localScale;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    void ChangeState(EnemyState newState)
    {
        //Exit the current state
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", false);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("IsChasing", false);
        }else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("IsAttacking", false);
        }
        //Update our current state
        enemyState = newState;
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", true);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("IsChasing", true);
        }else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("IsAttacking", true);
        }


    }
}
public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
        
    }