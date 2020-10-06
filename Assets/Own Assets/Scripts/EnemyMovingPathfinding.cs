using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class EnemyMovingPathfinding : Enemy
{
    public float speed;
    public float nextWaypointDistance;
    protected Path path;
    protected int currentWaypoint;
    protected bool reachedEndOfPath;
    protected Seeker seeker;
    protected Rigidbody2D rb;

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>().gameObject.transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    protected void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            this.path = p;
            currentWaypoint = 0;
        }
    }

}
