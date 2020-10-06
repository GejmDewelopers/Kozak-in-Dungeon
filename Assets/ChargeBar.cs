﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChargeBar : MonoBehaviour
{
    //bool isMouseKeyPressed = false;
    float timer;
    [SerializeField] PlayerShooting playerShootingScript;


    public Slider slider;


    public GameObject chargeBar;
    public GameObject chargeBarBorder;

    public Gradient gradient;


    public void ProcessChargeBar(bool isKeyPressed)
    {
        if (isKeyPressed)
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
            if(timer > 0.5f && playerShootingScript != null)
            {
                playerShootingScript.ProcessHardHit(timer-0.5f);
            }

            PlayerMovement.speed = 10f;
            timer = 0;
            chargeBar.SetActive(false);
            chargeBarBorder.SetActive(false);
        }
    }
}
