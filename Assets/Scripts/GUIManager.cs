using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public static GUIManager instance;
    public GameObject interactableGraphic;
    public GameObject OnScreenInstrument;
    private void OnEnable()
    {
        Player.OnEnterInteractable += PlayerEnterAction;
        Player.OnExitInteractable += PlayerExitAction;
        DialogueManager.OnStartDialogue += HideInteractableGraphic;

        //show onscreen instrument;

    }
    private void OnDisable()
    {
        Player.OnEnterInteractable -= PlayerExitAction;
        Player.OnExitInteractable -= PlayerExitAction;


    }
    public void ShowOnScreenInstrument()
    {
        OnScreenInstrument.SetActive(true);
    }
    public void HideOnScreenInstrument()
    {
        OnScreenInstrument.SetActive(false);
    }
    public void HideInteractableGraphic()
    {
        interactableGraphic.SetActive(false);
    }
    public void ShowInteractableGraphic()
    {
        interactableGraphic.SetActive(true);
    }
    void PlayerEnterAction()
    {
        interactableGraphic.SetActive(true);
    }
    void PlayerExitAction()
    {
        interactableGraphic.SetActive(false);
    }
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

}
