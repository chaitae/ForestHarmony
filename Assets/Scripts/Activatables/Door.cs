using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour,IActivatable
{
    public Animator anim;
    [Tooltip("doorOpen is true keeps door open on active")]
    public bool doorOpen = false;
    public void DisableAction()
    {
        //closeDoor
        anim.SetTrigger("Disable");
    }

    public void EnableAction()
    {
        //openDoor
        anim.SetTrigger("Enable");
    }

    // Start is called before the first frame update
    void Start()
    {
        if(anim == null)
        {
            anim = GetComponent<Animator>();
        }
        if(doorOpen)
        {
            anim.SetTrigger("Enable");
        }
    }
}
