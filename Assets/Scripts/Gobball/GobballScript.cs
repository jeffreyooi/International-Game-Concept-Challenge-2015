using UnityEngine;
using System.Collections;

public class GobballScript : MonoBehaviour {

	private int 	type;
	private bool 	pickedUp;
	private bool 	backToPrevPos;
	private float 	distance;
	private float 	speed;
	private Vector3 lastPosition;

	// Use this for initialization
	void Start () {
		pickedUp = false;
		backToPrevPos = false;	
		distance = 0.0f;
		speed = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (backToPrevPos) {
			// Move object back to last saved position
			BackToPreviousPos (lastPosition);
		}
	}

	public void SetGobballType(int newType) {
		type = newType;
	}

	public int GetGobballType() {
		return type;
	}

	public void SetPickedUp(bool pickUp) {
		pickedUp = pickUp;
		if (pickedUp == true) {
			lastPosition = transform.position;
		} else {
			distance = Vector3.Distance(transform.position, lastPosition);
		}
	}

	public bool GetPickedUp() {
		return pickedUp;
	}

	public void SetBackToPrev(bool back) {
		backToPrevPos = back;
	}

	void BackToPreviousPos(Vector3 prevPos) {
		// Check if the magnitude of current position and last saved position is more than a certain value
		if (Vector3.Magnitude (transform.position - prevPos) > 0.5f) {
			// Move the object towards the last saved position
			Vector3 newPos = Vector3.Lerp (transform.position, prevPos, Time.deltaTime * speed);
			transform.position = newPos;
		} else {
			// Set to false to stop running this function
			backToPrevPos = false;
		}
	}
}
