using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//acts as "weight" plate when character walks through game object it will trigger
public class WeightPlate : MonoBehaviour,IOnEnterInteractable
{
    public GameObject activatableGO; //pass activatable gameobject to run in disable and enable action
    IActivatable activatable;
    public bool useEnableAction = true;
    public void Enter()
    {
        if (useEnableAction)
            activatable.EnableAction();
        else
        {
            activatable.DisableAction();
        }
    }

    public void Leave()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;

        activatable = activatableGO.GetComponent<IActivatable>();
    }


}
