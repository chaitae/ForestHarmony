using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour,IOnEnterInteractable
{
    void SetSpawnPoint()
    {
        PlayerStats.spawnPoint = Player.instance.transform.parent.position;
    }
    public void Enter()
    {
        SetSpawnPoint();
    }

    public void Leave()
    {
        //throw new System.NotImplementedException();
    }

    //private void OnTriggerEnter(Collider other)
    //{

    //}
    void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
