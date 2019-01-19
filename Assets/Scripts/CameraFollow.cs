using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour{

    public Transform target;
    public float smoothSpeed = .002f;
    public Vector3 offset;
    public float minX;
    public float maxX;
    public float maxY;
    public float minY;
    private Vector3 velocity = Vector3.zero;
    Vector3 smoothedPosition;
    int facingDir = 1;

    public void SetBounds(float minX, float minY,float maxX, float maxY)
    {
        if(minX != 0)
        {
            this.minX = minX;
        }
        if(minY != 0)
        {
            this.minY = minY;
        }
        if(maxY != 0)
        {
            this.maxY = maxY;
        }
        if(maxX != 0)
        {
            this.maxX = maxX;
        }

    }
    void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireCube(transform.position, new Vector3(minX*2, maxY, 1));
        //Gizmos.DrawLine(transform.position, new Vector3(smoothedPosition.x,transform.position.y,0));

    }
    private void FixedUpdate()
    {
        if (!Player.instance.facingRight)
        {
            offset.x = Mathf.Abs(offset.x) * -1;

        }
        else if (Player.instance.facingRight)
        {
            offset.x = Mathf.Abs(offset.x);

        }
        Vector3 desiredPosition = target.position + offset;
        //desiredPosition.x = Mathf.Clamp(desiredPosition.x,minX,maxX);
        //desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

        smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition,ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }


}
