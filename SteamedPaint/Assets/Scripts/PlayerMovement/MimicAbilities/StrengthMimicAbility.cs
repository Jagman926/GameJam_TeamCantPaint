using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthMimicAbility : MonoBehaviour
{
    //Mimic RigidBody2D
    private Rigidbody2D rb;
    //Mimic Movement script
    private PlayerMovement movementScript;

    [Header("Object Pulling")]
    [SerializeField]
    private bool touchingPullObject;
    [SerializeField]
    private GameObject touchingMovableObject;
    private Rigidbody2D movableRB;
    [SerializeField]
    private bool pullingObject;

    [FMODUnity.EventRef]
    public string pushSound;

    string pushBusString = "Bus:/Push";
    FMOD.Studio.Bus pushBus;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        movementScript = gameObject.GetComponent<PlayerMovement>();
        
    }

    void Update()
    {
        CheckMoveObject();
    }

    private void CheckMoveObject()
    {
        if (Input.GetButton("Pull") && touchingPullObject)
        {
            MoveObject();
            movementScript.canJump = false;
        }
        else
        {
            pullingObject = false;
            movementScript.canJump = true;
        }
    }

    private void MoveObject()
    {
        pullingObject = true;
        movableRB.constraints = RigidbodyConstraints2D.None;
        movableRB.velocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "HeavyMove")
        {
            touchingPullObject = true;
            touchingMovableObject = col.gameObject;
            movableRB = touchingMovableObject.GetComponent<Rigidbody2D>();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "HeavyMove")
        {
            touchingPullObject = false;
            touchingMovableObject = null;
            movableRB = null;
        }
    }

    void CallPush()
    {
        if (pullingObject)
            FMODUnity.RuntimeManager.PlayOneShot(pushSound);
    }
}
