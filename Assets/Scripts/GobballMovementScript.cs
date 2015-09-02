using UnityEngine;
using System.Collections;

public class GobballMovementScript : MonoBehaviour {

	enum GOBBALL_BEHAVIOR {
		IDLE,
		MOVING,
		FLOATING,
		DROPPING
	}

	float idleTime;

	// Use this for initialization
	void Start () {
		idleTime = Random.Range (0.0f, 3.0f);
	}
	
	// Update is called once per frame
	void Update () {
		GobballMovement ();
	}

	void GobballMovement() {

	}
}
