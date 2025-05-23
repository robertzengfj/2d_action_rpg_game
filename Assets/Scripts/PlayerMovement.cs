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
    private bool isKnockedback = false;

    // FixedUpdate is called once 50x frame
    void FixedUpdate()
    {
        if(isKnockedback==false)
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
    }
    public void Flip()
    {
        facingDirection*=-1;
        transform.localScale = new Vector3(facingDirection, transform.localScale.y, transform.localScale.z);
    }
    public void Knockback(Transform enemy,float force,float stunTime){
        isKnockedback = true;
        Vector2 direction=(transform.position-enemy.position).normalized;
        Debug.Log(direction);
        rb.velocity=direction*force;
        StartCoroutine(KnockbackCounter(stunTime));
    }
    IEnumerator KnockbackCounter(float stunTime){
        yield return new WaitForSeconds(stunTime);
        rb.velocity=Vector2.zero;
        isKnockedback = false;
    }
}
