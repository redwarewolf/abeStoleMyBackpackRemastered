using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
            groundNormal.y = 1;
            groundNormal.x = 0;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0f) : (move.x < 0f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        animator.SetFloat("velocityY", velocity.y);

        animator.SetBool("grounded", grounded);
        animator.SetBool("moving", (Input.GetKey("left") || Input.GetKey("right") || Input.GetKey("a") || Input.GetKey("d")));

        targetVelocity = move * maxSpeed;
    }
}
