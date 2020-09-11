using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float healthPoints = 100;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Color normalColor = new Color(1f,1f,1f,1f);
    [SerializeField] Color damageColor = new Color(1f,0f,0f,1f);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if(bullet) ReceiveDamage(bullet.damage);
    }

    void ReceiveDamage(float damage)
    {
        healthPoints -= damage;
        StartCoroutine(ChangeColorOnDamage());
        if (healthPoints <= 0f) Destroy(gameObject);
    }

    IEnumerator ChangeColorOnDamage()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = normalColor;
    }

}

