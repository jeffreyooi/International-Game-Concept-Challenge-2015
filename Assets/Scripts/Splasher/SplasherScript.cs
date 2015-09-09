using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Farming_Gobball {
	public class SplasherScript : MonoBehaviour {

//		public Image logo;
//		private float displayTime;
//		private float fadeTime;
//		private bool splashScreen;
//		private Color fadedIn;
//		private Color fadedOut;
		private Touch touch;

		// Use this for initialization
		void Start () {
//			displayTime = 1.0f;
//			fadeTime = displayTime / 2;
//			splashScreen = true;
//			fadedIn = new Color (1.0f, 1.0f, 1.0f, 1.0f);
//			fadedOut = new Color (1.0f, 1.0f, 1.0f, 0.0f);
//			logo.color = fadedOut;
		}
		
		// Update is called once per frame
		void Update () {
//			if (splashScreen) {
//				displayTime -= Time.deltaTime;
//				if (displayTime >= fadeTime)
//					FadeIn ();
//				else
//					FadeOut ();
//			}
//			if (displayTime <= 0.0f) {
//				splashScreen = false;
//			}
			if (Input.touchCount > 0) {
				touch = Input.GetTouch(0);
				if (touch.phase == TouchPhase.Ended) {
					Application.LoadLevel("GameScene");
				}
			}
		}

//		void FadeOut() {
//			for (float time = 0.0f; time < fadeTime; time += Time.deltaTime) {
//				logo.color = Color.Lerp (fadedIn, fadedOut, time/fadeTime);
//			}
//		}
//
//		void FadeIn() {
//			for (float time = 0.0f; time < fadeTime; time += Time.deltaTime) {
//				logo.color = Color.Lerp (fadedOut, fadedIn, time/fadeTime);
//			}
//		}
	}
}