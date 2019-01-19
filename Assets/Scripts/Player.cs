using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// player needs to be be connected to a rigid body that will represent
/// the interaction area
/// </summary>
public class Player : MonoBehaviour
{
    public static Player instance;
    public GameObject triggerSphere;
    public Rigidbody characterRigidBody; //this is the rigidbody that controls whole character
    public float speed;
    IInteractable focus; //current interactable
    public delegate void NotifyEvent(); //generic method to allow others to subscribe to it
    public static event NotifyEvent OnEnterInteractable;
    public static event NotifyEvent OnExitInteractable;
    public float fallMultiplier = 2.5f; //allow for instant fall
    public float lowJumpMultiplier = 2f;
    public bool facingRight = false;
    float hitDistance = 2;

    public float jumpVelocity = 5f;
    bool move = true;
    bool isGrounded = true;

    private void OnEnable()
    {
        DialogueManager.OnStartDialogue += DisablePlayerMovement;
        DialogueManager.OnEndDialogue += EnablePlayerMovement;
        
    }
    private void OnDisable()
    {
        DialogueManager.OnStartDialogue -= DisablePlayerMovement;
        DialogueManager.OnEndDialogue -= EnablePlayerMovement;
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

    }
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;

    }

    public void DisablePlayerMovement()
    {
        move = false;
    }
    public void EnablePlayerMovement()
    {
        move = true;
    }
    void CheckGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up),out hit ,hitDistance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * 1000, Color.white);
        }
    }
    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if(moveHorizontal > 0)
        {
            facingRight = true;
        }
        else if(moveHorizontal<0)
        {
            facingRight = false;
        }
        if (Input.GetButtonDown("Jump") && isGrounded) //stilld eciding on jumping with add force or velocity
        {
            characterRigidBody.velocity+= Vector3.up* jumpVelocity;

        }
        transform.LookAt(transform.position +movement); //rotates character to moving direction

        characterRigidBody.MovePosition(transform.position + movement * Time.deltaTime * speed);


        if (Input.GetKeyUp(KeyCode.E) && focus != null) //interact button
        {
            focus.Interact();
        }


        if (characterRigidBody.velocity.y < 0)
        {
            characterRigidBody.velocity += Physics.gravity * (fallMultiplier - 1) * Time.deltaTime;

        }
        else if (characterRigidBody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            characterRigidBody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


    }
    private void Update()
    {
        CheckGrounded();
    }
    void FixedUpdate()
    {
       if(move)
        {
            Move();
        }
    }
    //call interact gui
    private void OnTriggerEnter(Collider other)
    {
        IInteractable temp = other.GetComponent<IInteractable>();
        IOnEnterInteractable autoInteract = other.GetComponent<IOnEnterInteractable>();
        if(temp != null)
        {
            OnEnterInteractable?.Invoke();
            focus = temp;
        }
        if(autoInteract != null)
        {
            autoInteract.Enter();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IInteractable temp = other.GetComponent<IInteractable>();
        IOnEnterInteractable autoInteract = other.GetComponent<IOnEnterInteractable>();

        if (temp == focus)
        {
            if(temp !=null)
            temp.LeaveInteract();
            OnExitInteractable?.Invoke();
            focus = null;
        }
        if(autoInteract !=null)
        {
            autoInteract.Leave();
        }
    }
}

