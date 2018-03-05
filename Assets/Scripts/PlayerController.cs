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
    private List<Collider2D> targetsInRange;

    public Text livesUI;
    public Text pointsUI;
    private int points = 0;
    private int lives = 5;

    private int damage = 1;

    // Use this for initialization
    void Awake()
    {
        targetsInRange = new List<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        pointsUI.text = points.ToString();
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
        foreach(BoxCollider2D target in targetsInRange)
        {
            target.GetComponent<EnemyBehaviour>().receiveAttack(damage,transform);
        }
    }

    public void receiveAttack(int damage,Transform attackerPosition)
    {
        lives = Mathf.Max(lives - damage,0);
        updateUI();
        knockback(damage,attackerPosition);
    }

    public void knockback(int force, Transform attackerPosition)
    {
        cameraShake();
        float direction = (Mathf.Sign(attackerPosition.position.x - transform.position.x))*(-1);
        rb2d.AddForce(new Vector2(direction * force * 40, 1*force*30));

    }

    public void cameraShake()
    {
        //TODO: Camera Shake when attacked.
    }

    public void earnPoints(int pointsAmmount)
    {
        points += pointsAmmount;
        updateUI();
    }
    private void updateUI()
    {
        livesUI.text = lives.ToString();
        pointsUI.text = pointsUI.ToString();
    }

    private void die()
    {
       
    }

    public void addEnemyInRange(Collider2D enemy)
    {
        if (!targetsInRange.Contains(enemy))
        {
            targetsInRange.Add(enemy);
        }
        Debug.Log("Enemies In range:" + targetsInRange.Count);
    }

    public void enemyLeftRange(Collider2D enemy)
    {
        if (targetsInRange.Contains(enemy))
        {
            targetsInRange.Remove(enemy);
        }
    }
}
