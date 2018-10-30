using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMapScene : MonoBehaviour {

	Managers.ScenesManager scenesManager;
	// Use this for initialization
	void Start () 
	{
		scenesManager = Managers.ScenesManager.Instance;
	}

	public void LoadStartMenu()
	{
		scenesManager.LoadStartMenu();
	}
	
	public void LoadMapScene()
	{
		scenesManager.LoadLevelMenu();
	}

	public void LoadOptionsMenu()
	{
		scenesManager.LoadOptionsMenu();
	}

	public void ExitGame()
	{
		scenesManager.ExitGame();
	}

	public void LoadScene(string sceneName)
	{
		scenesManager.LoadLevel(sceneName);
	}	
}
