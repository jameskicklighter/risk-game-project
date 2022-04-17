using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	// Start is called before the first frame update
	private float speed = 0.5f;
	private float verticalBound = 4.09f;

	void Start() {

	}

	// Update is called once per frame
	void Update() {
		if (transform.position.y > verticalBound) {
			transform.position = new Vector2(transform.position.x, verticalBound);
		}
		if (transform.position.y < -verticalBound) {
			transform.position = new Vector2(transform.position.x, -verticalBound);
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
			MoveCameraUp();
			Debug.Log("up");
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
			MoveCameraDown();
			Debug.Log("down");
		}
		//gameObject.GetComponent<Camera>().orthographicSize = 7;
	}

	void MoveCameraUp() {
		if (transform.position.y < verticalBound) {
			transform.Translate(Vector2.up * speed);
		}
	}

	void MoveCameraDown() {
		if (transform.position.y > -verticalBound) {
			transform.Translate(Vector2.down * speed);
		}
	}
}
