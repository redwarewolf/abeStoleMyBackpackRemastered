using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public int lives = 2;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void receiveAttack(int damage,Transform attackerPosition)
    {
        lives = Mathf.Max(lives - damage, 0);
        if(lives == 0)
        {
            die();
        }
        knockback(damage, attackerPosition);
    }

    public void knockback(int force, Transform attackerPosition)
    {
        float direction = (Mathf.Sign(attackerPosition.position.x - transform.position.x)) * (-1);
        rb2d.AddForce(new Vector2(direction * force * 40, 1 * force * 30));

    }


    public void die()
    {
        Debug.Log("Enemy Died");
        Destroy(this.gameObject);
    }
}
