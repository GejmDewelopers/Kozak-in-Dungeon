using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChargeBar : MonoBehaviour
{
    public Slider slider;


    public GameObject chargeBar;
    public GameObject chargeBarBorder;

    public Gradient gradient;


    public void ProcessChargeBar(bool isKeyPressed, float timer)
    {
        if (isKeyPressed)
        {
            if (timer >= 0.5f)
            {
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
            chargeBar.SetActive(false);
            chargeBarBorder.SetActive(false);
        }
    }
}
