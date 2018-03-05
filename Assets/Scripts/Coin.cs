using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private int points = 1;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Detecte una colision");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("La colision fue un player");
            other.gameObject.GetComponent<PlayerController>().earnPoints(points);
        }
    }
}
