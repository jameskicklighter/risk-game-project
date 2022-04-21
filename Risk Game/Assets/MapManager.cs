using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
	public static int keyCount = 0;
	// 48 is the fixed number of territories for this map.
	public static GameObject[] territoryArray = new GameObject[48];
	public static List<int>[] adjacentToTerritoryList = new List<int>[48];
	// Start is called before the first frame update
	void Start() {
		GameObject child;
		// Conditions should be equal. Keys 0...47.
		// The array position of a territory matches its placement in the hierarchy.
		for (int i = 0; i < territoryArray.Length && i < transform.childCount; i++) {
			child = transform.GetChild(i).gameObject;
			territoryArray[i] = child;
			child.GetComponent<TerritoryManager>().key = i;

			adjacentToTerritoryList[i] = new List<int>();
		}
		InitMapData();
	}

	// Update is called once per frame
	void Update() {

	}

	private int GetKey(string territory) {
		return GameObject.Find(territory).GetComponent<TerritoryManager>().key;
	}

	// Manually add all adjacent territories to every territory via the usage 
	// of their repective keys.
	public void InitMapData() {
		// The Gift
		adjacentToTerritoryList[0].Add(GetKey("Territory_Skagos"));
		adjacentToTerritoryList[0].Add(GetKey("Territory_Karhold"));
		adjacentToTerritoryList[0].Add(GetKey("Territory_TheDreadfort"));
		adjacentToTerritoryList[0].Add(GetKey("Territory_Winterfell"));
		// Skagos
		adjacentToTerritoryList[1].Add(GetKey("Territory_TheGift"));
		adjacentToTerritoryList[1].Add(GetKey("Territory_Karhold"));
		// Karhold
		adjacentToTerritoryList[2].Add(GetKey("Territory_TheGift"));
		adjacentToTerritoryList[2].Add(GetKey("Territory_Skagos"));
		adjacentToTerritoryList[2].Add(GetKey("Territory_TheDreadfort"));
	}
}
