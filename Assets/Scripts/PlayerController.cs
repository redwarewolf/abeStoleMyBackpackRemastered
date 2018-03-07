using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public BoxCollider2D attackArea;
    private List<Collider2D> targetsInRange;

    public TMP_Text livesUI;
    public TMP_Text pointsUI;
    private int points = 0;
    private int lives = 5;

    private int damage = 1;

    private float nextAttack;
    private float damagedInvulnerabilityTime;
    public float attackCd = 0.7f;
    public float damagedCd = 0.7f;

    public SpriteRenderer interactButton;
    private InteractObject interactable;

    // Use this for initialization
    void Awake()
    {
        nextAttack = Time.time;
        damagedInvulnerabilityTime = Time.time;
        targetsInRange = new List<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        pointsUI.text = points.ToString();
    }

    protected override void ComputeVelocity()
    {
        
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown("f") && Time.time >nextAttack)
        {
            attack();
        }

        if (Input.GetKeyDown("e") && interactable != null)
        {
            interact();
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
        nextAttack = Time.time + attackCd;
        foreach (BoxCollider2D target in targetsInRange)
        {
            target.GetComponent<EnemyBehaviour>().receiveAttack(damage,transform);
        }
    }

    public void receiveAttack(int damage,Transform attackerPosition)
    {
        if(Time.time > damagedInvulnerabilityTime)
        {
            damagedInvulnerabilityTime = Time.time + damagedCd;
            lives = Mathf.Max(lives - damage, 0);
            updateUI();
            knockback(damage, attackerPosition);
            StartCoroutine(InvulnerabilityColor());
            if (lives == 0)
            {
                die();
            }
        }
    }

    public void knockback(int force, Transform attackerPosition)
    {
        cameraShake();
        float direction = (Mathf.Sign(attackerPosition.position.x - transform.position.x))*(-1);
        rb2d.AddForce(new Vector2(direction * force * 80, 1*force*60));

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
        pointsUI.text = points.ToString();
    }

    private void die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void addEnemyInRange(Collider2D enemy)
    {
        if (!targetsInRange.Contains(enemy))
        {
            targetsInRange.Add(enemy);
        }

    }

    public void enemyLeftRange(Collider2D enemy)
    {
        if (targetsInRange.Contains(enemy))
        {
            targetsInRange.Remove(enemy);
        }
    }
    private IEnumerator InvulnerabilityColor()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
        yield return new WaitForSeconds(damagedCd);
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    public void enableInteractButton()
    {
        interactButton.enabled = true;
    }

    public void disableInteractButton()
    {
        interactButton.enabled = false;
    }

    public void addInteractable(InteractObject newInteractable)
    {
        interactable = newInteractable;
    }

    public void removeInteractable(InteractObject newInteractable)
    {
        interactable = null;
    }

    void interact()
    {
        interactable.interact();
    }
}
