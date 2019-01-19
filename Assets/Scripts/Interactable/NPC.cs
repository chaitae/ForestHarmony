using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour,IInteractable
{
    DialogueTrigger dialogueTrigger = new DialogueTrigger(); //utlilizes dialogue trigger
    public Dialogue dialogue;

    public void Interact()
    {
        dialogueTrigger.dialogue = dialogue; //set dialogue
        dialogueTrigger.Interact(); // need to implement dialogue interact within npc to trigger
    }

    public void LeaveInteract()
    {
        dialogueTrigger.LeaveInteract();
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueTrigger.dialogue = dialogue;
    }
}
