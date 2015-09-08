using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Farming_Gobball {

	public class GameplayScript : MonoBehaviour {

		public int Count;
		private bool gameend;
		private bool gamestart;
		public int TotalGobball;
		public GobballSpawnerScript gobballSpawner;
		// Use this for initialization
		void Start () {
			Count = 0;
			gameend = false;
			gamestart = false;
			TotalGobball = gobballSpawner.numOfGobball;
		}

		public void SetGameEnd(bool end) {
			gameend = end;
		}

		public bool GetGameEnd() {
			return gameend;
		}

		public void SetGameStart(bool start) {
			gamestart = start;
		}
		
		public bool GetGameStart() {
			return gamestart;
		}

		public int GetTotalGobball() {
			return TotalGobball;
		}

		public void SetTotalGobball(int newNum) {
			TotalGobball = newNum;
		}
	}
}