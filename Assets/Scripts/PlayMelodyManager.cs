using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
//rename file to instrument manager
//this class plays melodies with either player instrument or monster instrument
//should change to play melody manager

public class PlayMelodyManager : MonoBehaviour
{
    //add the sharp and flat values to notePitches
    public Dictionary<string, int> notePitches = new Dictionary<string, int>()
                                {
                                    {"a",0},{"a#",1},{"ab",-1},
                                    {"b", 2},{"bb",1},
                                    {"c",3},{"c#",4},
                                    {"d",5},{"d#",6},{"db",4},
                                    {"e",7 },{"eb",6},
                                    {"f",8 },{"f#",9},
                                    {"g",10 },{"g#",11},{"gb",9},
                                };

    float transpose = 0; //transposes relative to a4 in semi tones
    int halfNote = 0; //half note increase or decrease for sharp and flats
    float currentBeatDuration = MusicBeats.quarterNote;
    public PitchValue[] melody;
    AudioSource playerInstrument;
    AudioSource tempAudioSource; // temp audiosource represents instruments of npcs that will sing using this code
    public static PlayMelodyManager instance;
    public delegate void PlayNoteAction(string note);
    public static event PlayNoteAction OnPlayedNote;
    //public delegate void NotifyEvent();
    //public static event NotifyEvent OnMelodyComplete;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    //recieves melody in raw format,reformates melody, and plays with audiosource given
    public void PlayMelody(string melody, AudioSource _tempAudio)
    {
        string[] strArr = melody.Split(' ');
        tempAudioSource = _tempAudio;
        StartCoroutine(WaitAndPlayNotes(strArr));
    }
    bool HasSharp(string note)
    {
        if (note == "b") return false;
        if (note == "e") return false;
        return true;
    }
    bool HasFlat(string note)
    {
        if(note == "f")
        {
            return false;
        }
        if(note == "c")
        {
            return false;
        }
        return true;

    }
    //recieves note to pbe played by player instrument
    public void PlayNote(string _note)
    {
        OnPlayedNote?.Invoke(_note);

        int note = 0;
            note = notePitches[_note];
            playerInstrument.pitch = Mathf.Pow(2, (note + transpose + halfNote) / 12.0f);
            playerInstrument.Play();
    }
    //disable playnote

    //plays string of notes input in it waiting .5f seconds between each note plays what is pushed through which is sometimes melody/notes
    IEnumerator WaitAndPlayNotes(string[] notes)
    {
        GUIManager.instance.HideOnScreenInstrument();

        int i = 0;
        int note = 0;
        while (i < notes.Length)
        {
            yield return new WaitForSeconds(.5f);
            note = notePitches[notes[i]];
            tempAudioSource.pitch = Mathf.Pow(2, (note + transpose + halfNote) / 12.0f);
            note++;
            i++;
            tempAudioSource.Play();

        }
        GUIManager.instance.ShowOnScreenInstrument();

        //if(OnMelodyComplete!=null)
        //{
        //    OnMelodyComplete();
        //}
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        playerInstrument=GetComponent<AudioSource>();
    }


}
