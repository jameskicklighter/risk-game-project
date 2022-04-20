using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoryManager : MonoBehaviour {
	private PolygonCollider2D coll;
	public static GameObject selectedTerritory;
	private LineRenderer border;

	// Start is called before the first frame update
	void Start() {
		coll = gameObject.GetComponent<PolygonCollider2D>();
		InitBorderPoints();
	}

	private void InitBorderPoints() {
		border = gameObject.GetComponent<LineRenderer>();
		border.enabled = false;
		border.material = new Material(Shader.Find("Sprites/Default"));
		border.startWidth = 0.025f;
		border.endWidth = 0.025f;
		border.startColor = Color.yellow;
		border.endColor = Color.yellow;
		border.useWorldSpace = false;
		border.positionCount = coll.points.Length + 1;
		for (int i = 0; i < coll.points.Length; i++) {
			border.SetPosition(i, new Vector2(coll.points[i].x, coll.points[i].y));
		}
		border.SetPosition(coll.points.Length, new Vector2(coll.points[0].x, coll.points[0].y));
	}

	// Update is called once per frame
	void Update() {
		if (GameObject.ReferenceEquals(gameObject, selectedTerritory) == false && border.enabled == true) {
			Debug.Log(gameObject.name + " no longer clicked.");
			HighlightTerritory(false);
		}
	}

	private void OnMouseDown() {
		Debug.Log("clicked " + gameObject.name);
		selectedTerritory = gameObject;
		HighlightTerritory(true);
	}

	private void HighlightTerritory(bool enable) {
		border.enabled = enable;
	}
}
