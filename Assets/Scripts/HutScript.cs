using UnityEngine;
using System.Collections;

public class HutScript : MonoBehaviour {

	public int type;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D co) {
		// Check if object is gobball, type of gobball matches the colour of the hut, and if the gobball is touched / clicked
		if (co.gameObject.CompareTag("Gobball") && 
		    co.gameObject.GetComponent<GobballScript>().type == type && 
		    !co.gameObject.GetComponent<GobballScript>().GetPickedUp()) {
			// If all condition matches, set gobball to not active
			co.gameObject.SetActive(false);
		}
	}	
}
