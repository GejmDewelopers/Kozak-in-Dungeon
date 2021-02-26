using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] PlayerUtilities playerUtilities;
    [SerializeField] AudioClip pickupSound;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerUtilities.addMoney(1);
        AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        Destroy(gameObject);
    }
    // Update is called once per frame
}
