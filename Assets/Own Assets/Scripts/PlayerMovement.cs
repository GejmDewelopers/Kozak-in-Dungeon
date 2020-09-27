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
    PlayerHealth playerHealthScript;
    void Start()
    {
        playerHealthScript = GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (playerHealthScript.state == PlayerState.Alive && !PauseMenu.GameIsPaused)
        {
            TakeInputs();
            RunAnimHandler();
        }
    }

    private void TakeInputs()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void RunAnimHandler()
    {
        if (movement.magnitude >= Mathf.Epsilon) animator.SetBool("isRunning", true);
        else animator.SetBool("isRunning", false);
    }

    private void FixedUpdate()
    {
        MoveAndRotatePlayer();
    }

    private void MoveAndRotatePlayer()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}
