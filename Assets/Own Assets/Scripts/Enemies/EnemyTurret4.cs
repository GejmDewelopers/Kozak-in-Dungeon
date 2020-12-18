using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret4 : EnemyStationary
{
    [SerializeField] float timeBetweenShots = 1f;
    Vector2 dirVector0 = new Vector2(0, 1).normalized;
    Vector2 dirVector1 = new Vector2(1, 0).normalized;
    Vector2 dirVector2 = new Vector2(0, -1).normalized;
    Vector2 dirVector3 = new Vector2(-1, 0).normalized;



    private void Update()
    {
        if (enemyState == EnemyState.Active && wasActivated == false) StartCoroutine(FireBullets());
    }

    public IEnumerator FireBullets()
    {
        wasActivated = true;
        while (true)
        {
            InstantiateBulletAndAddForce(new Vector2(0f, 0f));

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
    }

    public override void OnDeath()
    {
        //throw new System.NotImplementedException();
    }

}
