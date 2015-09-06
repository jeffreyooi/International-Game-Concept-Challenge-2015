using UnityEngine;
using System.Collections;

public class SpawnPointScript : MonoBehaviour {

	float offset_x = 0;
	float offset_y = 0;
	public Vector2[,] waypointArrays;
	// Use this for initialization
	void Start () {
		waypointArrays = new Vector2[9, 9];
		for (int x = 0; x < 9; x++) {
			offset_x = -x * 1.0f;
			offset_y = -x * 0.5f;
			for (int y = 0; y < 9; y++) {
				Vector2 position = new Vector3(transform.position.x + offset_x, transform.position.y + offset_y);
				offset_x += 1.0f;
				offset_y -= 0.5f;
				waypointArrays[x, y] = position;
				//Debug.Log (waypointArrays[x, y]);
			}
		}
	}
	
	public Vector2 GetWaypoint(int x, int y) {
		return waypointArrays[x, y];
	}
}
