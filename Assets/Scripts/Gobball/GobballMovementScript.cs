using UnityEngine;
using System.Collections;

public class GobballMovementScript : MonoBehaviour {

	public enum GOBBALL_BEHAVIOR {
		IDLE,
		MOVING,
		FLOATING,
		DROPPING
	}

	private int 	action;
	private float 	idleTime;
	private float 	movingSpeed;
	private Vector3 nextWaypoint;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		action = 0;
		movingSpeed = 1.0f;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		RandomIdleTime ();
	}
	
	// Update is called once per frame
	void Update () {
		GobballMovement ();
		spriteRenderer.sortingOrder = (int)transform.position.y * -1;
	}

	void GobballMovement() {
		switch (action) {
		case (int)GOBBALL_BEHAVIOR.IDLE:
			idleTime -= Time.deltaTime;
			if (idleTime <= 0.0f) {
				RandomNewAction();
			}
			break;
		case (int)GOBBALL_BEHAVIOR.MOVING:
			if (!CheckIfReachedWaypoint()) 
				GobballWalking ();
			else
				RandomNewAction();
			break;
		case (int)GOBBALL_BEHAVIOR.FLOATING:

			break;
		case (int)GOBBALL_BEHAVIOR.DROPPING:

			break;
		}
	}

	public void SetGobballAction(int newAction) {
		action = newAction;
	}

	void RandomNewAction() {
		// Random new action based on the size of enum, minus of the behavior that is triggered by user interaction from below
		action = Random.Range (0, sizeof(GobballMovementScript.GOBBALL_BEHAVIOR) - 2);
		switch (action) {
		case (int)GOBBALL_BEHAVIOR.IDLE:
			RandomIdleTime ();
			break;
		case (int)GOBBALL_BEHAVIOR.MOVING:
			RandomNextWaypoint ();
			break;
		case (int)GOBBALL_BEHAVIOR.FLOATING:
			
			break;
		case (int)GOBBALL_BEHAVIOR.DROPPING:
			
			break;
		}
	}

	void RandomIdleTime() {
		idleTime = Random.Range (0.0f, 3.0f);
	}

	void RandomNextWaypoint() {
		// Randomize an angle from a circle
		float angle = Random.value * 360;
		// Randomize the range from center of circle
		float rad = Random.Range (0.0f, 3.5f);
		// Calculation of the position based on the angle and range from center of position
		Vector3 pos;
		Vector3 parentPosition = gameObject.transform.parent.position;
		pos.x = parentPosition.x + rad * Mathf.Sin (angle * Mathf.Deg2Rad);
		pos.y = parentPosition.y + rad * Mathf.Cos (angle * Mathf.Deg2Rad);
		pos.z = parentPosition.z;
		// Assign the new randomized vector to the next waypoint
		nextWaypoint = pos;
	}

	void GobballWalking() {
		Vector3 newPosition = (nextWaypoint - transform.position).normalized;
		newPosition *= movingSpeed * Time.deltaTime;
		gameObject.transform.Translate (newPosition, Space.World);
	}

	bool CheckIfReachedWaypoint() {
		if (Vector3.Magnitude (transform.position - nextWaypoint) > 0.5f)
			return false;
		else 
			return true;
	}
}
