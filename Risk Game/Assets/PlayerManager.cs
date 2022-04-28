using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
	private List<TerritoryObject> territoryListAll;
	private List<TerritoryObject> territoryListMine;
	private Color playerColor;
	public int playerIDIndex;
	public static int turnPlayerIndex;
	public static TurnState turnState = TurnState.DONE;
	public int reinforcements;
	private GameManager gameManager;
	private SimpleAI ai;
	public static int[] attackingRolls;
	public static int[] defendingRolls;

	// Start is called before the first frame update
	private void Start() {
		territoryListAll = TerritoryObject.territoryList;
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		attackingRolls = new int[3];
		defendingRolls = new int[2];
		if (CompareTag("Computer"))
			ai = GetComponent<SimpleAI>();
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

	public List<TerritoryObject> GetMyTerritoryList() {
		return territoryListMine;
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
		//Debug.Log(territoryListMine.Count);
		territoryListMine.Remove(territory);
		//Debug.Log(territoryListMine.Count);
		//territory.territoryGameObj.GetComponent<TerritoryManager>().SetOwnerID(gameObject);
	}

	public IEnumerator RemoveNullItems() {
		yield return 0;
		territoryListMine.RemoveAll(item => item == null);
	}

	public int CalculateReinforcements() {
		return Mathf.Max(3, territoryListMine.Count / 3);
	}

	public void HandleTurn(TurnState turnState) {
		PlayerManager.turnState = turnState;
		bool isHuman = CompareTag("Human");
		if (isHuman) {
			switch (turnState) {
				case TurnState.REINFORCE:
					reinforcements = CalculateReinforcements();
					Debug.Log(reinforcements);
					Reinforce();
					StartCoroutine(WaitToAttack());
					break;
				case TurnState.ATTACK:
					gameManager.continueButtonObj.SetActive(true);
					StartCoroutine(WaitToManeuver());
					break;
				case TurnState.MANEUVER:
					StartCoroutine(WaitToEndTurn());
					break;
				case TurnState.DONE:
					switch (gameManager.turnPlayer) {
						case TurnPlayer.P1:
							gameManager.StartTurn(TurnPlayer.P2);
							break;
						case TurnPlayer.P2:
							gameManager.StartTurn(TurnPlayer.P3);
							break;
						case TurnPlayer.P3:
							gameManager.StartTurn(TurnPlayer.P4);
							break;
						case TurnPlayer.P4:
							gameManager.StartTurn(TurnPlayer.P5);
							break;
						case TurnPlayer.P5:
							gameManager.StartTurn(TurnPlayer.P1);
							break;
					}
					break;
			}
		}
		else {
			switch (turnState) {
				case TurnState.REINFORCE:
					reinforcements = CalculateReinforcements();
					StartCoroutine(ai.WaitToBegin());
					break;
				case TurnState.ATTACK:
					StartCoroutine(ai.WaitToAttack());
					break;
				case TurnState.MANEUVER:
					ai.Maneuver();
					break;
				case TurnState.DONE:
					switch (gameManager.turnPlayer) {
						case TurnPlayer.P1:
							gameManager.StartTurn(TurnPlayer.P2);
							break;
						case TurnPlayer.P2:
							gameManager.StartTurn(TurnPlayer.P3);
							break;
						case TurnPlayer.P3:
							gameManager.StartTurn(TurnPlayer.P4);
							break;
						case TurnPlayer.P4:
							gameManager.StartTurn(TurnPlayer.P5);
							break;
						case TurnPlayer.P5:
							gameManager.StartTurn(TurnPlayer.P1);
							break;
					}
					break;
			}
		}
	}

	public void Reinforce() {
		Debug.Log("Reinforcement Phase beginning.");
		TerritoryManager.IncrementHighlightCounter();
		territoryListMine.ForEach(delegate (TerritoryObject territory) {
			territory.territoryGameObj.GetComponent<TerritoryManager>().HighlightBorder(playerColor);
		});
	}

	IEnumerator WaitToAttack() {
		while (reinforcements > 0) {
			yield return new WaitUntil(() => reinforcements == 0);
		}
		if (reinforcements == 0) {
			Debug.Log("Done reinforcing. Attack Phase begin.");
			TerritoryManager.selectedTerritory = null;
			HandleTurn(TurnState.ATTACK);
		}
	}

	public static int[] RollDicePerArmy(int[] rolls, int numArmies) {
		int i, temp;
		// Initialize
		for (i = 0; i < numArmies; i++) {
			rolls[i] = Random.Range(1, 7);
		}
		// Order decreasing.
		for (i = 0; i < numArmies - 1; i++) {
			if (rolls[i] < rolls[i + 1]) {
				temp = rolls[i];
				rolls[i] = rolls[i + 1];
				rolls[i + 1] = temp;
			}
		}
		if (numArmies == 3) {
			if (rolls[0] < rolls[1]) {
				temp = rolls[0];
				rolls[0] = rolls[1];
				rolls[1] = temp;
			}
		}
		for (i = 0; i < numArmies; i++) {
			Debug.Log("Rolls[" + i + "]: " + rolls[i]);
		}
		return rolls;
	}

	public static void CommenceAttack(TerritoryManager attackingTerrComponent, TerritoryManager defendingTerrComponent) {
		int attackingTerrArmies = attackingTerrComponent.GetArmyCount(); // This must be >=2 due to indirect checks in TerritoryManager.OnMouseDown()
		int defendingTerrArmies = defendingTerrComponent.GetArmyCount();
		int attackingWithArmies = Mathf.Min(3, attackingTerrArmies - 1);
		int defendingWithArmies = Mathf.Min(2, defendingTerrArmies);
		int attackingArmiesLost = 0;

		Debug.Log("Attacking Rolls with: " + attackingWithArmies);
		int[] attackingDice = RollDicePerArmy(attackingRolls, attackingWithArmies);
		Debug.Log("Defending Rolls: with: " + defendingWithArmies);
		int[] defendingDice = RollDicePerArmy(defendingRolls, defendingWithArmies);
		for (int i = 0; i < Mathf.Min(attackingWithArmies, defendingWithArmies); i++) {
			if (defendingDice[i] < attackingDice[i]) {
				defendingTerrArmies--;
				if (defendingTerrArmies == 0)
					break;
			}
			else {
				attackingArmiesLost++;
				attackingTerrArmies--;
			}
		}
		Debug.Log("Attacking" + attackingTerrArmies);
		Debug.Log("Defending" + defendingTerrArmies);
		attackingTerrComponent.SetArmyCount(attackingTerrArmies);
		defendingTerrComponent.SetArmyCount(defendingTerrArmies);
		if (defendingTerrArmies == 0) {
			defendingTerrComponent.GetOwnerID().GetComponent<PlayerManager>().RemoveTerritoryFromList(defendingTerrComponent.GetTerritoryInstance());
			attackingTerrComponent.GetOwnerID().GetComponent<PlayerManager>().AddTerritoryToList(defendingTerrComponent.GetTerritoryInstance());
			defendingTerrComponent.SetBorderColor(attackingTerrComponent.GetOwnerID().GetComponent<PlayerManager>().playerColor);
			// Move troops into conquered land.
			defendingTerrComponent.SetArmyCount(attackingWithArmies - attackingArmiesLost);
			attackingTerrComponent.SetArmyCount(attackingTerrArmies - defendingTerrComponent.GetArmyCount());
		}
	}

	IEnumerator WaitToManeuver() {
		while (gameManager.continueClicked == false) {
			yield return new WaitUntil(() => gameManager.continueClicked == true);
		}
		if (gameManager.continueClicked == true) {
			gameManager.continueClicked = false; // Reset it.
			Debug.Log("Done Attacking. Maneuver Phase begin.");
			TerritoryManager.selectedTerritory = null;
			TerritoryManager.territoryToAttack = null;
			TerritoryManager.IncrementHighlightCounter();
			HandleTurn(TurnState.MANEUVER);
		}
	}

	public static void CommenceManeuver(TerritoryManager startingTerrComponent, TerritoryManager endingTerrComponent) {
		int startingTerrArmies = startingTerrComponent.GetArmyCount();
		int endingTerrArmies = endingTerrComponent.GetArmyCount();
		startingTerrComponent.SetArmyCount(startingTerrArmies - 1);
		endingTerrComponent.SetArmyCount(endingTerrArmies + 1);
	}

	IEnumerator WaitToEndTurn() {
		while (gameManager.continueClicked == false) {
			yield return new WaitUntil(() => gameManager.continueClicked == true);
		}
		if (gameManager.continueClicked == true) {
			gameManager.continueClicked = false; // Reset it.
			Debug.Log("Done with turn.");
			TerritoryManager.selectedTerritory = null;
			TerritoryManager.territoryToMoveTo = null;
			TerritoryManager.IncrementHighlightCounter();
			HandleTurn(TurnState.DONE);
		}
	}
}
