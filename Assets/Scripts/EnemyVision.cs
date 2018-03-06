using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour {

    public AIPathFinding myPathFinding;
    public int visionRadius = 4;
    public Animator animator;

    private void Awake()
    {
        GetComponent<CircleCollider2D>().radius = visionRadius;
        myPathFinding.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player entered my vision radius");
            myPathFinding.enabled = true;
            animator.SetBool("followingPlayer", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player left my vision radius");
            myPathFinding.enabled = false;
            animator.SetBool("followingPlayer", false);
        }
    }
}
