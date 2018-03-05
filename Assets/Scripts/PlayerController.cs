using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public BoxCollider2D attackArea;

    public Text livesUI;
    private int lives = 5;

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

        if (Input.GetKey("f"))
        {

            Debug.Log("Key Pressed F");
            attack();

        }


        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
            groundNormal.y = 1;
            groundNormal.x = 0;
        }
        else if (Input.GetButtonUp("Jump") && velocity.y > 0)
        {
            velocity.y = velocity.y * 0.5f;
        }

       
        if (spriteRenderer.flipX ? (move.x > 0f) : (move.x < 0f))
        {
            flipCharacter();
        }

        animator.SetFloat("velocityY", velocity.y);
        animator.SetBool("grounded", grounded);
        animator.SetBool("moving", (Input.GetKey("left") || Input.GetKey("right") || Input.GetKey("a") || Input.GetKey("d")));

        targetVelocity = move * maxSpeed;
    }

    void flipCharacter()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        attackArea.offset = new Vector2(attackArea.offset.x*(-1), 0);
    }

    void attack()
    {

    }

    public void receiveAttack(int damage,Transform attackerPosition)
    {
        lives = Mathf.Max(lives - damage,0);
        livesUI.text = lives.ToString();
        knockback(damage,attackerPosition);
    }

    public void knockback(int force, Transform attackerPosition)
    {
        cameraShake();
        float direction = Mathf.Sign(attackerPosition.position.x - transform.position.x);
        rb2d.AddForce(new Vector2(direction * force * 40, 1*force*30));

    }

    public void cameraShake()
    {
        //TODO: Camera Shake when attacked.
    }
}
