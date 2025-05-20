using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Enemy_Movement : MonoBehaviour
{

    public float attackRange = 1.5f;
    public int damage = 1;

    public Transform attackPoint;

    public LayerMask playerLayer;

    public float weaponRange;

    public float speed = 4f;

    public Animator anim;

    public float attackCooldown = 2f;

    public float playerDetectionRange = 5f;

    public Transform detectionPoint;

    //public LayerMask playerLayer;



    private float attackCooldownTimer;

    private EnemyState enemyState;

    private Rigidbody2D rb;
    private Transform player;

    private int faceingDirection = -1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeState(EnemyState.Idle);

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPlayer();
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
        if (enemyState == EnemyState.Attacking)
        {

            Attack();
            attackCooldownTimer = attackCooldown;
        }
        if (enemyState == EnemyState.Chasing)
        {
            Chase();
        }
        else if (enemyState == EnemyState.Idle)
        {
            // rb.velocity = Vector2.zero;
        }
        else if (enemyState == EnemyState.Attacking)
        {
            //do attack
            rb.velocity = Vector2.zero;
        }
    }

    void Chase()
    {
        // Check if the player is to the left or right of the enemy
        if (player != null)
        {
            if(Vector2.Distance(player.position, transform.position) <= attackRange&&attackCooldown<=0)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }
         

            if ((player.position.x < transform.position.x && faceingDirection == -1) || (player.position.x > transform.position.x && faceingDirection == 1))
            {
                Flip();
            }
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectionRange, playerLayer);

        if (hits.Length > 0)
        {

            player = hits[0].transform;

            //if player in attack range and cooldown is ready
            if (Vector2.Distance(player.position, transform.position) <= attackRange && attackCooldownTimer <= 0)
            {
                Debug.Log("Player in range");
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }
            else if (Vector2.Distance(transform.position, player.position) > attackRange)
            {
                Debug.Log("Player out of range");
                ChangeState(EnemyState.Chasing);
            }

        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }

        //   if (collision.gameObject.tag == "Player")
        // {
        //     Debug.Log("Player detected");
        //     if (player == null)
        //     {
        //         player = collision.transform;
        //     }
        //     //isChasing = true;
        //     ChangeState(EnemyState.Chasing);
        // }
    }
    // private void OnTriggerStay2D(Collider2D collision)
    // {
    //     if (collision.gameObject.tag == "Player")
    //     {
    //         Debug.Log("Player detected");
    //         if (player == null)
    //         {
    //             player = collision.transform;
    //         }
    //         //isChasing = true;
    //         ChangeState(EnemyState.Chasing);
    //     }
    //     //isChasing = true;

    // }

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.gameObject.tag == "Player")
    //     {
    //         Debug.Log("Player out of range");
    //         // rb.velocity = Vector2.zero;
    //         // isChasing = false;
    //         ChangeState(EnemyState.Idle);
    //     }

    // }
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
        Debug.Log("Changing state to: " + newState);
        //Exit the current state
        // if (enemyState == EnemyState.Idle)
        // {
        //     anim.SetBool("IsIdle", false);
        // }
        // else if (enemyState == EnemyState.Chasing)
        // {
        //     anim.SetBool("IsChasing", false);
        // }
        // else if (enemyState == EnemyState.Attacking)
        // {
        //     anim.SetBool("IsAttacking", false);
        // }
        // //Update our current state
        // enemyState = newState;
        // if (enemyState == EnemyState.Idle)
        // {
        //     anim.SetBool("IsIdle", true);
        //     rb.velocity = Vector2.zero;
        // }
        // else if (enemyState == EnemyState.Chasing)
        // {
        //     anim.SetBool("IsChasing", true);
        // }
        // else if (enemyState == EnemyState.Attacking)
        // {
        //     Debug.Log("Attacking");
        //     anim.SetBool("IsAttacking", true);
        // }
        enemyState = newState;
        switch (newState)
        {
            case EnemyState.Idle:
                anim.SetBool("IsIdle", true);
                anim.SetBool("IsAttacking", false);
                anim.SetBool("IsChasing", false);
                rb.velocity = Vector2.zero;
                break;
            case EnemyState.Chasing:
                anim.SetBool("IsChasing", true);
                anim.SetBool("IsAttacking", false);
                anim.SetBool("IsIdle", false);
                break;
            case EnemyState.Attacking:
                Debug.Log("Attacking");
                anim.SetBool("IsAttacking", true);
                anim.SetBool("IsChasing", false);
                anim.SetBool("IsIdle", false);
                break;
        }


    }

    public void Attack()
    {
        // Implement attack logic here
        Debug.Log("Attacking the player!");
        // You can also add damage logic here

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);
        if (hits.Length > 0)
        {


            Debug.Log("Hit player");
            PlayerHealth playerHealth = hits[0].GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ChangeHealth(-damage); // Adjust damage value as needed
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }
}
public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,

}