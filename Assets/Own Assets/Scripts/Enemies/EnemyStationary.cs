using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStationary : Enemy
{
    public abstract Vector2 DetermineBulletDirection(int direction);
    public abstract void InstantiateBulletAndAddForce(Vector2 forceDirection);
}
