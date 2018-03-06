using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPathFinding : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    public float minHeightForJump = 0.2f;

    private SpriteRenderer spriteRenderer;

    public Transform target;
    public float updateRate = 2f;
    public float speed = 300f;

    void Awake ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Mathf.Sign(target.position.x - transform.position.x);

        if (target.position.y > (transform.position.y + minHeightForJump) && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
            groundNormal.y = 1;
            groundNormal.x = 0;
        }

        if (spriteRenderer.flipX ? (move.x > 0f) : (move.x < 0f))
        {
            flipCharacter();
        }

        targetVelocity = move * maxSpeed;

        move.x = 0;
    }

    void flipCharacter()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

}
