using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameplayScript : MonoBehaviour {

	public int Count;
	private bool gameend;
	// Use this for initialization
	void Start () {
		Count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetGameEnd(bool end) {
		gameend = end;
	}

	public bool GetGameEnd() {
		return gameend;
	}
}
