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

	void OnTriggerEnter2D(Collider2D co) {
		if (co.gameObject.CompareTag("Gobball") && co.gameObject.GetComponent<GobballSpawningScript>().type == type) {
			co.gameObject.SetActive(false);
		}
	}	
}
