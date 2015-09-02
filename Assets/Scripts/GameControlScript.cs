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
		foreach (Touch touch in Input.touches) {
			Vector3 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
			Vector2 touchPos = new Vector2(worldPoint.x, worldPoint.y);
			foreach (Transform child in gobballParent.transform) {
				if (child.GetComponent<Collider2D>() == Physics2D.OverlapPoint (touchPos)) {
					if (touch.phase == TouchPhase.Began) {
						Debug.Log ("Touch began");
					}
					else if (touch.phase == TouchPhase.Ended) {
						Debug.Log ("Touch ended");
					}
				}
			}
		}
	}
	
	void DetectInputEditor() {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePosition = new Vector2 (worldPoint.x, worldPoint.y);
			foreach (Transform child in gobballParent.transform) {
				if (child.GetComponent<Collider2D>() == Physics2D.OverlapPoint (mousePosition)) {
					child.transform.position = mousePosition;
					Debug.Log ("Selected");
				}
			}
		}
	}
}
