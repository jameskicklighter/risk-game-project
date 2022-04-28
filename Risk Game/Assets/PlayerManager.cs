using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
	private List<TerritoryObject> territoryListAll;
	private List<TerritoryObject> territoryListMine;
	private Color playerColor;
	public int playerIDIndex;
	// Start is called before the first frame update
	private void Start() {
		territoryListAll = TerritoryObject.territoryList;
	}

	// Update is called once per frame
	private void Update() {

	}

	public Color GetColor() {
		return playerColor;
	}

	public void SetColor(Color color) {
		playerColor = color;
		//Debug.Log("Player " + playerIDIndex + "'s Color: " + color);
	}

	public int GetNumberOfTerritories() {
		return territoryListMine.Count;
	}

	public void AddTerritoryToListInitial(TerritoryObject territory) {
		if (territoryListMine == null) {
			territoryListMine = new List<TerritoryObject>();
		}
		AddTerritoryToList(territory);
		// Starting armies is 2.
		territory.territoryGameObj.GetComponent<TerritoryManager>().SetArmyCount(2);
	}

	public void AddTerritoryToList(TerritoryObject territory) {
		territoryListMine.Add(territory);
		//Debug.Log("Adding territory to player " + (playerIDIndex + 1) + ": " + territory.territoryGameObj);
		territory.territoryGameObj.GetComponent<TerritoryManager>().SetOwnerID(gameObject);
	}

	public void RemoveTerritoryFromList(TerritoryObject territory) {
		Debug.Log(territoryListMine.Count);
		territoryListMine.Remove(territory);
		Debug.Log(territoryListMine.Count);
		territory.territoryGameObj.GetComponent<TerritoryManager>().SetOwnerID(gameObject);
	}

	public IEnumerator RemoveNullItems() {
		yield return 0;
		territoryListMine.RemoveAll(item => item == null);
	}
}
