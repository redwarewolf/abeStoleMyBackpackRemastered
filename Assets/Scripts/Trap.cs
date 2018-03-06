using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    public Transform myPosition;
    public int damage = 1;

    private void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log("Detecte una colision");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("La colision fue un player");
            other.gameObject.GetComponent<PlayerController>().receiveAttack(damage, myPosition);

        }
    }
}
