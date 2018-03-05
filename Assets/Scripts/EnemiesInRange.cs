using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesInRange : MonoBehaviour {

    public PlayerController player;

    // If a new enemy enters the trigger, add it to the list of targets
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Entered my range");
            player.addEnemyInRange(other);
        }
    }

    // When an enemy exits the trigger, remove it from the list
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            player.enemyLeftRange(other);
        }
    }
}
