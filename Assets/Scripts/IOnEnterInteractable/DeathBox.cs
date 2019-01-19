using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour,IOnEnterInteractable
{
    public void Enter()
    {
        Player.instance.gameObject.transform.parent.position = PlayerStats.spawnPoint; // getting player instance game object refers to child so must get parent
    }

    public void Leave()
    {
    }
  
}
