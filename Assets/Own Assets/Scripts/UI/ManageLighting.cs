using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageLighting : MonoBehaviour
{
    [SerializeField] GameObject light2d;
    void TurnLightOn()
    {
        light2d.SetActive(true);
    }
    void TurnLightOff()
    {
        light2d.SetActive(false);
    }
}
