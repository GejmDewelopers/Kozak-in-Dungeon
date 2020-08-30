using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    Animator animator;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        if (Input.GetAxisRaw("Horizontal") > 0.1f)
        {
            position.x += speed * Time.fixedDeltaTime;
            transform.position = position;
            spriteRenderer.flipX = false;
        }
        if (Input.GetAxisRaw("Horizontal") < -0.1f)
        {
            position.x -= speed * Time.fixedDeltaTime;
            transform.position = position;
            spriteRenderer.flipX = true;
        }
        if (Input.GetAxisRaw("Vertical") > 0.1f)
        {
            position.y += speed * Time.fixedDeltaTime;
            transform.position = position;
        }
        if (Input.GetAxisRaw("Vertical") < -0.1f)
        {
            position.y -= speed * Time.fixedDeltaTime;
            transform.position = position;
        }
    }
}
