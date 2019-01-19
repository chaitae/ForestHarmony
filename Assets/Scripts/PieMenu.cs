using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieMenu : MonoBehaviour
{
    public GameObject parent;
    public GameObject button;
    float distanceFromCenter = Screen.height / 10;
    Vector2[] buttonPoints;
    public RectTransform rect;
    public float  x= 100;
    public float y = 100;
    public float distance = 20;
    Vector2 centerCircle = new Vector2(0f,0f);
    Vector2 fromVector2m = new Vector2(.5f, 1.0f);
    List<Button> buttons = new List<Button>(); // because accessing button component doesnt work immediately
    //161 is the radius of the circle
    //make array of base notes
    string[] baseNotes = { "a", "b", "c", "d", "e", "f", "g" };
    void MakeButton()
    {
        button.GetComponent<RectTransform>().SetPositionAndRotation(new Vector3(Screen.height / 2, Screen.height / 2), Quaternion.identity);
        button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
        GameObject bah = Instantiate(button,parent.transform);
        bah.GetComponent<RectTransform>().anchoredPosition = new Vector2(300, 0);
        Debug.Log(bah.GetComponent<RectTransform>().anchorMax + "jfdklsg");
        Debug.Log(Screen.width);
        Debug.Log("make button");
    }
    //method makes circular menu of base notes
    void ShowBaseNotes()
    {
        //int i = 0;
        int numberNotes = 7;
        //float xoffset;
        //float yoffset;
        float angle = (2*Mathf.PI / numberNotes);
        for(int i = 0;i<numberNotes ;i++)
        {
            int copy = i;
            float x;
            float y;
            //Debug.Log(angle);
            x = Mathf.Cos(angle*i) * distance;
            y = Mathf.Sin(angle*i) * distance;
            GameObject tempNote = (GameObject)Instantiate(button, parent.transform);
            tempNote.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
            tempNote.GetComponentInChildren<Button>().onClick.AddListener(() => PlayMelodyManager.instance.PlayNote(baseNotes[numberNotes-1-copy]));
            tempNote.GetComponentInChildren<Text>().text = baseNotes[numberNotes-1-copy];
        }
    }
    private void Awake()
    {
        ShowBaseNotes();
        Button[] children = parent.GetComponentsInChildren<Button>();
        foreach(Button child in children)
        {
            buttons.Add(child);
        }
    }
    public static float FindDegree(float x, float y)
    {
        float value = (float)((Mathf.Atan2(x, y) / Mathf.PI) * 180f);
        if (value < 0) value += 360f;
        Debug.Log(value);
        return value;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(FindDegree(0, 1));

        //MakeButton();
    }
    void GetMousePositionRelativetoPie()
    {
       Vector2 mousePosition =new Vector2( Input.mousePosition.x/Screen.width,Input.mousePosition.y/Screen.height);
        float angle = Mathf.Atan2(fromVector2m.y - centerCircle.y, fromVector2m.x - centerCircle.x)-Mathf.Atan2(mousePosition.y -centerCircle.y, mousePosition.x -centerCircle.x)*Mathf.Rad2Deg;
        //angle = a
        if (angle < 0)
            angle += 360;

        //Debug.Log(angle);

        int highlightButtonIndex = (int)(angle / (360 / 7));
        buttons[7-highlightButtonIndex].Select();
        Debug.Log(7-highlightButtonIndex);
    }
    void blah()
    {

        Vector3 targetDir =  Input.mousePosition- new Vector3(Screen.width / 2, Screen.height / 2, 0);
        float angle = Vector3.Angle(targetDir, Input.mousePosition);
        Debug.Log(angle);
    }
    // Update is called once per frame
    void Update()
    {
        blah();
        //Debug.Log(Vector2.Angle(new Vector2(0, 100), Input.mousePosition));
        //Vector3 direction = Input.mousePosition - Vector3.zero;
        //Debug.Log(direction);

        //FindDegree(0-Input.mousePosition.x,0-Input.mousePosition.y);
        //GetMousePositionRelativetoPie();
        //shows default buttons
        if (Input.GetKeyUp(KeyCode.O))
         {
            //MakeButton();
            //ShowBaseNotes();
        }
        //MakeButton();
    }
}
