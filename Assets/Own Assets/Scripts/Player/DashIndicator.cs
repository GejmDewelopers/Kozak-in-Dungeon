using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashIndicator : MonoBehaviour
{
    public Slider slider;



    private void FixedUpdate()
    {
        slider.value = PlayerMovement.getEvadeTimer();
    }

}
