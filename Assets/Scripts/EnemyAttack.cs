using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public Transform myPosition;
    public int damage = 2;
    public float attackCD = 0.7f;
    private float nextAttack;

    public CameraShake cameraShake;

    private void Awake()
    {
        nextAttack = Time.time;
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if(Time.time > nextAttack && other.gameObject.tag == "Player")
        {        
            Debug.Log("Enemigo colisiono con un player");
            nextAttack = Time.time + attackCD;
            other.gameObject.GetComponent<PlayerController>().receiveAttack(damage, myPosition);
            //StartCoroutine(cameraShake.Shake(0.5f, 0.5f));

        }  
    }
}
