using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyChasingShooting : EnemyMovingPathfinding
{
    [SerializeField] float timeBetweenShots = 1f;
    [SerializeField] float timeToStopToShoot = 0.5f;
    Vector2 dirVector0 = new Vector2(0, 1).normalized;
    Vector2 dirVector1 = new Vector2(1, 0).normalized;
    Vector2 dirVector2 = new Vector2(0, -1).normalized;
    Vector2 dirVector3 = new Vector2(-1, 0).normalized;
    [SerializeField] AIPath aiPath;
    private void Update()
    {
        if (enemyState == EnemyState.Active && wasActivated == false) StartCoroutine(FireBullets());
    }

    private void Start()
    {
        aiPath.maxSpeed = 0;
        target = FindObjectOfType<PlayerHealth>().gameObject.transform;
        GetComponent<AIDestinationSetter>().target = target;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    public override IEnumerator FireBullets()
    {
        wasActivated = true;
        int direction = 0;
        while (true)
        {
            aiPath.maxSpeed = 0;
            Vector2 shootingDirection = DetermineBulletDirection(direction);
            InstantiateBulletAndAddForce(shootingDirection);
            if (direction < 3) direction++;
            else direction = 0;
            yield return new WaitForSeconds(timeToStopToShoot);
            aiPath.maxSpeed = speed;
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    public void InstantiateBulletAndAddForce(Vector2 forceDirection)
    {
        GameObject spawnedBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = spawnedBullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(dirVector0 * bulletSpeed, ForceMode2D.Impulse);

        GameObject spawnedBullet1 = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb1 = spawnedBullet1.GetComponent<Rigidbody2D>();
        bulletRb1.AddForce(dirVector1 * bulletSpeed, ForceMode2D.Impulse);

        GameObject spawnedBullet2 = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb2 = spawnedBullet2.GetComponent<Rigidbody2D>();
        bulletRb2.AddForce(dirVector2 * bulletSpeed, ForceMode2D.Impulse);

        GameObject spawnedBullet3 = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb3 = spawnedBullet3.GetComponent<Rigidbody2D>();
        bulletRb3.AddForce(dirVector3 * bulletSpeed, ForceMode2D.Impulse);
    }

    private Vector2 DetermineBulletDirection(int direction)
    {
        return new Vector2(0f, 0f);
    }

    public override void OnDeath()
    {
        //throw new System.NotImplementedException();
    }
 
}
