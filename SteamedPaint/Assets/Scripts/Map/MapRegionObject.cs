using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "Region", menuName = "Region/NewRegion", order = 2)]
public class MapRegionObject : ScriptableObject {

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
	
	public float requiredStreetCred; // amount of street cred needed to unlock the region
	
	public bool isLocked; // determines if the level is accessible based on the player's street cred
	
	public bool isHovered; // determines if the cursor is hovering over this region
	
	public Color color; // color of the region

	public string regionName;



}
