using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public Rigidbody2D rb;
    // Start is called before the first frame update


    // FixedUpdate is called once 50x frame
    void FixedUpdate()
    {
       float horizontal=Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(horizontal, vertical)* speed;

    }
}
