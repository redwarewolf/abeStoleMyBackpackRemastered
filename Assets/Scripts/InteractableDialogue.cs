using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDialogue : InteractObject {

    public Dialogue dialogue;

    public override void interact()
    {
        FindObjectOfType<DialogueManager>().startDialogue(dialogue);
    }

}
