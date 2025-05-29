using System.Collections;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Transform attackPoint;
    //public float weaponRange = 1;

    //public float knockbackForce = 50;
    //public float knckbackTime=0.15f;
    public LayerMask enemyLayer;

    //public int damage = 1;
    public Animator anim;
    public float cooldown = 2;
    private float timer;

    //public float stunTime = 1;

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
            anim.SetBool("isAttaching", true);

            timer = cooldown;
        }

    }
    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, StatsManager.Instance.weaponRange, enemyLayer);
        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<Enemy_Health>().ChangeHealth(-StatsManager.Instance.damage);
            enemies[0].GetComponent<Enemy_Knockback>().Knockback(transform,StatsManager.Instance.knockbackForce,StatsManager.Instance.knockbackTime,StatsManager.Instance.stunTime);
        }

    }
    public void FinishAttacking()
    {
        anim.SetBool("isAttaching", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, StatsManager.Instance.weaponRange);
    }

}
