using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret3 : EnemyStationary
{
    [SerializeField] float timeBetweenShots = 1f;
    Vector2 dirVector0 = new Vector2(0, 1).normalized;
    Vector2 dirVector1 = new Vector2(1, 1).normalized;
    Vector2 dirVector2 = new Vector2(1, 0).normalized;
    Vector2 dirVector3 = new Vector2(1, -1).normalized;
    Vector2 dirVector4 = new Vector2(0, -1).normalized;
    Vector2 dirVector5 = new Vector2(-1, -1).normalized;
    Vector2 dirVector6 = new Vector2(-1, 0).normalized;
    Vector2 dirVector7 = new Vector2(-1, 1).normalized;



    private void Update()
    {
        if (enemyState == EnemyState.Active && wasActivated == false) StartCoroutine(FireBullets());
    }

    public override IEnumerator FireBullets()
    {
        wasActivated = true;
        while (true)
        {
            InstantiateBulletAndAddForce(new Vector2(0f,0f));
 
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    public override Vector2 DetermineBulletDirection(int direction)
    {
        return new Vector2(0f, 0f);
    }

    public override void InstantiateBulletAndAddForce(Vector2 forceDirection)
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

        GameObject spawnedBullet4 = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb4 = spawnedBullet4.GetComponent<Rigidbody2D>();
        bulletRb4.AddForce(dirVector4 * bulletSpeed, ForceMode2D.Impulse);

        GameObject spawnedBullet5 = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb5 = spawnedBullet5.GetComponent<Rigidbody2D>();
        bulletRb5.AddForce(dirVector5 * bulletSpeed, ForceMode2D.Impulse);

        GameObject spawnedBullet6 = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb6 = spawnedBullet6.GetComponent<Rigidbody2D>();
        bulletRb6.AddForce(dirVector6 * bulletSpeed, ForceMode2D.Impulse);

        GameObject spawnedBullet7 = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb7 = spawnedBullet7.GetComponent<Rigidbody2D>();
        bulletRb7.AddForce(dirVector7 * bulletSpeed, ForceMode2D.Impulse);
    }

    public override void OnDeath()
    {
        //throw new System.NotImplementedException();
    }

}
