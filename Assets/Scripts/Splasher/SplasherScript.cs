﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Farming_Gobball {
	public class SplasherScript : MonoBehaviour {
		
		private Touch touch;

		// Update is called once per frame
		void Update () {
#if UNITY_ANDROID
			if (Input.touchCount > 0) {
				touch = Input.GetTouch(0);
				if (touch.phase == TouchPhase.Ended) {
					Application.LoadLevel("GameScene");
				}
			}

#elif UNITY_EDITOR
			if (Input.GetMouseButtonDown(0)) {
				Application.LoadLevel("GameScene");
			}
#endif
		}
	}
}