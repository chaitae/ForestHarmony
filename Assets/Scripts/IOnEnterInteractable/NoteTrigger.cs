using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NoteTrigger : MonoBehaviour,IOnEnterInteractable
{
    public bool isListening = false;
    public string triggerNote = "a";
    protected string actionEnableTrigger = "Enable"; //need to set enable trigger for all switch interactables which activates through note trigger
    protected string actionDisableTrigger = "Disable";
    public GameObject activatableGO;
    public Text notation;

    public IActivatable activatable;

    public void OnEnable()
    {
        PlayMelodyManager.OnPlayedNote += Listen;
    }
    public void Enter()
    {
        isListening = true;
    }
    public virtual void doAction() //overrided for various switches perhaps
    {
        activatable.EnableAction();
     
    }

    void Listen(string _note)
    {
        if(isListening)
        {
            if(_note == triggerNote)
            {
                doAction();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        notation = GetComponentInChildren<Text>();
        notation.text = triggerNote;
        activatable = activatableGO.GetComponent<IActivatable>();
    }
    

    public void Leave()
    {
        isListening = false;
    }
}
