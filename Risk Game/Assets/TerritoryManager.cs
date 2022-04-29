using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TerritoryManager : MonoBehaviour {
	private PolygonCollider2D coll;
	private LineRenderer border;
	private TerritoryObject territory;
	public static GameObject selectedTerritory;
	public static GameObject territoryToAttack;
	public static GameObject territoryToMoveTo;
	public GameObject armyCountCirclePrefab;
	private GameObject armyCountCircle;
	private SpriteRenderer armyCountCircleRenderer;
	private int armyCount;
	public static int highlightIntTracker = 0;
	public int highlightInt = 2;
	private GameObject ownerID;
	private Color defaultColor, highlightColor, adjHighlightColor, moveHighlightColor;
	private int colorMode = 0; // 0 is default, 1 is other.
	private bool isSelectable = true;
	private bool hasStarted = false;
	private GameManager gameManager;
	public bool maneuvered = false;

	// Start is called before the first frame update
	void Start() {
		if (hasStarted == true)
			return;
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		hasStarted = true;
		highlightColor = Color.white;
		adjHighlightColor = new Color(1.0f, 0.0f, 0.0f, 0.7f);
		moveHighlightColor = new Color(0.53f, 0.91f, 0.56f); // (134, 231, 143)
		defaultColor = Color.gray;
		coll = gameObject.GetComponent<PolygonCollider2D>();
		if (armyCountCirclePrefab != null) {
			armyCountCircle = (GameObject)Instantiate(armyCountCirclePrefab, new Vector3(transform.position.x, transform.position.y, 9),
			armyCountCirclePrefab.transform.rotation);
			armyCountCircleRenderer = armyCountCircle.GetComponent<SpriteRenderer>();
			armyCountCircle.SetActive(false);
		}
		InitBorderPoints();
	}

	// Update is called once per frame
	void Update() {
		if (gameManager.isGameActive) {
			//Debug.Log(gameObject + " highlight int: " + highlightInt);
			//Debug.Log(gameObject + " color mode: " + colorMode);
			// From the human perspective, only allow owned territories to be selectable
			// when it is their turn.
			if (gameManager.turnPlayer == TurnPlayer.P1) {
				if (GameObject.Equals(ownerID, GameObject.Find("Player 1"))) {
					if (selectedTerritory != null && colorMode == 0)
						isSelectable = false;
					else
						isSelectable = true;
				}
				// Territories we do not control should be selectable under certain
				// circumstances unless we are reinforcing our own at the moment.
				else {
					if (PlayerManager.turnState == TurnState.REINFORCE)
						isSelectable = false;
					else
						isSelectable = colorMode == 1;
				}
			}
			else {
				isSelectable = false;
			}

			if (highlightInt == highlightIntTracker) {
				border.enabled = true;
				armyCountCircleRenderer.color = new Color(armyCountCircleRenderer.color.r,
					armyCountCircleRenderer.color.g, armyCountCircleRenderer.color.b, 0.8f);
			}

			if (highlightInt != highlightIntTracker) {
				if (colorMode == 1) {
					//Debug.Log("changing highlighted color");
					colorMode = 0;
					SetBorderColor(defaultColor);
				}

				if (gameManager.turnPlayer == TurnPlayer.P1) {
					if  (PlayerManager.turnState == TurnState.REINFORCE) {
						border.enabled = false;
						armyCountCircle.SetActive(true);
						armyCountCircleRenderer.color = new Color(armyCountCircleRenderer.color.r,
							armyCountCircleRenderer.color.g, armyCountCircleRenderer.color.b, 0.5f);
					}
					else {
						armyCountCircleRenderer.color = new Color(armyCountCircleRenderer.color.r,
							armyCountCircleRenderer.color.g, armyCountCircleRenderer.color.b, 0.8f);
						if (selectedTerritory != null) {
							border.enabled = false;
							armyCountCircle.SetActive(false);
						}
						else {
							border.enabled = true;
							armyCountCircle.SetActive(true);
						}
					}
				}
				else {
					armyCountCircle.SetActive(true);
					if (ownerID.GetComponent<PlayerManager>().playerIDIndex == gameManager.turnPlayerID) {
						border.enabled = true;
						armyCountCircleRenderer.color = new Color(armyCountCircleRenderer.color.r,
							armyCountCircleRenderer.color.g, armyCountCircleRenderer.color.b, 0.8f);
					}
					else {
						border.enabled = false;
						armyCountCircleRenderer.color = new Color(armyCountCircleRenderer.color.r,
							armyCountCircleRenderer.color.g, armyCountCircleRenderer.color.b, 0.4f);
					}
				}
				
				// If we aren't supposed to be highlighted, make sure we stay one behind
				// the counter in case it is incremented again. The +3 helps us avoid 
				// computing modulo from negative number (in the case of the highlight tracker
				// being equal to 0.
				if (highlightInt != (highlightIntTracker + 3 - 1) % 3) {
					highlightInt = (highlightIntTracker + 3 - 1) % 3;
				}
			}
			else {
				armyCountCircleRenderer.color = new Color(armyCountCircleRenderer.color.r,
					armyCountCircleRenderer.color.g, armyCountCircleRenderer.color.b, 1.0f);
			}
		}
		else {
			border.enabled = false;
			armyCountCircle.SetActive(false);
		}
	}

	// ==================================================
	// Initialization Functions
	public TerritoryObject GetTerritoryInstance() {
		return territory;
	}

	public void SetTerritoryInstance(TerritoryObject territory) {
		this.territory = territory;
	}

	private void InitBorderPoints() {
		border = gameObject.GetComponent<LineRenderer>();
		border.enabled = false;
		border.material = new Material(Shader.Find("Sprites/Default"));
		border.startWidth = 0.03f;
		border.endWidth = 0.03f;
		border.startColor = defaultColor;
		border.endColor = defaultColor;
		border.useWorldSpace = false;
		border.positionCount = coll.points.Length + 1;
		for (int i = 0; i < coll.points.Length; i++) {
			border.SetPosition(i, new Vector2(coll.points[i].x, coll.points[i].y));
		}
		border.SetPosition(coll.points.Length, new Vector2(coll.points[0].x, coll.points[0].y));
	}
	// ==================================================

	// ==================================================
	// Color Functions
	public int GetColorMode() {
		return colorMode;
	}

	public void SetBorderColor(Color color) {
		border.startColor = color;
		border.endColor = color;
		armyCountCircleRenderer.color = new Color(color.r, color.g, color.b, 0.8f);
	}

	public static void IncrementHighlightCounter() {
		// 0, 1, 2
		highlightIntTracker++;
		if (highlightIntTracker > 2) {
			highlightIntTracker = 0;
		}
		//Debug.Log("Highlight Counter: " + highlightIntTracker);
	}

	public void HighlightBorder(Color color) {
		highlightInt = highlightIntTracker;
		colorMode = 1;
		SetBorderColor(color);
	}

	public void HighlightAdjacentEnemyTerritories() {
		territory.adjTerritoryList.ForEach(delegate (TerritoryObject adjTerritory) {
			TerritoryManager temp = adjTerritory.territoryGameObj.GetComponent<TerritoryManager>();
			if (GameObject.Equals(ownerID, temp.GetOwnerID()) == false) {
				temp.HighlightBorder(adjHighlightColor);
			}
		});
	}

	public void HighlightReachableFriendlyTerritories() {
		territory.adjTerritoryList.ForEach(delegate (TerritoryObject adjTerritory) {
			TerritoryManager temp = adjTerritory.territoryGameObj.GetComponent<TerritoryManager>();
			// We own an adjacent territory and it is not already highlighted.
			if (GameObject.Equals(ownerID, temp.GetOwnerID()) == true && temp.GetColorMode() == 0) {
				temp.HighlightBorder(moveHighlightColor);
				temp.HighlightReachableFriendlyTerritories();
			}
		});
	}

	private void OnMouseDown() { // Only the human P1 can use this.
		if (isSelectable && gameManager.isGameActive) {
			Debug.Log("clicked Player " + ownerID.name + "'s " + gameObject.name);
			switch (PlayerManager.turnState) {
				case TurnState.REINFORCE:
					maneuvered = false;
					selectedTerritory = gameObject;
					ownerID.GetComponent<PlayerManager>().reinforcements--;
					SetArmyCount(GetArmyCount() + 1);
					break;
				case TurnState.ATTACK:
					if (GameObject.Equals(selectedTerritory, gameObject)) {
						IncrementHighlightCounter();
						//Debug.Log("Highlight Int: " + highlightInt);
						selectedTerritory = null;
						territoryToAttack = null;
						return;
					}
					else if (GameObject.Equals(territoryToAttack, gameObject)) {
						PlayerManager.CommenceAttack(selectedTerritory.GetComponent<TerritoryManager>(),
							territoryToAttack.GetComponent<TerritoryManager>());
						selectedTerritory = null;
						territoryToAttack = null;
						return;
					}

					if (selectedTerritory == null) {
						// Can't attack if we don't have more than one army on the territory.
						if (GetArmyCount() > 1) {
							selectedTerritory = gameObject;
							IncrementHighlightCounter();
							HighlightBorder(highlightColor);
							HighlightAdjacentEnemyTerritories();
						}
					}
					else { // We already selected a territory to attack from, now we have selected an enemy's.
						territoryToAttack = gameObject;
						IncrementHighlightCounter();
						selectedTerritory.GetComponent<TerritoryManager>().HighlightBorder(gameManager.playerColors[0]);
						HighlightBorder(ownerID.GetComponent<PlayerManager>().GetColor());
					}
					break;
				case TurnState.MANEUVER:
					if (GameObject.Equals(selectedTerritory, gameObject)) {
						IncrementHighlightCounter();
						//Debug.Log("Highlight Int: " + highlightInt);
						selectedTerritory = null;
						if (territoryToMoveTo.GetComponent<TerritoryManager>().maneuvered == true)
							gameManager.continueClicked = true;
						return;
					}
					else if (GameObject.Equals(territoryToMoveTo, gameObject)) {
						if (selectedTerritory.GetComponent<TerritoryManager>().GetArmyCount() > 1) {
							PlayerManager.CommenceManeuver(selectedTerritory.GetComponent<TerritoryManager>(),
								territoryToMoveTo.GetComponent<TerritoryManager>());
						}
						else {
							if (maneuvered == true)
								gameManager.continueClicked = true;
						}

						maneuvered = true; // only one maneuver allowed.
						return;
					}

					if (selectedTerritory == null) {
						// Can't maneuver from here if we don't have more than one army on the territory.
						if (GetArmyCount() > 1) {
							selectedTerritory = gameObject;
							IncrementHighlightCounter();
							HighlightBorder(highlightColor);
							HighlightReachableFriendlyTerritories();
						}
					}
					else { // We already selected a territory to move from, now we selected another of ours to move to.
						territoryToMoveTo = gameObject;
					}
					break;
				default:
					if (GameObject.Equals(selectedTerritory, gameObject)) {
						IncrementHighlightCounter();
						//Debug.Log("Highlight Int: " + highlightInt);
						selectedTerritory = null;
						return;
					}
					selectedTerritory = gameObject;
					IncrementHighlightCounter();
					HighlightBorder(highlightColor);
					HighlightReachableFriendlyTerritories();
					//Debug.Log("Highlight Int: " + highlightInt);
					break;
			}
		}
	}

	public bool SpecialSelectionActive() {
		return selectedTerritory != null; // || (gameManager.turnPlayer == TurnPlayer.P1 && PlayerManager.turnState == TurnState.REINFORCE)
	}
	// ==================================================

	// ==================================================
	// Gameplay Functions
	public GameObject GetOwnerID() {
		return ownerID;
	}

	public void SetOwnerID(GameObject playerID) {
		if (hasStarted == false) {
			Start();
		}
		ownerID = playerID;
		defaultColor = ownerID.GetComponent<PlayerManager>().GetColor();
		//Debug.Log(gameObject + "default color: " + defaultColor);
		if (colorMode == 0) {
			SetBorderColor(defaultColor);
		}
	}

	public int GetArmyCount() {
		return armyCount;
	}

	public void SetArmyCount(int num) {
		armyCount = num;
		armyCountCircle.transform.GetChild(0).GetComponent<TextMeshPro>().text = num.ToString();
	}
}
