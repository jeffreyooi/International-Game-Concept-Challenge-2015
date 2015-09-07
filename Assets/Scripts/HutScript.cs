using UnityEngine;
using System.Collections;

public class HutScript : MonoBehaviour {

	public enum HUT_TYPE
	{
		HUT_CYAN,
		HUT_ORANGE,
		HUT_PINK
	}

	public int type;
	public GameplayScript gameplayObj;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D co) {
		// Check if object is gobball, and if the gobball is touched / clicked
		if (co.gameObject.CompareTag("Gobball") && !co.gameObject.GetComponent<GobballScript>().GetPickedUp()) {
			// Check if type of gobball matches the colour of the hut or it's a rainbow gobball
			if (co.gameObject.GetComponent<GobballScript>().GetGobballType() == type || 
			    co.gameObject.GetComponent<GobballScript>().GetGobballType() == (int)GobballSpawnerScript.GOBBALL_TYPE.GOBBALL_RAINBOW) {
				co.gameObject.SetActive(false);

				gameplayObj.Count++;
			} else {
				co.gameObject.GetComponent<GobballScript>().SetBackToPrev(true);
			}
		}
	}	
}
