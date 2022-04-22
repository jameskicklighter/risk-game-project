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
		GameObject temp = (GameObject.Find(territory));
		if (temp == null) {
			Debug.Log(territory + " object not found.");
			return -1;
		}
		return temp.GetComponent<TerritoryManager>().key;
	}

	// Manually add all adjacent territories to every territory via the usage 
	// of their repective keys. This also includes port accessibility.
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
		// The Dreadfort
		adjacentToTerritoryList[3].Add(GetKey("Territory_TheGift"));
		adjacentToTerritoryList[3].Add(GetKey("Territory_Karhold"));
		adjacentToTerritoryList[3].Add(GetKey("Territory_Winterfell"));
		adjacentToTerritoryList[3].Add(GetKey("Territory_WidowsWatch"));
		// Winterfell
		adjacentToTerritoryList[4].Add(GetKey("Territory_TheGift"));
		adjacentToTerritoryList[4].Add(GetKey("Territory_TheDreadfort"));
		adjacentToTerritoryList[4].Add(GetKey("Territory_WidowsWatch"));
		adjacentToTerritoryList[4].Add(GetKey("Territory_WhiteHarbor"));
		adjacentToTerritoryList[4].Add(GetKey("Territory_BearIsland"));
		adjacentToTerritoryList[4].Add(GetKey("Territory_Wolfswood"));
		adjacentToTerritoryList[4].Add(GetKey("Territory_Barrowlands"));
		// Widow's Watch
		adjacentToTerritoryList[5].Add(GetKey("Territory_TheDreadfort"));
		adjacentToTerritoryList[5].Add(GetKey("Territory_Winterfell"));
		adjacentToTerritoryList[5].Add(GetKey("Territory_WhiteHarbor"));
		adjacentToTerritoryList[5].Add(GetKey("Territory_Gulltown"));
		adjacentToTerritoryList[5].Add(GetKey("Territory_Dragonstone"));
		adjacentToTerritoryList[5].Add(GetKey("Territory_KingsLanding"));
		adjacentToTerritoryList[5].Add(GetKey("Territory_StormsEnd"));
		adjacentToTerritoryList[5].Add(GetKey("Territory_Sunspear"));
		// White Harbor
		adjacentToTerritoryList[6].Add(GetKey("Territory_Winterfell"));
		adjacentToTerritoryList[6].Add(GetKey("Territory_WidowsWatch"));
		adjacentToTerritoryList[6].Add(GetKey("Territory_Barrowlands"));
		adjacentToTerritoryList[6].Add(GetKey("Territory_TheNeck"));
		adjacentToTerritoryList[6].Add(GetKey("Territory_Gulltown"));
		adjacentToTerritoryList[6].Add(GetKey("Territory_Dragonstone"));
		adjacentToTerritoryList[6].Add(GetKey("Territory_KingsLanding"));
		adjacentToTerritoryList[6].Add(GetKey("Territory_StormsEnd"));
		adjacentToTerritoryList[6].Add(GetKey("Territory_Sunspear"));
		// Bear Island
		adjacentToTerritoryList[7].Add(GetKey("Territory_Winterfell"));
		adjacentToTerritoryList[7].Add(GetKey("Territory_Wolfswood"));
		adjacentToTerritoryList[7].Add(GetKey("Territory_StoneyShore"));
		// Wolfswood
		adjacentToTerritoryList[8].Add(GetKey("Territory_Winterfell"));
		adjacentToTerritoryList[8].Add(GetKey("Territory_BearIsland"));
		adjacentToTerritoryList[8].Add(GetKey("Territory_Barrowlands"));
		adjacentToTerritoryList[8].Add(GetKey("Territory_StoneyShore"));
		adjacentToTerritoryList[8].Add(GetKey("Territory_CapeKraken"));
		adjacentToTerritoryList[8].Add(GetKey("Territory_TheTrident"));
		adjacentToTerritoryList[8].Add(GetKey("Territory_Harlaw"));
		adjacentToTerritoryList[8].Add(GetKey("Territory_Pyke"));
		adjacentToTerritoryList[8].Add(GetKey("Territory_CasterlyRock"));
		adjacentToTerritoryList[8].Add(GetKey("Territory_Oldtown"));
		// Barrowlands
		adjacentToTerritoryList[9].Add(GetKey("Territory_Winterfell"));
		adjacentToTerritoryList[9].Add(GetKey("Territory_WhiteHarbor"));
		adjacentToTerritoryList[9].Add(GetKey("Territory_Wolfswood"));
		adjacentToTerritoryList[9].Add(GetKey("Territory_StoneyShore"));
		adjacentToTerritoryList[9].Add(GetKey("Territory_BearIsland"));
		adjacentToTerritoryList[9].Add(GetKey("Territory_TheNeck"));
		// Stoney Shore
		adjacentToTerritoryList[10].Add(GetKey("Territory_BearIsland"));
		adjacentToTerritoryList[10].Add(GetKey("Territory_Wolfswood"));
		adjacentToTerritoryList[10].Add(GetKey("Territory_Barrowlands"));
		// The Neck
		adjacentToTerritoryList[11].Add(GetKey("Territory_WhiteHarbor"));
		adjacentToTerritoryList[11].Add(GetKey("Territory_Barrowlands"));
		adjacentToTerritoryList[11].Add(GetKey("Territory_CapeKraken"));
		adjacentToTerritoryList[11].Add(GetKey("Territory_TheTwins"));
		adjacentToTerritoryList[11].Add(GetKey("Territory_Harlaw"));
		// Cape Kraken
		adjacentToTerritoryList[12].Add(GetKey("Territory_TheNeck"));
		adjacentToTerritoryList[12].Add(GetKey("Territory_Wolfswood"));
		adjacentToTerritoryList[12].Add(GetKey("Territory_TheTrident"));
		adjacentToTerritoryList[12].Add(GetKey("Territory_Harlaw"));
		adjacentToTerritoryList[12].Add(GetKey("Territory_Pyke"));
		adjacentToTerritoryList[12].Add(GetKey("Territory_CasterlyRock"));
		adjacentToTerritoryList[12].Add(GetKey("Territory_Oldtown"));
		// The Fingers
		adjacentToTerritoryList[13].Add(GetKey("Territory_MountainsOfTheMoon"));
		// The Eyrie
		adjacentToTerritoryList[14].Add(GetKey("Territory_MountainsOfTheMoon"));
		adjacentToTerritoryList[14].Add(GetKey("Territory_Gulltown"));
		// Mountains of the Moon
		adjacentToTerritoryList[15].Add(GetKey("Territory_TheFingers"));
		adjacentToTerritoryList[15].Add(GetKey("Territory_TheEyrie"));
		adjacentToTerritoryList[15].Add(GetKey("Territory_TheTwins"));
		adjacentToTerritoryList[15].Add(GetKey("Territory_TheTrident"));
		adjacentToTerritoryList[15].Add(GetKey("Territory_Harrenhal"));
		// Gulltown
		adjacentToTerritoryList[16].Add(GetKey("Territory_WidowsWatch"));
		adjacentToTerritoryList[16].Add(GetKey("Territory_WhiteHarbor"));
		adjacentToTerritoryList[16].Add(GetKey("Territory_TheEyrie"));
		adjacentToTerritoryList[16].Add(GetKey("Territory_Dragonstone"));
		adjacentToTerritoryList[16].Add(GetKey("Territory_KingsLanding"));
		adjacentToTerritoryList[16].Add(GetKey("Territory_StormsEnd"));
		adjacentToTerritoryList[16].Add(GetKey("Territory_Sunspear"));
		// The Twins
		adjacentToTerritoryList[17].Add(GetKey("Territory_TheNeck"));
		adjacentToTerritoryList[17].Add(GetKey("Territory_MountainsOfTheMoon"));
		adjacentToTerritoryList[17].Add(GetKey("Territory_TheTrident"));
		adjacentToTerritoryList[17].Add(GetKey("Territory_Harlaw"));
		// The Trident
		adjacentToTerritoryList[18].Add(GetKey("Territory_Wolfswood"));
		adjacentToTerritoryList[18].Add(GetKey("Territory_CapeCraken"));
		adjacentToTerritoryList[18].Add(GetKey("Territory_MountainsOfTheMoon"));
		adjacentToTerritoryList[18].Add(GetKey("Territory_TheTwins"));
		adjacentToTerritoryList[18].Add(GetKey("Territory_Harrenhal"));
		adjacentToTerritoryList[18].Add(GetKey("Territory_Riverrun"));
		adjacentToTerritoryList[18].Add(GetKey("Territory_Harlaw"));
		adjacentToTerritoryList[18].Add(GetKey("Territory_Pyke"));
		adjacentToTerritoryList[18].Add(GetKey("Territory_CasterlyRock"));
		adjacentToTerritoryList[18].Add(GetKey("Territory_Oldtown"));
		// Harrenhal
		adjacentToTerritoryList[19].Add(GetKey("Territory_MountainsOfTheMoon"));
		adjacentToTerritoryList[19].Add(GetKey("Territory_TheTrident"));
		adjacentToTerritoryList[19].Add(GetKey("Territory_Riverrun"));
		adjacentToTerritoryList[19].Add(GetKey("Territory_Harlaw"));
	}
}
