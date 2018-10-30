using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour 
{
	
	//Player Rigidbody2D
    private Rigidbody2D rb;
	// Facing
	private bool facingRight;
	[Header ("Ai States")]
	public bool isMoving = true;
	public bool isChasingPlayer;
	

	[Header ("Detection")]
	public GameObject [] moveTargets;
	[SerializeField]
	GameObject currentTarget;

	[Header ("Chase Radius")]

	[SerializeField]
    private bool circleVisable;
    [SerializeField]
    [Range(0, 10)]
     private float chaseRadius;
    [SerializeField]
    [Range(0, 50)]
    private int segments = 50;
    LineRenderer chaseRadiusLine;

	[SerializeField]
	float moveSpeed = 0;

	float targetMoveSpeed;

	Managers.ScenesManager scenesManager;


	private void Awake()
    {
    	//Set Rigidbody2D
        rb = gameObject.GetComponent<Rigidbody2D>();
		scenesManager = Managers.ScenesManager.Instance;
    }
	void Start () 
	{
		//Set facing
        facingRight = true;
		isMoving = true;
		currentTarget = moveTargets[0];
		chaseRadiusLine = GetComponent<LineRenderer>();
        chaseRadiusLine.positionCount = segments + 1;
        chaseRadiusLine.useWorldSpace = false;
		CreateCircle();
	}
	
	
	void Update () 
	{
		UpdateMovement();
		CheckPlayerHidden();
		ChasePlayer();
		
	}
	
	void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

	void UpdateMovement()
	{
		if(isMoving)
			transform.position = Vector2.MoveTowards(transform.position, currentTarget.transform.position,moveSpeed * Time.deltaTime); // move towards the current target
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (isChasingPlayer && col.gameObject.tag == "Player" && gameObject.tag == "Enemy")
		{
			isMoving = false;
			scenesManager.LoadLevel("Busted_Scene");
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject == currentTarget && col.tag != "Player")
		{
			SwapTargets();
		}
	}

	void SwapTargets()
	{
		GameObject nextSource = moveTargets[1];
		moveTargets[1] = moveTargets[0];
		moveTargets[0] = nextSource;
		currentTarget = nextSource;
		Flip();
	}

	void CheckPlayerHidden() // ignore collisions between player and enemy if the player is hidden
	{
		if (Managers.PlayerManager.Instance.isHidden) // if the player is hidden
		{
			Physics2D.IgnoreLayerCollision(9, 11, true); // ignore collisions between layer 9 (Player) and 11 (Enemy)
		}
		else if (!Managers.PlayerManager.Instance.isHidden) // if the player is NOT hidden
		{
			Physics2D.IgnoreLayerCollision(9, 11, false); // enable collisions between layer 9 (Player) and 11 (Enemy)
		}
	}

	void ChasePlayer()
	{
		if (isChasingPlayer)
		{
			currentTarget = GameObject.Find("Player");
			ShowChaseRadius();

			if (Mathf.Abs(Vector3.Distance(currentTarget.transform.position, transform.position)) > chaseRadius)
				isChasingPlayer = false;
		}
		if (!isChasingPlayer || Managers.PlayerManager.Instance.isHidden)
		{
			HideChaseRadius();
			currentTarget = moveTargets[0];
		}
	}

	void CreateCircle()
    {
    	float x;
        float y;
        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * chaseRadius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * chaseRadius;
            chaseRadiusLine.SetPosition(i, new Vector3(x, y, 0));
            angle += (360f / segments);
        }
         HideChaseRadius();
    }

    private void ShowChaseRadius()
    {
    	chaseRadiusLine.enabled = true;
    }

    private void HideChaseRadius()
    {
        chaseRadiusLine.enabled = false;
    }
}

