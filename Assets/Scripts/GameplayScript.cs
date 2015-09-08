using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameplayScript : MonoBehaviour {

	public int Count;
	public int TotalGobball;
	public GobballSpawnerScript gobballSpawner;
	// Use this for initialization
	void Start () {
		Count = 0;
		TotalGobball = gobballSpawner.numOfGobball;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int GetTotalGobball() {
		return TotalGobball;
	}

	public void SetTotalGobball(int newNum) {
		TotalGobball = newNum;
	}
}
