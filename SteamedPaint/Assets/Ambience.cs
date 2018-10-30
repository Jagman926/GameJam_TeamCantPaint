using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambience : MonoBehaviour {

	[FMODUnity.EventRef]
	public string ambienceEvent;
	void Start () {
		FMODUnity.RuntimeManager.PlayOneShot(ambienceEvent);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
