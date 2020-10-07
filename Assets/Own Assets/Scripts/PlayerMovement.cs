using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //this script will manage the clicked things other than movement, like M for monimap enlargement etc.

    public float speed = 10f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Camera cam;

    [Space(5)]
    [SerializeField] RectTransform minimapUIImage;
    bool isMinimapEnlarged;

    Animator animator;

    Vector2 movement;
    Vector2 mousePos;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (PlayerHealth.state == PlayerHealthState.Alive && !PauseMenu.GameIsPaused)
        {
            TakeInputs();
            RunAnimHandler();
            ManageClickedButtonsNotForMoving();
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void ManageClickedButtonsNotForMoving()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (isMinimapEnlarged)
            {
                minimapUIImage.localScale = new Vector3(3, 3, 1);

            }
            else
            {
                minimapUIImage.localScale = new Vector3(1, 1, 1);
            }
            isMinimapEnlarged = !isMinimapEnlarged;
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
        if (PlayerHealth.state == PlayerHealthState.Alive && !PauseMenu.GameIsPaused)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }
}
