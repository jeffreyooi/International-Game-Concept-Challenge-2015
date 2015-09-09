using UnityEngine;
using System.Collections;

public class SpawnPointScript : MonoBehaviour {

	float offset_x = 0;
	float offset_y = 0;
	public Vector2[,] waypointArrays;
	GameObject SpawnPoint;
	// Use this for initialization
	void Start () {
		SpawnPoint = GameObject.Find ("Spawn Points");
		GameObject temp = new GameObject ();
		waypointArrays = new Vector2[8, 8];
		for (int x = 0; x < 8; x++) {
			offset_x = -x * 0.75f;
			offset_y = -x * 0.4f;
			for (int y = 0; y < 8; y++) {
				Vector2 position = new Vector3(transform.position.x + offset_x, transform.position.y + offset_y);
				GameObject spawnpoint = Instantiate (temp, position, Quaternion.identity) as GameObject;
				spawnpoint.transform.parent = SpawnPoint.transform;
				spawnpoint.name = "Spawn point " + spawnpoint.transform.position;
				offset_x += 0.75f;
				offset_y -= 0.4f;
				waypointArrays[x, y] = position;
				//Debug.Log (waypointArrays[x, y]);
			}
		}
		Destroy (temp);
	}
	
	public Vector2 GetWaypoint(int x, int y) {
		return waypointArrays[x, y];
	}
}
