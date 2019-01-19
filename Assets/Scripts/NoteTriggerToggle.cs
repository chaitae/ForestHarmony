using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTriggerToggle : NoteTrigger
{
    bool isEnabled = false;
    //public GameObject activatable;
    //Animator[] animators;
    public override void doAction()
    {
        if(isEnabled)
        {
            base.activatableGO.GetComponent<IActivatable>().EnableAction();
        }
        else
        {
           base.activatableGO.GetComponent<IActivatable>().DisableAction();
        }
        isEnabled = !isEnabled;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
