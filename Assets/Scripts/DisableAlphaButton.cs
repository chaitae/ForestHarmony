using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

//example class to disable alpha button points

public class DisableAlphaButton : MonoBehaviour
{
    public Image theButton;
    public Image[] buttons;

    // Use this for initialization
    void Start()
    {
        foreach(Image button in buttons)
        {
            button.alphaHitTestMinimumThreshold = .5f;
        }

    }
}