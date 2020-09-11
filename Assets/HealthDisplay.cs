using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthDisplay : MonoBehaviour
{
    [SerializeField] RawImage[] healthImages;

    public void SetHealth(int currentHealth)
    {
        for(int i = 0; i < currentHealth-1; i++)
        {
            healthImages[i].gameObject.SetActive(true);
        }
        for(int i = 9; i> currentHealth-1; i--)
        {
            healthImages[i].gameObject.SetActive(false);
        }
        
    }
}
