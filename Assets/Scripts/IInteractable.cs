using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
    void LeaveInteract();
}
//public class Interactable:MonoBehaviour
//{
//    public float triggerDistance = 2f;
//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.yellow;
//        Gizmos.DrawWireSphere(transform.position, triggerDistance);
//    }
//    public virtual void Interact()
//    {

//    }

//}