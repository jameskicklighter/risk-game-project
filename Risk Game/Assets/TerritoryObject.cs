using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoryObject {
	public GameObject territoryGameObj;
	public bool hasCastle;
	public List<TerritoryObject> adjTerritoryList;
	// add owned Player ID

	public TerritoryObject(GameObject territory, bool hasCastle) {
		this.territoryGameObj = territory;
		this.hasCastle = hasCastle;
		adjTerritoryList = new List<TerritoryObject>();
		territory.GetComponent<TerritoryManager>().SetTerritoryInstance(this);
	}

	public void AddAdjTerritory(TerritoryObject adjTerritory) {
		adjTerritoryList.Add(adjTerritory);
	}

	// Some temporary debugging only to be used after adding new territories, as they won't
	// be changing after I make sure they are correct. To use paste in AddAdjTerritory before
	// the list addition.

	// ...
		/*if (adjTerritory.territory == null) {
			Debug.Log("Game object not found when trying to add adjacency to " + territory.name);
			return;
		}
		for (int i = 0; i < adjTerritoryList.Count; i++) {
			if (adjTerritoryList.Contains(adjTerritory)) {
				Debug.Log(adjTerritory.territory.name + " has already been added to adjacency list.");
				return;
			}
		}*/
	// ...

	// More temporary debugging. Used to check and make sure if territory A's list contained B,
	// then the reverse is also true. Else our game would run into some trouble. 

	/*public void OtherHasDebug(TerritoryObject adjTerritory) {
		if (adjTerritory.adjTerritoryList.Contains(this) == false) {
			Debug.Log(adjTerritory.territory.name + " does not contain " + territory.name);
		}
	}*/
}
