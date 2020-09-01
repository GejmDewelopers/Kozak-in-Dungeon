using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Camera cam;
    Animator animator;

    Vector2 movement;
    Vector2 mousePos;
    SpriteRenderer spr; //TODO: Remove everything about it and replace with animations
    void Start()
    {
        animator = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (movement.x < 0) spr.flipX = true;
        if(movement.x > 0) spr.flipX = false;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}
