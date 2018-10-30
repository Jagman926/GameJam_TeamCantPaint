using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCursor : MonoBehaviour {
/*
	Controls for moving the map cursor with the controller or mouse
*/
	
	float movementX; // horizontal movement input
	float movementY; // vertical movement input
	[SerializeField]
	float movementSpeed = 5; // speed of the cursor movement when a controller is being used

	void Update () 
	{
		HandleInput();
	}

	void HandleInput()
	{
		movementX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed; // initalize movementX to the input axis
		movementY = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed; // initailize movementY to its input axis

		transform.Translate(movementX, movementY, 0); // apply the movement with a simple translation
	}
}
