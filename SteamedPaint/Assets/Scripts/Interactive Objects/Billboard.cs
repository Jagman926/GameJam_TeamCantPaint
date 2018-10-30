using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour 
{	
	[FMODUnity.EventRef]
	public string sprayPaintEvent;
	public GameObject sprayTag;
	
	public WinLevelTrigger winLevelTriggerReference;

	public bool sprayed = false;
	void OnTriggerStay2D (Collider2D col)
	{
		if (col.tag == "Player" && Input.GetButtonDown("Spray") && sprayed == false)
		{
			sprayTag.SetActive(true);
			CallSprayEvent();
			winLevelTriggerReference.boardsTagged += 1f;
			sprayed = true;
		}
	}

	void CallSprayEvent()
	{
		FMODUnity.RuntimeManager.PlayOneShot(sprayPaintEvent);
	}
	
}
