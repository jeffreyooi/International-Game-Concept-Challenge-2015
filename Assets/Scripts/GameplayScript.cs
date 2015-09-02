﻿using UnityEngine;
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
			Vector3 gobballPosition = new Vector3 (Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f), 0);
			GameObject newGobball = Instantiate (gobball, gobballPosition, Quaternion.identity) as GameObject;
			newGobball.GetComponent<SpriteRenderer>().sprite = gobballSprite[Random.Range (0, 3)];
			newGobball.transform.SetParent(gobballParent);
		}
	}
}