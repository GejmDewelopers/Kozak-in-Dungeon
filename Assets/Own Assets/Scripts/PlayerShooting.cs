using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //HARD HIT IS IN THE CHARGE BAR SCRIPT
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Animator animator;
    [SerializeField] ChargeBar chargeBar;
    public GameObject playerArm;
    Bullet playerArmBulletScript;
    float hitDamage;
    float memoryDamage;

    public float bulletForce = 20f;
    PlayerHealth playerHealthScript;

    float timer;

    private void Start()
    {
        playerHealthScript = GetComponent<PlayerHealth>();

        playerArmBulletScript = playerArm.GetComponentInChildren<Bullet>();
        hitDamage = playerArmBulletScript.damage;

        playerArm.SetActive(false);
    }

    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            //TODO: MAYBE REMOVE ONE OF THE OFFENCE MECHANISM LATER
            if (Input.GetButtonDown("Fire1") && playerHealthScript.state == PlayerHealthState.Alive)
            {
                Shoot();
                animator.SetTrigger("Shoot");
            }

            if (Input.GetButton("Fire2") && playerHealthScript.state == PlayerHealthState.Alive)
            {
                timer += Time.deltaTime;
                if (timer >= 0.5f)
                {
                    PlayerMovement.speed = 3f;
                    chargeBarBorder.SetActive(true);
                    chargeBar.SetActive(true);

                    if (timer >= 1.5f)
                    {
                        slider.value = 1f;
                        chargeBar.GetComponent<Image>().color = gradient.Evaluate(slider.value);
                    }
                    else
                    {
                        slider.value = timer - 0.5f;
                        chargeBar.GetComponent<Image>().color = gradient.Evaluate(slider.value);
                    }
                }
            }
            else
            {
                slider.value = 0f;
                if (timer > 0.5f && playerShootingScript != null)
                {
                    playerShootingScript.ProcessHardHit(timer - 0.5f);
                }

                PlayerMovement.speed = 10f;
                timer = 0;
                chargeBar.SetActive(false);
                chargeBarBorder.SetActive(false);
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

    IEnumerator SwingArm(float damage)
    {
        playerArm.SetActive(true);
        memoryDamage = playerArmBulletScript.damage;
        playerArmBulletScript.damage = damage;
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
        playerArmBulletScript.damage = memoryDamage;
        playerArm.SetActive(false);
    }

    void ProcessHardHit(float value)
    {
        if (value < 0.4f) {
            StartCoroutine(SwingArm(hitDamage * 1.3f));
        }
        if(value >= 0.4f && value <= 0.9f)
        {
            StartCoroutine(SwingArm(hitDamage * 1.6f));
        }
        if (value >= 0.9f)
        {
            StartCoroutine(SwingArm(hitDamage * 2f));
        }
    }

}
