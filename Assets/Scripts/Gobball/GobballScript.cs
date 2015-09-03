using UnityEngine;
using System.Collections;

public class GobballScript : MonoBehaviour {

	private int 	type;
	private bool 	pickedUp;
	private bool 	backToPrevPos;
	private float 	distance;
	private float 	speed;
	private Vector3 lastPosition;
	private float	countdown;
	private Transform gobballParent;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		pickedUp = false;
		backToPrevPos = false;	
		distance = 0.0f;
		speed = 5.0f;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		gobballParent = gameObject.transform.parent;
		if (type == (int)GobballSpawnerScript.GOBBALL_TYPE.GOBBALL_RAINBOW) {
			countdown = 3.0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (backToPrevPos) {
			// Move object back to last saved position
			BackToPreviousPos (lastPosition);
		}
		if (type == (int)GobballSpawnerScript.GOBBALL_TYPE.GOBBALL_RAINBOW) {
			countdown -= Time.deltaTime;
			if (countdown <= 0.0f) {
				type = Random.Range (0, sizeof(GobballSpawnerScript.GOBBALL_TYPE) - 1);
				spriteRenderer.sprite = gobballParent.GetComponent<GobballSpawnerScript>().ReturnSprite(type);
			}
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
