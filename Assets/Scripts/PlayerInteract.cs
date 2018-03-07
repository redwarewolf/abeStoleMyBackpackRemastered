using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {

    public PlayerController player;

    void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Detected Collision. Interactable?");
        if (collision.CompareTag("Interactable"))
        {
            Debug.Log("Collision was interactable");
            player.enableInteractButton();
            player.addInteractable(collision.GetComponent<InteractObject>());

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            player.disableInteractButton();
            player.removeInteractable(collision.GetComponent<InteractObject>());

        }
    }
}
