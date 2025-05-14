using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public int damage=1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Call the method to deal damage to the player
           collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
        }
        
    }
}
