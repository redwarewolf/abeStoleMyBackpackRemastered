using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public Transform myPosition;
    public int damage = 2;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Detecte una colision");
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("La colision fue un player");
            other.gameObject.GetComponent<PlayerController>().receiveAttack(damage, myPosition);
        }
    }
}
