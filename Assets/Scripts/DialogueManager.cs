using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance; //singleton so dialogue can access it easily and only need one instance of manager
    public GameObject dialogueRoot; //game object holds dialogue children
    public Text text;
    public Queue<string> sentences = new Queue<string>(); //holdes sentences sent by dialogueTrigger
    public float typingSpeed = 2.5f;
    public delegate void NotifyEvent();
    public static event NotifyEvent OnStartDialogue; 
    public static event NotifyEvent OnEndDialogue;

    IActivatable activatable; //activatable on end?

    // Start is alled before the first frame update
    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public void ShowDialogue()
    {
        //hide interactable gui
        if(OnStartDialogue != null)
        {
            OnStartDialogue();
        }
        foreach(Transform child in dialogueRoot.GetComponentInChildren<Transform>())
        {
            child.gameObject.SetActive(true);
        }
        //dialogueRoot.SetActive(true);
    }
    public void HideDialogue()
    {
        foreach (Transform child in dialogueRoot.GetComponentInChildren<Transform>())
        {
            child.gameObject.SetActive(false);
        }
    }

    public void SetActivatable(IActivatable _activatable)
    {
        activatable = _activatable;
    }
    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); //places sentences in order for queu
        }
        Debug.Log(dialogue.sentences[0]);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        else
        {
            string sentence = sentences.Dequeue();//set this string to text
            text.text = sentence;
            ShowDialogue();

        }

    }

    private void EndDialogue()
    {
        if (activatable != null)
        {
            activatable.EnableAction();
            activatable = null;
        }
        if (OnEndDialogue != null)
        {
            OnEndDialogue();
        }
        HideDialogue();
        GUIManager.instance.ShowInteractableGraphic();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }
}
