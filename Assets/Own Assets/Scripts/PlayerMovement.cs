using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //this script will manage the clicked things other than movement, like M for monimap enlargement etc.

    public float defaultSpeed = 6f;
    public float chargingAttackSpeed = 3f;
    public float speed = 6f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Camera cam;

    [Space(5)]
    [SerializeField] RectTransform minimapUIImage;
    bool isMinimapEnlarged;

    Animator animator;

    Vector2 movement;
    Vector2 mousePos;

    bool wasEvadeClicked;
    public float evadeMultiplicator = 1f;
    static float evadeTimer = 0f;
    [SerializeField] float timeToChargeOneEvadeStack = 2f;
    [SerializeField] float timeBetweenEvades = 1f;
    float chargeCooldown = 0.5f;
    float temporary = 0f;
    float timeSinceLastEvade = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        evadeTimer = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        ManageEvadeTimer();
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

    private void ManageEvadeTimer()
    {
        float timeThisFrame = Time.deltaTime;
        temporary += timeThisFrame;
        timeSinceLastEvade += timeThisFrame;
        if (timeSinceLastEvade >= 100f) timeSinceLastEvade = 100f;
        if (temporary >= chargeCooldown)
        {
            evadeTimer += timeThisFrame;
            if (evadeTimer >= 3 * timeToChargeOneEvadeStack) evadeTimer = 3 * timeToChargeOneEvadeStack;
        }
    }

    private void ManageClickedButtonsNotForMoving()
    {
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Tab))
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
        wasEvadeClicked = Input.GetAxisRaw("Jump") > Mathf.Epsilon || Input.GetAxisRaw("Jump") < -Mathf.Epsilon;
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
            if (wasEvadeClicked && timeSinceLastEvade >= timeBetweenEvades && evadeTimer >= timeToChargeOneEvadeStack)
            {
                evadeTimer -= timeToChargeOneEvadeStack;
                temporary = 0;
                timeSinceLastEvade = 0;
                Vector2 whereToTPPlayer = DetermineTeleportDirection();
                gameObject.transform.position = rb.position + whereToTPPlayer * evadeMultiplicator;
            }
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }

    private Vector2 DetermineTeleportDirection()
    {
        if (movement.x > 0 && movement.y > 0) return new Vector2(0.707f, 0.707f);
        if (movement.x < 0 && movement.y < 0) return new Vector2(-0.707f, -0.707f);
        if (movement.x > 0 && movement.y < 0) return new Vector2(0.707f, -0.707f);
        if (movement.x < 0 && movement.y > 0) return new Vector2(-0.707f, 0.707f);
        if (movement.x > 0 && movement.y == 0) return new Vector2(1, 0);
        if (movement.x < 0 && movement.y == 0) return new Vector2(-1, 0);
        if (movement.x == 0 && movement.y > 0) return new Vector2(0, 1);
        if (movement.x == 0 && movement.y < 0) return new Vector2(0, -1);

        return new Vector2(0, 0);
    }

    public static float getEvadeTimer()
    {
        return evadeTimer;
    }

    public void FlatChangeSpeed(float value)
    {
        defaultSpeed += value;
        speed += value;
    }

    public void MultiplierChangeSpeed(float multiplier)
    {
        defaultSpeed *= multiplier;
        speed *= multiplier;
    }

    public float GetTimeToChargeOneEvadeStack()
    {
        return timeToChargeOneEvadeStack;
    }

    public void FlatChangeTimeToChargeOneEvadeStack(float value)
    {
        timeToChargeOneEvadeStack -= value;
    }
}