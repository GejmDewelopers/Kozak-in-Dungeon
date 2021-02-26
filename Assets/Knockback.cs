using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10) //bullet
        {
            rb.AddForce(collision.relativeVelocity * 0.01f, ForceMode2D.Force);
        }
    }
}
