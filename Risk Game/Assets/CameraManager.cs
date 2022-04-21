using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
	// Start is called before the first frame update
	private readonly float speed = 0.5f;
	private float verticalBound;
	private float zoomSize;

	void Start() {

	}

	// Update is called once per frame
	void Update() {
		zoomSize = gameObject.GetComponent<Camera>().orthographicSize;
		verticalBound = DetermineVerticalBound();

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
			Debug.Log("Vertical bound: " + verticalBound);
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

	private void ZoomIn() {
		zoomSize -= 0.5f;
		gameObject.GetComponent<Camera>().orthographicSize = zoomSize;
	}

	private void ZoomOut() {
		zoomSize += 0.5f;
		gameObject.GetComponent<Camera>().orthographicSize = zoomSize;
	}

	private void MoveCameraUp() {
		Debug.Log("verticalBound " + verticalBound);
		Debug.Log("transform y " + transform.position.y);
		if (transform.position.y < verticalBound) {
			transform.Translate(Vector2.up * speed);
		}
	}

	private void MoveCameraDown() {
		if (transform.position.y > -verticalBound) {
			transform.Translate(Vector2.down * speed);
		}
	}

	private float DetermineVerticalBound() {
		return 8f - zoomSize;
	}
}
