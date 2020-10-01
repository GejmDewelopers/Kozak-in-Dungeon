using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Animator animator;
    public GameObject playerArm;

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
            //TODO: MAYBE REMOVE ONE OF THE OFFENCE MECHANISM LATER
            if (Input.GetButtonDown("Fire1") && playerHealthScript.state == PlayerState.Alive)
            {
                Shoot();
                animator.SetTrigger("Shoot");
            }

            if (Input.GetButtonDown("Fire2") && playerHealthScript.state == PlayerState.Alive)
            {
                if(!playerArm.activeSelf) StartCoroutine(SwingArm());
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

    IEnumerator SwingArm()
    {
        playerArm.SetActive(true);
        for (float i=1f; i>0.1; i-=0.1f)
        {
            playerArm.transform.rotation = Quaternion.Euler(0, 0, 60 * i + this.gameObject.transform.rotation.eulerAngles.z);
            yield return new WaitForSeconds(0.005f);
        }
        for (float i = 0.1f; i < 1; i+=0.1f)
        {
            playerArm.transform.rotation = Quaternion.Euler(0, 0, -60 * i + this.gameObject.transform.rotation.eulerAngles.z);
            yield return new WaitForSeconds(0.005f);
        }
        playerArm.SetActive(false);
    }


}
