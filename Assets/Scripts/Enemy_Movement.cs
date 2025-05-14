using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform player;

    private bool isChasing;

    public float speed = 4f;

    private int faceingDirection = -1;   

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isChasing==true){
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity=direction*speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player"){
            Debug.Log("Player detected");
            if(player==null){
            player = collision.transform;
            }
            isChasing = true;
        }
        //isChasing = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player"){
            Debug.Log("Player out of range");
            rb.velocity = Vector2.zero;
         isChasing = false;
        }
    }
}
