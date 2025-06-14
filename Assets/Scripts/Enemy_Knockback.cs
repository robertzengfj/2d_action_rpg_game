using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Knockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private Enemy_Movement enemy_Movement;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy_Movement = GetComponent<Enemy_Movement>();
    }
    public void Knockback(Transform forceTransform, float knockbackForce, float knockbackTime,float stunTime)
    {
        enemy_Movement.ChangeState(EnemyState.Knockback);
        StartCoroutine(StunTimer(knockbackTime,stunTime));
        Vector2 direction = (transform.position - forceTransform.position).normalized;
        rb.velocity = direction * knockbackForce;
        Debug.Log("knockback applied.");
    }
    IEnumerator StunTimer(float knockbackTime,float stunTime)
    {
         yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        Debug.Log("enemy rb velocity is zero");
        enemy_Movement.ChangeState(EnemyState.Idle);
    }
}
