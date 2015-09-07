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

	float offset_x = 0;
	float offset_y = 0;
	public Vector2[,] waypointArrays;

	// Use this for initialization
	void Start () {
		SpawningGrid ();
		SpawningGobball ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void SpawningGobball() {
		for (int i = 0; i < numOfGobball; ++i) {
			// Return a random spawn point inside a circle
			//Vector3 spawnPosition = SpawningCircleRange(transform.position, 3.0f);
			Vector3 spawnPosition = GetGrid(Random.Range(0, 8), Random.Range (0, 8));
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

	void SpawningGrid() {
		waypointArrays = new Vector2[8, 8];
		for (int x = 0; x < 8; x++) {
			offset_x = -x * 0.75f;
			offset_y = -x * 0.4f;
			for (int y = 0; y < 8; y++) {
				Vector2 position = new Vector3(transform.position.x + offset_x, transform.position.y + offset_y);
				offset_x += 0.75f;
				offset_y -= 0.4f;
				waypointArrays[x, y] = position;
				//Debug.Log (waypointArrays[x, y]);
			}
		}
	}

	Vector2 GetGrid(int x, int y) {
		return waypointArrays[x, y];
	}

	public Sprite ReturnSprite(int type) {
		return gobballSprite [type];
	}
}
