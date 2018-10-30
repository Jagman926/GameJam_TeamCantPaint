using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRegions : MonoBehaviour 
{
	/*
	Handles all player interactions with the regions on the map:
	
	1. Checks if a player moves over a region with the cursor
		If they have enough street cred to access the region, it will turn green
		Else, it will turn red
	2. Checks for selection input
		If the player has enough street cred to access the region, player goes to that level
		Else, a message is displayed saying they don't have enough street cred to access the level yet

	*/	

	[Header("Individual Region Variables")]
	[SerializeField]
	float requiredStreetCred; // amount of street cred needed to unlock the region
	[SerializeField]
	bool isLocked; // determines if the level is accessible based on the player's street cred
	[SerializeField]
	bool isHovered; // determines if the cursor is hovering over this region
	[SerializeField]
	Color color; // color of the region

	void Start()
	{
		
	}

	float playerStreetCred = 5;
	// Update is called once per frame
	void Update () 
	{
		CheckStreetCred(); // check the player's street cred
		CheckForInput(); // check if the player inputs on the map
		CheckColors();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Cursor") // if the cursor hovers over the region
		{
			isHovered = true; // set the hovering state to true
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Cursor") // if the cursor leaves the region
		{
			isHovered = false; // set the hovering state to false
		}
	}

	void CheckStreetCred()
	{
		if (playerStreetCred >= requiredStreetCred) // if the player has enough street cred
		{
			isLocked = false; // unlock the level		
		}
		else
		{
			isLocked = true; // the level is locked if the player doesn't have enough street cred
		}
	}

	void CheckForInput()
	{
		if (isHovered) // if hovering over the region
		{
			if (Input.GetButtonDown("Submit")) // if the player presses the A button 
			{
				if (isLocked == false) // if the level is not locked
				{
					// go to scene
					Debug.Log ("Going to level...");
				}
				else if (isLocked == true) // if the level is locked
				{
					// display error message
					Debug.Log ("Level locked fuck you");
				}
			}
		}
	}

	void CheckColors()
	{
		if (!isLocked) // if the player has enough street cred
		{
			GetComponent<SpriteRenderer>().color = Color.green; // make the region geen
		}
		else
		{
			GetComponent<SpriteRenderer>().color = Color.red; // make the region red
		}
	}
	
}
