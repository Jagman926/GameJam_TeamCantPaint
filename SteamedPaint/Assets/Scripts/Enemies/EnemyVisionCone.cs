using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisionCone : MonoBehaviour 
{
	/*
		1. Check if player is hidden using isHidden from playerManager
		2. If hidde, ignore getting caught and layer collisions
		3. Otherwise, player gets caught when they enter the spotlight
	*/
	public EnemyMovement enemScriptRef;
	void OnTriggerEnter2D(Collider2D col)	
	{
		if (Managers.PlayerManager.Instance.isHidden == false)
		{
			if (col.tag == "Player")
			{
				Debug.Log("DETECTED!");
				enemScriptRef.isChasingPlayer = true;
			}
		}
	}
	
}
