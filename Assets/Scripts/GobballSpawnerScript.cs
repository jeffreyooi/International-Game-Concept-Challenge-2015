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
	public float		timeToSpawnRainbow;
	public bool			spawnedRainbow;
	public GameObject 	gobball;
	public Sprite[] 	gobballSprite;
	public SpawnPointScript	spawnPointList;
	public RuntimeAnimatorController[] animController;

	// Use this for initialization
	void Start () {
		timeToSpawnRainbow = Random.Range (1.0f, 5.0f);
		spawnedRainbow = false;
		SpawningGobball ();
	}

	void Update () {
		if (!spawnedRainbow) {
			if (timeToSpawnRainbow > 0.0f) {
				timeToSpawnRainbow -= Time.deltaTime;
			} else {
				SpawnRainbow();
				spawnedRainbow = true;
			}
		}
	}

	void SpawningGobball() {
		for (int i = 0; i < numOfGobball; ++i) {
			// Return a random spawn point inside a circle
			//Vector3 spawnPosition = SpawningCircleRange(transform.position, 3.0f);
			Vector2 tempSpawnpoint = new Vector2 (Random.Range (0, 8), Random.Range (0, 8));
			Vector3 spawnPosition = spawnPointList.GetWaypoint((int)tempSpawnpoint.x, (int)tempSpawnpoint.y);
			// Instantiate a new gobball
			GameObject newGobball = Instantiate (gobball, spawnPosition, Quaternion.identity) as GameObject;
			// Random the type and set the sprite
			int type = Random.Range (0, sizeof(GobballSpawnerScript.GOBBALL_TYPE) - 1);
			newGobball.GetComponent<SpriteRenderer>().sprite = gobballSprite[type];
			newGobball.GetComponent<GobballScript>().animController = animController[type];
			newGobball.GetComponent<GobballScript>().SetGobballType(type);
			// Set the coordinate of the default waypoint to the current spawn point
			newGobball.GetComponent<GobballMovementScript>().waypoint_coord = tempSpawnpoint;
			// Make it a child to a parent which governs all the gobball
			newGobball.transform.SetParent(this.gameObject.transform);
		}
	}

	void SpawnRainbow() {
		Vector2 tempSpawnpoint = new Vector2 (Random.Range (0, 8), Random.Range (0, 8));
		Vector3 spawnPosition = spawnPointList.GetWaypoint((int)tempSpawnpoint.x, (int)tempSpawnpoint.y);
		// Instantiate a new gobball
		GameObject newGobball = Instantiate (gobball, spawnPosition, Quaternion.identity) as GameObject;
		// Random the type and set the sprite
		int type = (int)GobballSpawnerScript.GOBBALL_TYPE.GOBBALL_RAINBOW;
		newGobball.GetComponent<SpriteRenderer>().sprite = gobballSprite[type];
		newGobball.GetComponent<GobballScript>().animController = animController[type];
		newGobball.GetComponent<GobballScript>().SetGobballType(type);
		// Set the coordinate of the default waypoint to the current spawn point
		newGobball.GetComponent<GobballMovementScript>().waypoint_coord = tempSpawnpoint;
		// Make it a child to a parent which governs all the gobball
		newGobball.transform.SetParent(this.gameObject.transform);
		newGobball.GetComponent<GobballScript> ().PlayExplosion ();
	}

	public Sprite ReturnSprite(int type) {
		return gobballSprite [type];
	}

	public RuntimeAnimatorController ReturnAnimController(int type) {
		return animController[type];
	}
}
