using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player_Bow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform lauchPoint;

    public GameObject arrowPrefab;
    public PlayerMovement playerMovement;
    private Vector2 aimDirection = Vector2.right;

    public float shootCooldown = 0.5f;
    private float shootTimer;
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        shootTimer-=Time.deltaTime;
        HandleAiming();
        if (Input.GetButtonDown("Shoot")&& shootTimer <= 0)
        {
            playerMovement.isShooting = true;
            anim.SetBool("isShooting", true);
            //Shoot();
            //shootTimer = shootCooldown;
        }
        
        //Shoot();
    }

    private void OnEnable()
    {
        anim.SetLayerWeight(0, 0);
        anim.SetLayerWeight(1, 1);
    }

    private void OnDisable()
    {
        anim.SetLayerWeight(0, 1);
        anim.SetLayerWeight(1, 0);
    }
    public void Shoot()
    {
        if (shootTimer <= 0)
        {
            Arrow arrow = Instantiate(arrowPrefab, lauchPoint.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.direction = aimDirection;
            shootTimer = shootCooldown;
        }
        anim.SetBool("isShooting", false);
        playerMovement.isShooting = false;
    }
    private void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            aimDirection = new Vector2(horizontal, vertical).normalized;
            anim.SetFloat("aimX", aimDirection.x);
            anim.SetFloat("aimY", aimDirection.y);
        }
    }
}
