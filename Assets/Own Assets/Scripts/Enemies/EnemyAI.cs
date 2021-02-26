
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : EnemyMovingPathfinding
{
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>().gameObject.transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemyState == EnemyState.Waiting) return;
        if (wasActivated == false) InvokeRepeating("UpdatePath", 0f, 0.5f);
        if (path == null) return;
        if(wasActivated==false) wasActivated = true;
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    public override void OnDeath()
    {
        //throw new System.NotImplementedException();
    }

    public override IEnumerator FireBullets()
    {
        yield return new WaitForSeconds(0.001f);
       // throw new System.NotImplementedException();
    }
}
