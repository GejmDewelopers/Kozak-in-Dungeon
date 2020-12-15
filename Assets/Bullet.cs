using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem hitParticlePrefab;
    public float damage = 20f;
    [SerializeField] AudioClip destroyingSound;

    //private void Start()
    //{
    //    audioSource = GetComponent<AudioSource>();
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ParticleSystem pa = Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
        Destroy(pa.gameObject, 1f);
        if(destroyingSound) AudioSource.PlayClipAtPoint(destroyingSound, transform.position);
        Destroy(gameObject);
    }

}
