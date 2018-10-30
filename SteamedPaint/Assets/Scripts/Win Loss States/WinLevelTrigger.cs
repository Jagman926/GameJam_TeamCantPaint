using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WinLevelTrigger : MonoBehaviour {

	// Use this for initialization
	public float streetCredIncrease; // amount street cred is increased by when the level is completed
	public float boardsTagged;
	public float boardsNeeded;
	
	Managers.ScenesManager scenesManager;

	public GameObject boardsTaggedCount;
	// Use this for initialization
	void Start () 
	{
		scenesManager = Managers.ScenesManager.Instance;
		boardsNeeded = 5f;
		boardsTaggedCount = GameObject.Find("Sprayed CountText");
	}

	void Update()
	{
		boardsTaggedCount.GetComponent<Text>().text = boardsTagged.ToString();
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player" && boardsTagged >= boardsNeeded)
		{
			Managers.LevelManager.playerStreetCred += streetCredIncrease; // increment player street cred
			scenesManager.LoadLevelMenu();
		}
	}
}
