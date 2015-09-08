using UnityEngine;
using System.Collections;

namespace Farming_Gobball {
public class GameControlScript : MonoBehaviour {

		private GameplayScript gameplay;
		public GameObject gobballParent;
		private Touch touch;
		private Transform target;

		// Use this for initialization
		void Start () {
			target = null;
			gameplay = GetComponent<GameplayScript> ();
		}
		
		// Update is called once per frame
		void Update () {
	#if UNITY_ANDROID
			DetectInputAndroid();
	#endif
		}
		
		void DetectInputAndroid() {
			if (gameplay.GetGameEnd () == false && gameplay.GetGameStart()) {
				// If there's one touch
				if (Input.touchCount > 0) {
					// Only get the first touch
					touch = Input.GetTouch (0);
					// Convert the touch position from screen coordinates to world coordinates
					Vector3 touchPosition = Camera.main.ScreenToWorldPoint (touch.position);
					// If begin touch 
					if (touch.phase == TouchPhase.Began) {
						// Get the child from the parent list
						foreach (Transform child in gobballParent.transform) {
							// Check if the touch position overlaps with the child collider
							if (child.GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPosition)) {
								// Assign child to target and control target from now on
								target = child;
								target.transform.position = touchPosition;
								target.GetComponent<SpriteRenderer> ().sortingOrder = 1;
								target.GetComponent<GobballScript> ().SetPickedUp (true);
								target.GetComponent<GobballMovementScript> ().SetGobballAction ((int)GobballMovementScript.GOBBALL_BEHAVIOR.FLOATING);
							}
						}
					}
					// Drag touch, update object position to touch position
					else if (touch.phase == TouchPhase.Moved) {
						target.position = touchPosition;
					}
					// End touch, revert the rendering order
					else if (touch.phase == TouchPhase.Ended) {
						target.GetComponent<SpriteRenderer> ().sortingOrder = 0;
						target.GetComponent<GobballScript> ().SetPickedUp (false);
						target.GetComponent<GobballMovementScript> ().SetGobballAction ((int)GobballMovementScript.GOBBALL_BEHAVIOR.DROPPING);
						target = null;
					}
					
				}
			} else {
				if (Input.touchCount > 0) {
					// Only get the first touch
					touch = Input.GetTouch (0);
					if (touch.phase == TouchPhase.Ended) {
						Application.LoadLevel("Splasher");
					}
				}
			}
		}
	}
}