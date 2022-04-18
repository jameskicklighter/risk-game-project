using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	// Start is called before the first frame update
	private float speed = 0.5f;
	private float verticalBound = 4.09f;
	private float zoomSize;

	void Start() {

	}

	// Update is called once per frame
	void Update() {
		zoomSize = gameObject.GetComponent<Camera>().orthographicSize;

		if (transform.position.y > verticalBound) {
			transform.position = new Vector2(transform.position.x, verticalBound);
		}

		if (transform.position.y < -verticalBound) {
			transform.position = new Vector2(transform.position.x, -verticalBound);
		}

		if (Input.GetKey(KeyCode.LeftControl) == false) {
			if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
				MoveCameraUp();
				Debug.Log("up");
			}
			else if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
				MoveCameraDown();
				Debug.Log("down");
			}
		}
		else if (zoomSize >= 7) {
			if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
				ZoomIn();
				Debug.Log("zoom in");
			}
		}
		else if (zoomSize <= 3.5) {
			if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
				ZoomOut();
				Debug.Log("zoom out");
			}
		}
		else {
			if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
				ZoomIn();
				Debug.Log("zoom in");
			}
			else if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
				ZoomOut();
				Debug.Log("zoom out");
			}
		}
	}

	void ZoomIn() {
		zoomSize -= 0.5f;
		gameObject.GetComponent<Camera>().orthographicSize = zoomSize;
	}

	void ZoomOut() {
		zoomSize += 0.5f;
		gameObject.GetComponent<Camera>().orthographicSize = zoomSize;
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
