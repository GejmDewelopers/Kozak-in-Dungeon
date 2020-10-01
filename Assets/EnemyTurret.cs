using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyTurret : Enemy
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public ParticleSystem deathParticles;
    public Animator animator;

    public float bulletSpeed = 5f;
    public abstract IEnumerator FireBullets();
    public abstract void OnDeath();
}
