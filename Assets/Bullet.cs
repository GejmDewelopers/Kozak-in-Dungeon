using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem twoShockwavesCollisionParticles;

    [Space(4)]
    [SerializeField] float explosionRange = 5f;
    [SerializeField] float explosionDamageMultiplier = 2f; //this is proced twice, so 2f on hardlight means 1.3*baseDamage*4
    public LayerMask enemyLayers;
    public float damage; 
    [SerializeField] AudioClip destroyingSound;
    [SerializeField] AudioClip shockwaveCollisionSound;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if two shockwaves collide
        if (collision.gameObject.layer == 10) //bullet
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth!=null) enemyHealth.ReceiveDamage(damage * explosionDamageMultiplier);
            }
            ParticleSystem pa = Instantiate(twoShockwavesCollisionParticles, transform.position, Quaternion.identity);
            Destroy(pa.gameObject, 1f);
            if (shockwaveCollisionSound) AudioSource.PlayClipAtPoint(shockwaveCollisionSound, transform.position);
            Destroy(gameObject);
        }
        //if shockwave collides with terrain or enemies
        else
        {
            ParticleSystem pa = Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
            Destroy(pa.gameObject, 1f);
            if (destroyingSound) AudioSource.PlayClipAtPoint(destroyingSound, transform.position);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
