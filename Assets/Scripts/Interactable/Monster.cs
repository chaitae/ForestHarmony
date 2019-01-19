using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]



public class Monster : MonoBehaviour, IInteractable
{
    public float triggerDistance = 2f;
    string currentMeasure = "a b a";
    public string melody = "a b a|c d d a|f g a"; //measure is split by | each note split by a space
    string[] measures;
    int noteIndex = 0;
    int measureIndex = 0;
    bool isListening = false;
    bool melodyCompleted = false; //indicates actions have been completed
    AudioSource monsterVoice;
    public AudioClip correctMelodySound;
    public AudioClip correctMeasureSound;
    public AudioClip voice;
    //bool playerEntered = false;
    [Tooltip("Activatable object that will be activated on melody correctly played")]
    public GameObject activatableGameObject;
    //animator should be included
    private void OnEnable()
    {
        PlayMelodyManager.OnPlayedNote += Listen;
        //PlayMelodyManager.OnMelodyComplete += MelodyPlayed;
    }

    private void OnDisable()
    {
        PlayMelodyManager.OnPlayedNote -= Listen;
        //PlayMelodyManager.OnMelodyComplete -= MelodyPlayed;


    }
    private IEnumerator WaitForMelodyToPlayAndDoAction(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (activatableGameObject != null)
        {
            //wait till you play melody then you can do iactivatable
            activatableGameObject.GetComponent<IActivatable>().EnableAction();
        }
    }


    //set listen to true
    public void LeaveInteract()
    {
            isListening = false;
    }
        //play melody
    public void Interact()
    {
        //show listening gui
        GUIManager.instance.HideInteractableGraphic();
        //Player.instance.DisablePlayerMovement();
        isListening = true;
        Sing();

    }
    void Start()
    {
        monsterVoice =GetComponent<AudioSource>();
        monsterVoice.clip.LoadAudioData();
        measures = melody.Split('|');
        currentMeasure = measures[0];
    }
    
    //correctMeldoyAction should call function with master delegate to notify melody completed
    //or maybe each mosnter has special action?***
    void CorrectMelodyAction()
    {
        
        Debug.Log("correct melody");
        PlayMelody();
        //at end of play melody kill self? Generate new melody?
        //play animation
        melodyCompleted = true;
        //play scene

    }
    //correctMeasureAction call game manager?
    //no each mosnter has individual measure actions and sounds
    void CorrectMeasureAction()
    {
        Debug.Log("Measure correct");
        //Sing();
        if (measureIndex == measures.Length-1)
        {
            CorrectMelodyAction();
        }
        else
        {
        //measureIndex increased while sing() working
            noteIndex = 0;
            measureIndex++;
            currentMeasure = measures[measureIndex];
            Sing();
        }

    }
    //either call correct note action delegate?
    void CorrectNoteAction()
    {
        //Player.instance.EnablePlayerMovement();

        Debug.Log("correct");
        string[] musicNotes = currentMeasure.Split(' ');
        if (noteIndex == musicNotes.Length-1)
        {
            CorrectMeasureAction();
        }
        else
        {
            noteIndex++;
        }
    }
    void IncorrectNoteAction()
    {
        Debug.Log("wrong");
        Sing();
        noteIndex = 0;
    }
    //if is listening this method recieves information from instrument/aka user input
    //replace input.inputString with note
    void Listen(string note)
    {
        Debug.Log("islistneing:" + isListening);
        if(isListening && !melodyCompleted)
        {
            string[] musicNotes = currentMeasure.Split(' ');
            if (note == musicNotes[noteIndex].ToString())
            {
                CorrectNoteAction();
            }
            else if (note != musicNotes[noteIndex].ToString())
            {
                IncorrectNoteAction();
            }
        }
     
    }
    void PlayCorrectMeasureSound()
    {
        Sing();
    }
    void PlayCorrectMelodySound()
    {
        //set clip to melody sound
        //setclipback to voice
    }
    void PlayMelody()
    {
        string temp = melody;
        temp = temp.Replace("|"," ");
        //show melody
        Debug.Log(temp);
        PlayMelodyManager.instance.PlayMelody(temp, monsterVoice);
        float melodyTime = (temp.Length-1) * .5f;
        StartCoroutine(WaitForMelodyToPlayAndDoAction(melodyTime));

    }
    IEnumerator PauseAndPlayMelody(string melody)
    {
        isListening = false;
        yield return new WaitForSeconds(1f);
        PlayMelodyManager.instance.PlayMelody(melody, monsterVoice);
        yield return new WaitForSeconds(melody.Length*.5f);
        isListening = true;
    }
    //play song
    void Sing()
    {
        IEnumerator coroutine;
        coroutine = PauseAndPlayMelody(currentMeasure);
        StartCoroutine(coroutine);
        Debug.Log(currentMeasure);
    }

 
}
