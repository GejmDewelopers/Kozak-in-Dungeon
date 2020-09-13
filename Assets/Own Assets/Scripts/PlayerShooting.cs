using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Animator animator;

    public float bulletForce = 20f;
    PlayerHealth playerHealthScript;

    private void Start()
    {
        playerHealthScript = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            if (Input.GetButtonDown("Fire1") && playerHealthScript.state == PlayerState.Alive)
            {
                Shoot();
                animator.SetTrigger("Shoot");
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse); // TODO: maybe firepoint.up later
        Destroy(bullet.gameObject, 5f);
    }
}
