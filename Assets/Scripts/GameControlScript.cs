using UnityEngine;
using System.Collections;

public class GameControlScript : MonoBehaviour {

	public GameObject gobballParent;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
		DetectInputEditor();
#elif UNITY_ANDROID
		DetectInputAndroid();
#endif
	}
	
	void DetectInputAndroid() {
		// Multi touch, multiple fingers to move multiple objects
		foreach (Touch touch in Input.touches) {
			// Convert the touch position from screen coordinates to world coordinates
			Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
			// Get the child from the parent list
			foreach (Transform child in gobballParent.transform) {
				// Check if the touch position overlaps with the child collider
				if (child.GetComponent<Collider2D>() == Physics2D.OverlapPoint (touchPosition)) {
					// Begin touch, update object position to touch position and change rendering order
					if (touch.phase == TouchPhase.Began) {
						child.transform.position = touchPosition;
						child.GetComponent<SpriteRenderer>().sortingOrder = 30;
						child.GetComponent<GobballScript>().SetPickedUp(true);
						child.GetComponent<GobballMovementScript>().SetGobballAction((int)GobballMovementScript.GOBBALL_BEHAVIOR.FLOATING);
					}
					// Drag touch, update object position to touch position
					else if (touch.phase == TouchPhase.Moved) {
						child.transform.position = touchPosition;
					}
					// End touch, revert the rendering order
					else if (touch.phase == TouchPhase.Ended) {
						if (child.GetComponent<GobballScript>().GetPickedUp()) {
							child.GetComponent<SpriteRenderer>().sortingOrder = 0;
							child.GetComponent<GobballScript>().SetPickedUp(false);
							child.GetComponent<GobballMovementScript>().SetGobballAction((int)GobballMovementScript.GOBBALL_BEHAVIOR.IDLE);
						}
					}
				}
			}
		}
	}
	
	void DetectInputEditor() {
		// Check for left click every frame
		//if (Input.GetMouseButton (0)) {
			// Convert cursor position from screen coordinates to world coordinates
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		foreach (Transform child in gobballParent.transform) {
			// If mouse is clicked, bring the object rendering to front, set pick up and store previous position of gobball and set the position to cursor position
			if (Input.GetMouseButtonDown(0)) {
				if (child.GetComponent<Collider2D>() == Physics2D.OverlapPoint (cursorPosition)) {
					child.GetComponent<SpriteRenderer>().sortingOrder = 30;
					child.GetComponent<GobballScript>().SetPickedUp(true);
					child.GetComponent<GobballMovementScript>().SetGobballAction((int)GobballMovementScript.GOBBALL_BEHAVIOR.FLOATING);
					child.transform.position = cursorPosition;
				} 
			} 
			// If mouse is dragged, update the position of the object to the cursor position
			else if (Input.GetMouseButton (0)) {
				if (child.GetComponent<Collider2D>() == Physics2D.OverlapPoint (cursorPosition)) {
					child.transform.position = cursorPosition;
				} 
			} 
			// If mouse is released, revert the rendering order and set pick up to false
			else if (Input.GetMouseButtonUp(0)) {
				if (child.GetComponent<GobballScript>().GetPickedUp()) {
					child.GetComponent<SpriteRenderer>().sortingOrder = 0;
					child.GetComponent<GobballScript>().SetPickedUp(false);
					child.GetComponent<GobballMovementScript>().SetGobballAction((int)GobballMovementScript.GOBBALL_BEHAVIOR.DROPPING);
				}
			}
		}
	}
}
