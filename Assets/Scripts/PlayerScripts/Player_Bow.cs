using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player_Bow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform lauchPoint;

    public GameObject arrowPrefab;
    private Vector2 aimDirection = Vector2.right;

    public float shootCooldown = 0.5f;
    private float shootTimer;

    // Update is called once per frame
    void Update()
    {
        HandleAiming();
        if (Input.GetButtonDown("Shoot"))
        {
            Shoot();
        }
        //Shoot();
    }
    public void Shoot()
    {
        Arrow arrow = Instantiate(arrowPrefab, lauchPoint.position, Quaternion.identity).GetComponent<Arrow>();
        arrow.direction = aimDirection;
    }
    private void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            aimDirection = new Vector2(horizontal, vertical).normalized; 
        }
    }
}
