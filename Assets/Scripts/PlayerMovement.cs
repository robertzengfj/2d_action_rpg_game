using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public int facingDirection = 1;
    public Rigidbody2D rb;
    public Animator anim;
    // Start is called before the first frame update


    // FixedUpdate is called once 50x frame
    void FixedUpdate()
    {
       float horizontal=Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if ((horizontal > 0 && transform.localScale.x < 0)|| (horizontal < 0 && transform.localScale.x > 0))
        {
            Flip();
        }
        anim.SetFloat("horizontal", Mathf.Abs(horizontal));
        anim.SetFloat("vertical", Mathf.Abs(vertical));
        rb.velocity = new Vector2(horizontal, vertical)* speed;

    }
    public void Flip()
    {
        facingDirection*=-1;
        transform.localScale = new Vector3(facingDirection, transform.localScale.y, transform.localScale.z);
    }
}
