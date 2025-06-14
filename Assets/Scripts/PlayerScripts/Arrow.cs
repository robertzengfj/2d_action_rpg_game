using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public Rigidbody2D rb;
    public Vector2 direction = Vector2.right;
    public float lifeSpawn = 2;
    public float speed;
    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;

    public SpriteRenderer sr;
    public Sprite buriedSprite;
    public int damage;

    public float KnockbackForce;
    public float KnockbackTime;

    public float stunTime;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = direction * speed;
        RotateArrow();
        Destroy(gameObject, lifeSpawn);
    }

    private void RotateArrow()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Arrow hit: " + collision.gameObject.name);
        Debug.Log("Arrow hit layer: " + collision.gameObject.layer);

        // if(collision.gameObject.name=="Arrow")
        // {
        //     Debug.Log("Arrow hit itself,destroying.");
        //     Destroy(collision.gameObject);
        //     //return; // Ignore collision with itself
        // }
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) > 0)
        {

            Enemy_Health cores = collision.gameObject.GetComponent<Enemy_Health>();
            if (cores != null)
            {
                cores.ChangeHealth(-damage);
            }
            Enemy_Knockback knockback = collision.gameObject.GetComponent<Enemy_Knockback>();
            if (knockback != null)
            {
                knockback.Knockback(transform, KnockbackForce, KnockbackTime, stunTime);
            }
            AttachToTarget(collision.gameObject.transform);
        }
        else if ((obstacleLayer.value & (1 << collision.gameObject.layer)) > 0)
        {   //
            AttachToTarget(collision.gameObject.transform);
            // }

        }
    }
    private void AttachToTarget(Transform target)
    {
        sr.sprite = buriedSprite;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        transform.SetParent(target);
    }

}
