using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem hitParticlePrefab;
    public float damage = 20f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ParticleSystem pa = Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
        Destroy(pa.gameObject, 1f);
        Destroy(gameObject);
    }

}
