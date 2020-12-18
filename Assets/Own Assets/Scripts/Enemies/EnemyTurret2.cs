using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret2 : EnemyStationary
{
    [SerializeField] float timeBetweenShots = 1f;
    private void Update()
    {
        if (enemyState == EnemyState.Active && wasActivated == false) StartCoroutine(FireBullets());
    }

    public IEnumerator FireBullets()
    {
        wasActivated = true;
        int direction = 0;
        while (true)
        {
            Vector2 shootingDirection = DetermineBulletDirection(direction);
            InstantiateBulletAndAddForce(shootingDirection);
            if (direction < 3) direction++;
            else direction = 0;
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    public override Vector2 DetermineBulletDirection(int direction)
    {
        Vector2 dirVector;
        switch (direction)
        {
            case 0:
                dirVector = new Vector2(0, 1);
                break;
            case 1:
                dirVector = new Vector2(1, 0);
                break;
            case 2:
                dirVector = new Vector2(0, -1);
                break;
            case 3:
                dirVector = new Vector2(-1, 0);
                break;
            default:
                dirVector = new Vector2(0, 0);
                print("Enemy turret 1 - omething went wrong!");
                break;
        }
        dirVector.Normalize();
        return dirVector;
    }

    public override void InstantiateBulletAndAddForce(Vector2 forceDirection)
    {
        GameObject spawnedBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = spawnedBullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(forceDirection * bulletSpeed, ForceMode2D.Impulse);
    }

    public override void OnDeath()
    {
        //throw new System.NotImplementedException();
    }

}
