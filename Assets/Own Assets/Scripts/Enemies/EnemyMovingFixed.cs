using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovingFixed : Enemy
{
    public float speed;

    protected abstract void SetPathToFollow();
}
