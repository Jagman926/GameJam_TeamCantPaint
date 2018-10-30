using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Region_MASTER", menuName = "Region/Region_MASTER", order = 1)]

public class MapRegion_MASTER : ScriptableObject {

	public List <MapRegionObject> regionData;

	public MapRegionObject currentRegion;
}
