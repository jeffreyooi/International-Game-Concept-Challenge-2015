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
	public GameObject waypoint;

	// Use this for initialization
	void Start () {
		action = 0;
		movingSpeed = 1.0f;
		nextWaypoint = transform.position;
		waypoint = GameObject.Find ("Spawn Points");
		RandomIdleTime ();
	}
	
	// Update is called once per frame
	void Update () {
		// Only run update if Gobball is not picked up
		if (!GetComponent<GobballScript>().GetPickedUp()) {
			GobballMovement ();
			this.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y);
		}
	}

	void GobballMovement() {
		// Finite state machine for the gobball
		switch (action) {
		case (int)GOBBALL_BEHAVIOR.IDLE:
			idleTime -= Time.deltaTime;
			// If idle time reaches 0, random a new action
			if (idleTime <= 0.0f) {
				RandomNewAction();
			}
			break;
		case (int)GOBBALL_BEHAVIOR.MOVING:
			// Check if the gobball reached the waypoint
			if (!CheckIfReachedWaypoint()) {
				// If not, keep moving
				GobballWalking ();
			} else {
				// If reached, random a new action
				RandomNewAction();
			}
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
			// Random a new idle time for the gobball
			RandomIdleTime ();
			break;
		case (int)GOBBALL_BEHAVIOR.MOVING:
			// Random a next waypoint and change the direction of the gobball depending on the next waypoint it is going to
			RandomNextWaypoint ();
			ChangeDirection();
			break;
		case (int)GOBBALL_BEHAVIOR.FLOATING:
			
			break;
		case (int)GOBBALL_BEHAVIOR.DROPPING:
			
			break;
		}
	}

	void RandomIdleTime() {
		idleTime = Random.Range (1.0f, 5.0f);
	}

	void RandomNextWaypoint() {
		// Randomize an angle from a circle
		float angle = (Random.Range (0, 4) * 90) + 45;
		// Randomize the range from center of circle
		float rad = Random.Range (0.0f, 3.5f);
		// Calculation of the position based on the angle and range from center of position
		Vector3 pos;
		//Vector3 parentPosition = gameObject.transform.parent.position;
//		pos.x = parentPosition.x + rad * Mathf.Sin (angle * Mathf.Deg2Rad);
//		pos.y = parentPosition.y + rad * Mathf.Cos (angle * Mathf.Deg2Rad);
//		pos.z = parentPosition.z;
		pos.x = this.gameObject.transform.position.x + rad * Mathf.Sin (angle * Mathf.Deg2Rad);
		pos.y = this.gameObject.transform.position.y + rad * Mathf.Cos (angle * Mathf.Deg2Rad);
		pos.z = this.gameObject.transform.position.z;
		// Assign the new randomized vector to the next waypoint
		nextWaypoint = pos;

//		int offset_x = 0;
//		int offset_y = 0;
//		for (int x = 0; x < 9; x++) {
//			offset_x = -x * 1.0f;
//			offset_y = -x * 0.5f;
//			for (int y = 0; y < 9; y++) {
//				Vector3 position = new Vector3(transform.position.x + offset_x, transform.position.y + offset_y, transform.position.z);
//				GameObject newSpawnPoint = Instantiate (temp, position, Quaternion.identity) as GameObject;
//				newSpawnPoint.name = "Spawnpoint " + offset_x + " " + offset_y;
//				newSpawnPoint.transform.parent = gameObject.transform;
//				offset_x += 1.0f;
//				offset_y -= 0.5f;
//			}
//		}
	}

	void GobballWalking() {
		Vector3 newPosition = (nextWaypoint - transform.position).normalized;
		newPosition *= movingSpeed * Time.deltaTime;
		gameObject.transform.Translate (newPosition, Space.World);
	}

	bool CheckIfReachedWaypoint() {
		if (Vector2.Distance(transform.position, nextWaypoint) > 1.0f)
			return false;
		else 
			return true;
	}

	void ChangeDirection() {
		int dir_x = 1;
		int dir_y = 1;
		if (nextWaypoint.x < transform.position.x) {
			dir_x = 1;
		} else {
			dir_x = -1;
		}

//		if (nextWaypoint.y < transform.position.y) {
//			dir_y = 1;
//		} else {
//			dir_y = -1;
//		}
		transform.localScale = new Vector3 (dir_x * transform.localScale.x, dir_y * transform.localScale.y, transform.localScale.z);
	}
}
