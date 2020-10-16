﻿using System;
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

    [Space(4)]

    [SerializeField] TrailRenderer[] swingTrails;
    [SerializeField] Gradient defaultTrailGradient;
    [SerializeField] Gradient lightTrailGradient;
    [SerializeField] Gradient mediumTrailGradient;
    [SerializeField] Gradient hardTrailGradient;


    [Space(4)]
    //Multipliers to charged hits
    public float hardLightMultiplier = 1.3f;
    public float hardMediumMultiplier = 1.6f;
    public float hardStrongMultiplier = 2f;

    [Space(4)]

    public GameObject playerArm;
    Bullet playerArmBulletScript;
    float hitDamage;
    float memoryDamage;

    public float bulletForce = 20f;
    PlayerHealth playerHealthScript;

    PlayerMovement playerMovement;

    float timer;

    private void Start()
    {
        playerHealthScript = GetComponent<PlayerHealth>();
        playerArmBulletScript = playerArm.GetComponentInChildren<Bullet>();
        hitDamage = playerArmBulletScript.damage;

        playerArm.SetActive(false);

        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            //TODO: MAYBE REMOVE ONE OF THE OFFENCE MECHANISM LATER
            if (Input.GetButtonDown("Fire1") && PlayerHealth.state == PlayerHealthState.Alive)
            {
                Shoot();
                animator.SetTrigger("Shoot");
            }

            ProcessMeleeAttackAndChargeBar();
        }
    }

    private void ProcessMeleeAttackAndChargeBar()
    {
        if (Input.GetButton("Fire2") && PlayerHealth.state == PlayerHealthState.Alive && !playerArm.activeSelf)
        {
            timer += Time.deltaTime;
        }
        if (Input.GetButtonUp("Fire2") && PlayerHealth.state == PlayerHealthState.Alive && !playerArm.activeSelf)
        {
            if (timer < 0.5f)
            {
                ProcessHit(false, 0);
                timer = 0f;
            }
            else
            {
                ProcessHit(true, timer - 0.5f);
                timer = 0f;
            }
        }
        if (timer >= 0.5f) playerMovement.speed = playerMovement.chargingAttackSpeed;
        else playerMovement.speed = playerMovement.defaultSpeed;
        chargeBar.ProcessChargeBar(Input.GetButton("Fire2"), timer);
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse); // TODO: maybe firepoint.up later
        Destroy(bullet.gameObject, 5f);
    }

    IEnumerator SwingArm(float damage, int type)
    {
        playerArm.SetActive(true);

        memoryDamage = playerArmBulletScript.damage;
        playerArmBulletScript.damage = damage;

        SetTrailColors(type);

        //changing arm position
        for (float i = 1f; i > 0.1; i -= 0.1f)
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

    private void SetTrailColors(int type)
    {
        if (type == 0)
        {
            foreach (TrailRenderer trail in swingTrails)
            {
                trail.colorGradient = defaultTrailGradient;
            }
        }
        if (type == 1)
        {
            foreach(TrailRenderer trail in swingTrails)
            {
                trail.colorGradient = lightTrailGradient;
            }
        }
        if (type == 2)
        {
            foreach (TrailRenderer trail in swingTrails)
            {
                trail.colorGradient = mediumTrailGradient;
            }
        }
        if (type == 3)
        {
            foreach (TrailRenderer trail in swingTrails)
            {
                trail.colorGradient = hardTrailGradient;
            }
        }
    }

    void ProcessHit(bool isHard, float value)
    {
        if (!isHard) StartCoroutine(SwingArm(hitDamage,0));
        else
        {
            if (value < 0.4f)
            {
                StartCoroutine(SwingArm(hitDamage * hardLightMultiplier, 1));
            }
            if (value >= 0.4f && value <= 0.9f)
            {
                StartCoroutine(SwingArm(hitDamage * hardMediumMultiplier, 2));
            }
            if (value >= 0.9f)
            {
                StartCoroutine(SwingArm(hitDamage * hardStrongMultiplier, 3));
            }
        }
    }

    public float GetHitDamage()
    {
        return hitDamage;
    }

    public void FlatChangeDamage(float value)
    {
        hitDamage += value;
    }

    public void MultiplierChangeDamage(float multiplier)
    {
        hitDamage *= multiplier;
    }

    public void FlatChangeLightAttackMultiplier(float value)
    {
        hardLightMultiplier += value;
    }

    public void MultiplierChangeLightAttackMultiplier(float multiplier)
    {
        hardLightMultiplier *= multiplier;
    }

    public void FlatChangeMediumAttackMultiplier(float value)
    {
        hardMediumMultiplier += value;
    }

    public void MultiplierChangeMediumAttackMultiplier(float multiplier)
    {
        hardMediumMultiplier *= multiplier;
    }

    public void FlatChangeStrongAttackMultiplier(float value)
    {
        hardStrongMultiplier += value;
    }

    public void MultiplierChangeStrongAttackMultiplier(float multiplier)
    {
        hardStrongMultiplier *= multiplier;
    }
}
