using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Transform target;

    public EnemyState enemyState = EnemyState.Waiting;
    public bool wasActivated = false;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;

    public ParticleSystem deathParticles;
    public Animator animator;

    public abstract void OnDeath();
    public abstract IEnumerator FireBullets();
}
