using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret1 : EnemyTurret
{
    void Start()
    {
        healthPoints = 30;
        StartCoroutine(FireBullets());
    }
    public override IEnumerator FireBullets()
    {
        int direction = 0;
        while (true)
        {
            Vector2 shootingDirection = DetermineBulletDirection(direction);
            InstantiateAndAddForce(shootingDirection);
            if (direction < 7) direction++;
            else direction = 0;
            yield return new WaitForSeconds(0.2f);
        }
    }

    Vector2 DetermineBulletDirection(int direction)
    {
        Vector2 dirVector;
        switch (direction)
        {
            case 0: 
                dirVector = new Vector2(0, 1);
                break;
            case 1:
                dirVector = new Vector2(1, 1);
                break;
            case 2:
                dirVector = new Vector2(1, 0);
                break;
            case 3:
                dirVector = new Vector2(1, -1);
                break;
            case 4:
                dirVector = new Vector2(0, -1);
                break;
            case 5:
                dirVector = new Vector2(-1, -1);
                break;
            case 6:
                dirVector = new Vector2(-1, 0);
                break;
            case 7:
                dirVector = new Vector2(-1, 1);
                break;
            default:
                dirVector = new Vector2(0, 0);
                print("Enemy turret 1 - omething went wrong!");
                break;
        }
        dirVector.Normalize();
        return dirVector;
    }

    private void InstantiateAndAddForce(Vector2 forceDirection)
    {
        GameObject spawnedBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = spawnedBullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(forceDirection * bulletSpeed, ForceMode2D.Impulse);
    }

    public override void OnDeath()
    {
        throw new System.NotImplementedException();
    }
}
