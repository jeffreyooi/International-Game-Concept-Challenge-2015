using UnityEngine;
using System.Collections;

public class GameControlScript : MonoBehaviour {

	public GameObject gobballParent;

	Transform meTransform;

	// Use this for initialization
	void Start () {
		meTransform = this.transform;
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
						child.GetComponent<Renderer>().sortingOrder = 1;
						child.GetComponent<GobballScript>().SetPickedUp(true);
					}
					// Drag touch, update object position to touch position
					else if (touch.phase == TouchPhase.Moved) {
						child.transform.position = touchPosition;
					}
					// End touch, revert the rendering order
					else if (touch.phase == TouchPhase.Ended) {
						child.GetComponent<Renderer>().sortingOrder = 0;
						child.GetComponent<GobballScript>().SetPickedUp(false);
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
			if (Input.GetMouseButton(0)) {
				if (child.GetComponent<Collider2D>() == Physics2D.OverlapPoint (cursorPosition)) {
					child.GetComponent<Renderer>().sortingOrder = 1;
					child.GetComponent<GobballScript>().SetPickedUp(true);
					child.transform.position = cursorPosition;
				} 
			} else if (Input.GetMouseButtonUp(0)) {
				child.GetComponent<Renderer>().sortingOrder = 0;
				child.GetComponent<GobballScript>().SetPickedUp(false);
			}
		}
	}
}
