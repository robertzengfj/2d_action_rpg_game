using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Play_Combat : MonoBehaviour
{
    public Animator anim;
    public float cooldown = 2;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
    public void Attack()
    {
        if (timer <= 0)
        {
            anim.SetBool("isAttacking", true);
            //Collider2D[] enemies=Physics2D.OverlapCircleAll()
            timer = cooldown;
        }
    }
}
