using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAI : MonoBehaviour {
	private GameManager gameManager;
	private PlayerManager playerManager;
	private List<TerritoryObject> myTerritoryList;
	// Start is called before the first frame update
	void Start() {
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		playerManager = gameObject.GetComponent<PlayerManager>();
	}

	// Update is called once per frame
	void Update() {

	}

	public IEnumerator WaitToBegin() {
		yield return new WaitUntil(() => gameManager.continueClicked);
		if (gameManager.continueClicked) {
			gameManager.continueClicked = false;
			Reinforce();
			playerManager.HandleTurn(TurnState.ATTACK);
		}
	}

	public void Reinforce() {
		Debug.Log("AI is Reinforcing.");
		TerritoryObject chosenTerritory;
		TerritoryManager chosenTerritoryScript;
		myTerritoryList = playerManager.GetMyTerritoryList();
		for (int i = 0; i < playerManager.reinforcements; i++) {
			chosenTerritory = myTerritoryList[Random.Range(0, myTerritoryList.Count)];
			chosenTerritoryScript = chosenTerritory.territoryGameObj.GetComponent<TerritoryManager>();
			chosenTerritoryScript.SetArmyCount(chosenTerritoryScript.GetArmyCount() + 1);
		}
	}

	public IEnumerator WaitToAttack() {
		yield return new WaitUntil(() => gameManager.continueClicked);
		if (gameManager.continueClicked) {
			gameManager.continueClicked = false;
			Attack();
			playerManager.HandleTurn(TurnState.MANEUVER);
		}
	}

	public void Attack() {
		Debug.Log("AI is Reinforcing.");
		TerritoryObject origin = null;
		TerritoryManager originScript = null;
		TerritoryObject target = null;
		TerritoryManager targetScript = null;
		bool goodToAttack = false;
		myTerritoryList = playerManager.GetMyTerritoryList();
		int i, j, a, b;
		int preference = Random.Range(2, 6);
		for (i = Random.Range(0, myTerritoryList.Count), j = 0; 
			 j < myTerritoryList.Count;
			 i = (i + 1) % myTerritoryList.Count, j++) {
			originScript = myTerritoryList[i].territoryGameObj.GetComponent<TerritoryManager>();
			if (originScript.GetArmyCount() > preference) {
				origin = myTerritoryList[i];
				for (a = 0; a < origin.adjTerritoryList.Count; a++) {
					targetScript = origin.adjTerritoryList[a].territoryGameObj.GetComponent<TerritoryManager>();
					// Ignore territories we control for attack consideration.
					if (GameObject.Equals(targetScript.GetOwnerID(), gameManager.playerIDArray[playerManager.playerIDIndex]) == false) {
						target = origin.adjTerritoryList[a];
						goodToAttack = true;
						break;
					}
				}
				if (goodToAttack == true) {
					break;
				}
			}
		}

		// If we found a good place to attack from, find a target.
		if (goodToAttack) {
			Debug.Log("AI is attacking " + origin.territoryGameObj + " from " + target.territoryGameObj);
			PlayerManager.CommenceAttack(originScript, targetScript);
		}
	}

	public void Maneuver() {

	}
}
