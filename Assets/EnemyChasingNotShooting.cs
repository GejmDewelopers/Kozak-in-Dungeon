using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyChasingNotShooting : EnemyMovingPathfinding
{
    public override IEnumerator FireBullets()
    {
        wasActivated = true;
        yield return new WaitForSeconds(0.001f);
    }

    public override void OnDeath()
    {
       // throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>().gameObject.transform;
        GetComponent<AIDestinationSetter>().target = target;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
