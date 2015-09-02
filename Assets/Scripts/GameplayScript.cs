using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameplayScript : MonoBehaviour {

	public enum GOBBALL_TYPE {
		GOBBALL_PINK,
		GOBBALL_CYAN,
		GOBBALL_ORANGE,
		GOBBALL_RAINBOW
	}

	//public Text countdownTimerText;
	//public Text numOfGobballLeftText;
	public Transform gobballParent;
	public GameObject gobball;
	public Sprite[] gobballSprite;
	public int numOfGobball = 10;

	// Use this for initialization
	void Start () {
		SpawningGobball ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawningGobball() {
		for (int i = 0; i < numOfGobball; ++i) {
			Vector3 spawnPosition = SpawningCircleRange(transform.position, 4.0f);
			Vector3 gobballPosition = new Vector3 (Random.Range(-5.0f, 5.0f), Random.Range(-4.0f, 4.0f), 0.0f);
			//GameObject newGobball = Instantiate (gobball, gobballPosition, Quaternion.identity) as GameObject;
			GameObject newGobball = Instantiate (gobball, spawnPosition, Quaternion.identity) as GameObject;
			int type = Random.Range (0, 3);
			newGobball.GetComponent<SpriteRenderer>().sprite = gobballSprite[type];
			newGobball.GetComponent<GobballSpawningScript>().type = type;
			newGobball.transform.SetParent(gobballParent);
		}
	}

	Vector3 SpawningCircleRange(Vector3 position, float radius) {
		float angle = Random.value * 360;
		float rad = Random.Range (0.0f, radius);
		Vector3 pos;
		pos.x = position.x + rad * Mathf.Sin (angle * Mathf.Deg2Rad);
		pos.y = position.y + rad * Mathf.Cos (angle * Mathf.Deg2Rad);
		pos.z = position.z;
		return pos;
	}
}
