using UnityEngine;
using System.Collections;

public class SpawnPointScript : MonoBehaviour {

	float offset_x = 0;
	float offset_y = 0;
	public Vector2[,] waypointArrays;

	// Use this for initialization
	void Start () {
		// Intialize double array
		waypointArrays = new Vector2[8, 8];
		// Assign the values into the double array container
		for (int x = 0; x < 8; x++) {
			offset_x = -x * 0.75f;
			offset_y = -x * 0.4f;
			for (int y = 0; y < 8; y++) {
				Vector2 position = new Vector3(transform.position.x + offset_x, transform.position.y + offset_y);
				offset_x += 0.75f;
				offset_y -= 0.4f;
				waypointArrays[x, y] = position;
			}
		}
	}
	
	public Vector2 GetWaypoint(int x, int y) {
		return waypointArrays[x, y];
	}
}
