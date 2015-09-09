using UnityEngine;
using System.Collections;

namespace Farming_Gobball {
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
		public int	direction;
		public GameObject waypoint;
		public Vector2 waypoint_coord;
		public SpawnPointScript waypointsList;
		public Animator anim;

		// Use this for initialization
		void Start () {
			action = 0;
			movingSpeed = 1.0f;
			direction = 1;
			nextWaypoint = transform.position;
			waypoint = GameObject.Find ("Spawn Points"); 
			waypointsList = waypoint.GetComponent<SpawnPointScript> ();
			anim = GetComponent<Animator> ();
			if (waypoint == null) {
				Debug.Log ("Spawn points not found");
			}
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
	//		case (int)GOBBALL_BEHAVIOR.FLOATING:
	//			anim.SetBool("pickup", true);
	//			break;
	//		case (int)GOBBALL_BEHAVIOR.DROPPING:
	//
	//			break;
			}
		}

		public void SetGobballAction(int newAction) {
			action = newAction;
			if (newAction == 2) {
				anim.SetBool ("pickup", true);
			} else if (newAction == 3) {
				anim.SetBool ("pickup", false);
				anim.SetInteger("action", 0);
			}
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

			}
			anim.SetInteger ("action", action);
		}

		void RandomIdleTime() {
			idleTime = Random.Range (1.0f, 5.0f);
		}

		void RandomNextWaypoint() {
			int horizontal = Random.Range (0, 2);
			if (horizontal == 0) {
				waypoint_coord = new Vector2 (waypoint_coord.x, Random.Range (0, 8));
			} else {
				waypoint_coord = new Vector2 (Random.Range (0, 8), waypoint_coord.y);
			}
			Vector2 newWaypoint = waypointsList.GetWaypoint ((int)waypoint_coord.x, (int)waypoint_coord.y);
			nextWaypoint = new Vector3 (newWaypoint.x, newWaypoint.y, newWaypoint.y);
		}

		void GobballWalking() {
			Vector3 newPosition = (nextWaypoint - transform.position).normalized;
			newPosition *= movingSpeed * Time.deltaTime;
			gameObject.transform.Translate (newPosition, Space.World);
		}

		bool CheckIfReachedWaypoint() {
			// Check if reached waypoint by determine distance between 2 vectors
			if (Vector2.Distance(transform.position, nextWaypoint) > 0.5f)
				return false;
			else 
				return true;
		}

		void ChangeDirection() {
			if (nextWaypoint.x < transform.position.x) {
				//direction = 1;
				transform.localScale = new Vector3 (0.3f, transform.localScale.y, transform.localScale.z);
			} else {
				//direction = -1;
				transform.localScale = new Vector3 (-0.3f, transform.localScale.y, transform.localScale.z);
			}
			if (nextWaypoint.y < transform.position.y) {
				anim.SetBool ("back", false);
			} else {
				anim.SetBool ("back", true);
			}
			//transform.localScale = new Vector3 (direction * transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
	}
}