using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoryManager : MonoBehaviour {
	private PolygonCollider2D coll;
	private LineRenderer border;
	private TerritoryObject territory;
	public static GameObject selectedTerritory;
	public static int highlightIntTracker = 0;
	private int highlightInt = 2;

	// Start is called before the first frame update
	void Start() {
		coll = gameObject.GetComponent<PolygonCollider2D>();
		InitBorderPoints();
	}

	// Update is called once per frame
	void Update() {
		if (highlightInt != highlightIntTracker) {
			if (border.enabled == true) {
				border.enabled = false;
			}
			// If we aren't supposed to be highlighted, make sure we stay one behind
			// the counter in case it is incremented again. The +3 helps us avoid 
			// computing modulo from negative number (in the case of the highlight tracker
			// being equal to 0.
			if (highlightInt != (highlightIntTracker + 3 - 1) % 3) {
				highlightInt = (highlightIntTracker + 3 - 1) % 3;
			}
		}
		else if (border.enabled == false) {
			border.enabled = true;
		}
	}

	private void InitBorderPoints() {
		border = gameObject.GetComponent<LineRenderer>();
		border.enabled = false;
		border.material = new Material(Shader.Find("Sprites/Default"));
		border.startWidth = 0.025f;
		border.endWidth = 0.025f;
		border.startColor = Color.gray;
		border.endColor = Color.gray;
		border.useWorldSpace = false;
		border.positionCount = coll.points.Length + 1;
		for (int i = 0; i < coll.points.Length; i++) {
			border.SetPosition(i, new Vector2(coll.points[i].x, coll.points[i].y));
		}
		border.SetPosition(coll.points.Length, new Vector2(coll.points[0].x, coll.points[0].y));
	}

	public void SetTerritoryInstance(TerritoryObject territory) {
		this.territory = territory;
	}

	private void IncrementHighlightCounter() {
		// 0, 1, 2
		highlightIntTracker++;
		if (highlightIntTracker > 2) {
			highlightIntTracker = 0;
		}
	}

	private void OnMouseDown() {
		Debug.Log("clicked " + gameObject.name);
		selectedTerritory = gameObject;
		IncrementHighlightCounter();
		HighlightBorder(Color.yellow);
		HighlightAdjacentTerritories();
	}

	public void HighlightBorder(Color color) {
		highlightInt = highlightIntTracker;
		border.startColor = color;
		border.endColor = color;
	}

	public void HighlightAdjacentTerritories() {
		territory.adjTerritoryList.ForEach(delegate (TerritoryObject adjTerritory) {
			adjTerritory.territoryGameObj.GetComponent<TerritoryManager>().HighlightBorder(Color.red);
		});
	}

}
