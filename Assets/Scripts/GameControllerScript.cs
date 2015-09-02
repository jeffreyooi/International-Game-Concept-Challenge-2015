using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameControllerScript : MonoBehaviour {

	public enum GOBBALL_TYPE {
		GOBBALL_PINK,
		GOBBALL_CYAN,
		GOBBALL_ORANGE,
		GOBBALL_RAINBOW
	}

	//public Text countdownTimerText;
	//public Text numOfGobballLeftText;
	public GameObject gobball;
	public Sprite gobball_Pink;
	public Sprite gobball_Cyan;
	public Sprite gobball_Orange;
	public Sprite gobball_Rainbow;
	int numOfGobball = 10;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < numOfGobball; ++i) {
			Vector3 gobballPosition = new Vector3 (Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f), 0);
			GameObject newGobball = Instantiate (gobball, gobballPosition, Quaternion.identity) as GameObject;
			newGobball.GetComponent<GobballSpawningScript>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
