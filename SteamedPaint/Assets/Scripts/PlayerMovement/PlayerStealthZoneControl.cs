using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStealthZoneControl : MonoBehaviour {

/*
	In OnTriggerEnter:
	1. If the collided variable is a stealth zone, light up the stealth zone with a shader and set canEnterStealthZone  to true
	2. If the player presses up on the joystick while canEnterStealthZone is true, play little animation and set "isHidden" from playerManager to true
	3. If isHidden is true and the player presses down on the joystick, play animation and set isHidden to false;
*/
	[SerializeField]
	float y; // used for the y input axis
	[FMODUnity.EventRef]
    public string hideEvent;
    [FMODUnity.EventRef]
    public string unHideEvent;

	[SerializeField]
    Color hiddenColor;
	Color startColor;
	void Start()
	{
		startColor = GetComponent<SpriteRenderer>().color;
	}

	void Update () 
	{
		ButtonInput();
	}

	void ButtonInput()
	{
		

		if (Managers.PlayerManager.Instance.canEnterStealthZone == true) // if player can enter a stealth zone
		{
			/* if (Input.GetButtonDown("Hide") && !Managers.PlayerManager.Instance.isHidden) // if pushing button
			{
				Managers.PlayerManager.Instance.isHidden = true; // set the player to the hidden state
				Debug.Log("Hidden State = " + Managers.PlayerManager.Instance.isHidden);
				// play animation
			}
			if (Input.GetButtonDown("Hide") && Managers.PlayerManager.Instance.isHidden)
			{
				Managers.PlayerManager.Instance.isHidden = false; // set the player to the hidden state
				Debug.Log("Hidden State = " + Managers.PlayerManager.Instance.isHidden);
				// play animation
			}
			*/
			if (Input.GetButtonDown("Hide"))
			{
				Managers.PlayerManager.Instance.isHidden = !Managers.PlayerManager.Instance.isHidden;
				if (Managers.PlayerManager.Instance.isHidden)
				{
					CallHide();
					
				}
				else if (!Managers.PlayerManager.Instance.isHidden)
				{
					CallUnHide();
				}
			}
		}
	}
	void OnTriggerEnter2D(Collider2D col) // will allow player to enter
	{
		if (col.tag == "Stealth Zone")
		{
			Managers.PlayerManager.Instance.canEnterStealthZone = true;
			Debug.Log("Can Enter Stealth zone = " + Managers.PlayerManager.Instance.canEnterStealthZone); 
			// highlight stealth zone with a shader
		}
	}

	void OnTriggerExit2D(Collider2D col) // will prevent player from entering
	{
		if (col.tag == "Stealth Zone")
		{
			Managers.PlayerManager.Instance.canEnterStealthZone = false;
			Debug.Log("Can Enter Stealth zone = " + Managers.PlayerManager.Instance.canEnterStealthZone); 
			if (Managers.PlayerManager.Instance.isHidden)
				Managers.PlayerManager.Instance.isHidden = false;
			// change shader back to normal
		}
	}

	void CallHide()
	{
		FMODUnity.RuntimeManager.PlayOneShot(hideEvent);
		GetComponent<SpriteRenderer>().color = hiddenColor;
	}

	void CallUnHide()
	{
		FMODUnity.RuntimeManager.PlayOneShot(unHideEvent);
		GetComponent<SpriteRenderer>().color = startColor;
	}
}
