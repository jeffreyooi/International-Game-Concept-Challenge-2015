using UnityEngine;
using System.Collections;

public class FenceColliderScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D co) {
		// Check if it is gobball by tag
		if (co.gameObject.CompareTag("Gobball")) {
			// Check if gobball is picked up
			if (co.gameObject.GetComponent<GobballScript>().GetPickedUp()) {
				// Set the boolean to move the gobball back to the previous position where it was picked up
				co.gameObject.GetComponent<GobballScript>().SetBackToPrev(true);
			} else {
				// Change the gobball state to idle if collided with the fence
				co.gameObject.GetComponent<GobballMovementScript>().SetGobballAction((int)GobballMovementScript.GOBBALL_BEHAVIOR.IDLE);
			}
		}
	}
}
