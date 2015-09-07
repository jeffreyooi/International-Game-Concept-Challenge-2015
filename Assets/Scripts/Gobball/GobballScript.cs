using UnityEngine;
using System.Collections;

public class GobballScript : MonoBehaviour {

	private int 	type;
	private bool 	pickedUp;
	private bool 	backToPrevPos;
	//private float 	distance;
	private float 	speed;
	private Vector3 lastPosition;
	private float	countdown;
	private Transform gobballParent;
	private SpriteRenderer spriteRenderer;
	private Vector3	cursorLastPos;
	private Vector3 cursorSpeed;
	private float swipeDistance;
	private Vector2 direction;
	private float swipeSpeed;

	// Use this for initialization
	void Start () {
		pickedUp = false;
		backToPrevPos = false;	
		//distance = 0.0f;
		speed = 5.0f;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		gobballParent = gameObject.transform.parent;
		if (type == (int)GobballSpawnerScript.GOBBALL_TYPE.GOBBALL_RAINBOW) {
			countdown = 3.0f;
		}
		cursorLastPos = Vector3.zero;
		cursorSpeed = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (pickedUp) {
			CalculateMouseSpeed ();
		}
		if (backToPrevPos && !Input.GetMouseButton(0)) {
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

	void OnMouseDown() {
		spriteRenderer.sortingOrder = 1;
		pickedUp = true;
		lastPosition = transform.position;
	}
	
	void OnMouseDrag() {
		Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 newPosition = new Vector3 (screenToWorld.x, screenToWorld.y, transform.position.y);
		transform.position = newPosition;
	}
	
	void OnMouseUp() {
		spriteRenderer.sortingOrder = 0;
		pickedUp = false;
	}

	void CalculateMouseSpeed() {
		cursorSpeed = cursorLastPos - Input.mousePosition;
		cursorLastPos = Input.mousePosition;
	}
}
