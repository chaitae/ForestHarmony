using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NoteButton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public Color highlightColor;
    public Color normalColor;
    public bool pointerHovered = false;
    public string note;
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Button>().image.color = highlightColor;
        pointerHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Button>().image.color = normalColor;
        pointerHovered = false;
    }


    void Update()
    {
        if(Input.GetMouseButtonUp(0) && pointerHovered)
        {
            PlayMelodyManager.instance.PlayNote(note);
        }
    }
}
