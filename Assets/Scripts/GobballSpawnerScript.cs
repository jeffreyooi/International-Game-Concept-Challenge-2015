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
	public GameObject	spawnPoint;
	public SpawnPointScript	spawnPointList;
	public RuntimeAnimatorController[] animController;

	// Use this for initialization
	void Start () {
		spawnPointList = spawnPoint.GetComponent<SpawnPointScript> ();
		SpawningGobball ();
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
			int type = Random.Range (0, sizeof(GobballSpawnerScript.GOBBALL_TYPE));
			newGobball.GetComponent<SpriteRenderer>().sprite = gobballSprite[type];
			newGobball.GetComponent<GobballScript>().animController = animController[type];
			newGobball.GetComponent<GobballScript>().SetGobballType(type);
			// Set the coordinate of the default waypoint to the current spawn point
			newGobball.GetComponent<GobballMovementScript>().waypoint_coord = tempSpawnpoint;
			// Make it a child to a parent which governs all the gobball
			newGobball.transform.SetParent(this.gameObject.transform);
		}
	}
//	
//	Vector3 SpawningCircleRange(Vector3 position, float radius) {
//		// Randomize an angle from a circle
//		float angle = Random.value * 360;
//		// Randomize the range from center of circle
//		float rad = Random.Range (0.0f, radius);
//		// Calculation of the position based on the angle and range from center of position
//		Vector3 pos;
//		pos.x = position.x + rad * Mathf.Sin (angle * Mathf.Deg2Rad);
//		pos.y = position.y + rad * Mathf.Cos (angle * Mathf.Deg2Rad);
//		pos.z = position.z;
//		// Return the vector3 calculated
//		return pos;
//	}

	public Sprite ReturnSprite(int type) {
		return gobballSprite [type];
	}

	public RuntimeAnimatorController ReturnAnimController(int type) {
		return animController[type];
	}
}
