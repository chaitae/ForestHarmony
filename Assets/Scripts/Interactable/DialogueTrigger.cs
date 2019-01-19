using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : IInteractable
{
    int numberOfTimesConversed = 0; // used for special dialogue with different options each interaction
    public Dialogue dialogue;
    [Tooltip("optional adding activatable on dialogue end")]
    public IActivatable activatable;
    public void Interact()
    {
        numberOfTimesConversed++;
        DialogueManager.instance.StartDialogue(dialogue);
        if(activatable != null)
        DialogueManager.instance.SetActivatable(activatable);
    }

    public void LeaveInteract()
    {
        Debug.Log("leave");
    }

 
}
