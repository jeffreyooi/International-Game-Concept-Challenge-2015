using UnityEngine;
using System.Collections;

public class GobballScript : MonoBehaviour {

	public enum GOBBALL_DIRECTION {
		DIR_UP,
		DIR_DOWN,
		DIR_LEFT,
		DIR_RIGHT
	}

	public int type;
	bool pickedUp;
	Vector3 lastPosition;

	// Use this for initialization
	void Start () {
		pickedUp = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetGobballDirection(int dir) {

	}

	public void SetGobballType(int type, Sprite sprite) {
		
	}

	public void SetPickedUp(bool pickUp) {
		pickedUp = pickUp;
		lastPosition = transform.position;
	}

	public bool GetPickedUp() {
		return pickedUp;
	}

	public void ReturnToPreviousPos() {
	
	}
}
