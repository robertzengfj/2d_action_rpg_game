using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public int damage=1;
    public float knockbackForce;
    public float attackRange = 1.5f;
    public float weaponRange=1;

    public LayerMask playerLayer;
    public Transform attackPoint;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Call the method to deal damage to the player
          collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
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
            //Debug.Log("Hit player");
            PlayerHealth playerHealth = hits[0].GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ChangeHealth(-damage); // Adjust damage value as needed

            }
            PlayerMovement playerMovement=hits[0].GetComponent<PlayerMovement>();
            playerMovement.Knockback(transform,knockbackForce);
        }
    }
}
