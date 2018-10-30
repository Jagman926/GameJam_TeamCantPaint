using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Player Rigidbody2D
    private Rigidbody2D rb;

    [Header("Movement Values")]
    public bool movementActive;
    [SerializeField]
    [Tooltip("Player Movement: Physics based (test around 250)")]
    private float moveSpeed = 0;
    private float targetMoveSpeed = 0;

    [Header("Jump Values")]
    [SerializeField]
    [Tooltip("Player Jump Power: Physics based (test around 30)")]
    [Range(0, 500)]
    private int jumpPower = 0;
    [SerializeField]
    [Range(0, 5)]
    private float downPower = 1.5f;

    //Jump
    [Header("Jump Settings")]
    [SerializeField]
    private bool isGrounded;
    public bool canJump;
    [SerializeField]
    private LayerMask groundLayer;

    //Climbing
    [Header("Climbing settings")]
    [SerializeField]
    private bool touchingLadder;
    [SerializeField]
    private bool climbingLadder;
    [SerializeField]
    private float climbingSpeed;
    [SerializeField]
    private float descendingSpeed;

    [Header("Falling")]
    [SerializeField]
    private float deathVelocity;
    [SerializeField]
    private bool deathFall;

    //Facing
    private bool facingRight;

    public Animator anim;

    [FMODUnity.EventRef]
    public string footstepsEvent;
    [FMODUnity.EventRef]
    public string jumpEvent;

    [FMODUnity.EventRef]
    public string landingEvent;

    [FMODUnity.EventRef]
    public string ladderEvent;
    

    private void Awake()
    {
        //Set Rigidbody2D
        anim = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //Set facing
        facingRight = true;
        //Set active movement
        movementActive = true;
        //Set can jump
        canJump = true;
        //Set Death fall
        deathFall = false;

        InvokeRepeating ("CallFootsteps", 0,  0.35f);
        InvokeRepeating ("CallLadder", 0,  0.35f);
    }

    private void Update()
    {
        if (movementActive)
        {
            //Update Player Movement
            UpdateMovement();
            //Update Player Jump
            UpdateJump();
            //Update Climb
            UpdateClimb();
            //Update Facing
            UpdateFacing();
        }
        else
        {
            //stop all movement
            rb.velocity = new Vector3(0.0f, rb.velocity.y, 0.0f);
        }
    }

    void UpdateMovement()
    {
        //Player Movement
        if (!Managers.PlayerManager.Instance.isHidden)
        {
            targetMoveSpeed = Mathf.Lerp(rb.velocity.x, Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, Time.deltaTime * 10);
            rb.velocity = new Vector2(targetMoveSpeed, rb.velocity.y);

            if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f && !Managers.PlayerManager.Instance.isHidden) 
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }
        else if (Managers.PlayerManager.Instance.isHidden)
        {
            rb.velocity = Vector2.zero;
        }
    }

    void UpdateJump()
    {
        //Check Grounding
        CheckGrounding();

        //Player Jump
        if (Input.GetButtonDown("Jump") && isGrounded && canJump)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            CallJump();
        }
    }

    void UpdateClimb()
    {
        if (Input.GetButton("Jump") && touchingLadder)
        {
            climbingLadder = true;
            rb.velocity = new Vector3(rb.velocity.x, 1.0f * climbingSpeed, rb.velocity.y);
        }
        else if (touchingLadder && !isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, -1.0f * descendingSpeed, rb.velocity.y);
        }
        else
        {
            climbingLadder = false;
        }
    }

    void UpdateFacing()
    {
        //If moving right and not facing right
        if (rb.velocity.x > 0.5f && !facingRight)
        {
            Flip();
        }
        //If moving left and not facing left
        if (rb.velocity.x < -0.5f && facingRight)
        {
            Flip();
        }
    }

    void CheckGrounding()
    {
        //Check overlap to see if on groundLayer
        isGrounded = Physics2D.OverlapArea(new Vector2(gameObject.transform.position.x - .3f * transform.localScale.x, gameObject.transform.position.y - .5f * transform.localScale.y),
            new Vector2(gameObject.transform.position.x + .3f * transform.localScale.x, gameObject.transform.position.y - .305f * transform.localScale.y), groundLayer);

        if (!isGrounded)
        {
            rb.AddForce(Vector2.down * downPower, ForceMode2D.Impulse);
            if (deathVelocity < Mathf.Abs(rb.velocity.y))
            {
                Debug.Log(rb.velocity.y);
                deathFall = true;
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ladder")
        {
            touchingLadder = true;
        }
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Ladder")
        {
            touchingLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Ladder")
        {
            touchingLadder = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Ground")
        {  
            CallLanding();
            if (deathFall)
            {
                Managers.ScenesManager.Instance.LoadLevel("Fall_Scene");
            }
        }
    }

    // AUDIO////////////////////

    void CallFootsteps()
    {
        if (anim.GetBool("isMoving") == true && isGrounded && !Managers.PlayerManager.Instance.isHidden && movementActive)
        {
            
            FMODUnity.RuntimeManager.PlayOneShot(footstepsEvent);
        }
    }

    void CallJump()
    {
        FMODUnity.RuntimeManager.PlayOneShot(jumpEvent);
    }

    void CallLanding()
    {
        FMODUnity.RuntimeManager.PlayOneShot(landingEvent);
    }

    void CallLadder()
    {
        if (climbingLadder)
        {
            FMODUnity.RuntimeManager.PlayOneShot(ladderEvent);
        }
    }
}
