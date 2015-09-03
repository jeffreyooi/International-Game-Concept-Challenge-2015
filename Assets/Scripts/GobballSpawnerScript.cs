using UnityEngine;
using System.Collections;

public class GobballSpawnerScript : MonoBehaviour {

	public enum GOBBALL_TYPE {
		GOBBALL_CYAN,
		GOBBALL_ORANGE,
		GOBBALL_PINK,
		GOBBALL_RAINBOW
	}

	public int 			numOfGobball;
	public GameObject 	gobball;
	public Sprite[] 	gobballSprite;
	
	// Use this for initialization
	void Start () {
		SpawningGobball ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void SpawningGobball() {
		for (int i = 0; i < numOfGobball; ++i) {
			// Return a random spawn point inside a circle
			Vector3 spawnPosition = SpawningCircleRange(transform.position, 3.0f);
			// Vector3 gobballPosition = new Vector3 (Random.Range(-5.0f, 5.0f), Random.Range(-4.0f, 4.0f), 0.0f);
			// Instantiate a new gobball
			GameObject newGobball = Instantiate (gobball, spawnPosition, Quaternion.identity) as GameObject;
			// Random the type and set the sprite
			int type = Random.Range (0, sizeof(GobballSpawnerScript.GOBBALL_TYPE));
			newGobball.GetComponent<SpriteRenderer>().sprite = gobballSprite[type];
			newGobball.GetComponent<GobballScript>().SetGobballType(type);
			// Make it a child to a parent which governs all the gobball
			newGobball.transform.SetParent(this.gameObject.transform);
		}
	}
	
	Vector3 SpawningCircleRange(Vector3 position, float radius) {
		// Randomize an angle from a circle
		float angle = Random.value * 360;
		// Randomize the range from center of circle
		float rad = Random.Range (0.0f, radius);
		// Calculation of the position based on the angle and range from center of position
		Vector3 pos;
		pos.x = position.x + rad * Mathf.Sin (angle * Mathf.Deg2Rad);
		pos.y = position.y + rad * Mathf.Cos (angle * Mathf.Deg2Rad);
		pos.z = position.z;
		// Return the vector3 calculated
		return pos;
	}

	public Sprite ReturnSprite(int type) {
		return gobballSprite [type];
	}
}
