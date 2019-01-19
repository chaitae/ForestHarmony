using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovingStates
{
    travelling,sedentary
}
public class Platform : MonoBehaviour,IActivatable
{
    public GameObject initialPoint;
    public GameObject secondPoint;
    public GameObject targetPoint; //point for gameobject to move to
    public GameObject platform;
    public MovingStates movingState = MovingStates.sedentary;
    public float speed;

    public void EnableAction()
    {
        movingState = MovingStates.travelling;
        targetPoint = secondPoint;
    }
    public void DisableAction()
    {
        movingState = MovingStates.travelling;
        targetPoint = initialPoint;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.parent = this.gameObject.transform;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        switch(movingState)
        {
            case MovingStates.travelling:
                if (Vector3.Distance(platform.transform.position, targetPoint.transform.position) > 0)
                {
                    float step = speed * Time.deltaTime;

                    //don't move
                    platform.transform.position = Vector3.MoveTowards(transform.position, targetPoint.transform.position, step);
                }
                else
                {
                    movingState = MovingStates.sedentary;
                }
                    break;
            case MovingStates.sedentary:
                break;
        }
    }
}
