using UnityEngine;
using System.Collections;

namespace Farming_Gobball {

	public class GobballScript : MonoBehaviour {

		private int 	type;
		private bool 	pickedUp;
		private bool 	backToPrevPos;
		private float 	speed;
		private float	countdown;
		private Vector2 direction;
		private Vector3 lastPosition;
		private Animator anim;
		private Transform gobballParent;
		private SpriteRenderer spriteRenderer;
		private GobballMovementScript movement;
		private GameObject game;
		public ParticleSystem rainbowExplosion;
		public ParticleSystem rainbowImplosion;
		public RuntimeAnimatorController animController;
		
		private AudioSource audioSource;

		// Use this for initialization
		void Start () {
			pickedUp = false;
			backToPrevPos = false;	
			speed = 5.0f;
			gobballParent = gameObject.transform.parent;
			anim = GetComponent<Animator> ();
			anim.runtimeAnimatorController = animController;
			audioSource = GetComponent<AudioSource> ();
			spriteRenderer = GetComponent<SpriteRenderer> ();
			movement = GetComponent<GobballMovementScript> ();
			game = GameObject.Find ("Gameplay");
			if (type == (int)GobballSpawnerScript.GOBBALL_TYPE.GOBBALL_RAINBOW) {
				countdown = 3.0f;
			}

			//cursorLastPos = Vector3.zero;
			//cursorSpeed = Vector3.zero;


			audioSource = GetComponent<AudioSource> ();
		}
		
		// Update is called once per frame
		void Update () {
			if (backToPrevPos && !Input.GetMouseButton(0)) {
				// Move object back to last saved position
				BackToPreviousPos (lastPosition);
			}
			if (type == (int)GobballSpawnerScript.GOBBALL_TYPE.GOBBALL_RAINBOW) {
				countdown -= Time.deltaTime;
				if (countdown <= 0.0f) {
					PlayImplosion();
					gameObject.SetActive(false);
					game.GetComponent<GameplayScript>().SetTotalGobball (game.GetComponent<GameplayScript>().GetTotalGobball () - 1);
				}
			}
		}

		public void SetAnimController(int type) {
			anim.runtimeAnimatorController = gobballParent.GetComponent<GobballSpawnerScript>().ReturnAnimController(type);
		}

		public void SetGobballType(int newType) {
			type = newType;
		}

		public int GetGobballType() {
			return type;
		}

		public void SetPickedUp(bool pickup) {
			pickedUp = pickup;
			if (pickedUp == true) {
				lastPosition = transform.position;
				audioSource.PlayOneShot(audioSource.clip);
			}
		}

		public bool GetPickedUp() {
			return pickedUp;
		}

		public void SetBackToPrev(bool back) {
			backToPrevPos = back;
		}

		public void PlayExplosion() {
			ParticleSystem poof = Instantiate(rainbowExplosion, transform.position, Quaternion.identity) as ParticleSystem;
			Destroy (poof.gameObject, poof.startLifetime);
		}

		void PlayImplosion() {
			ParticleSystem poof = Instantiate(rainbowImplosion, transform.position, Quaternion.identity) as ParticleSystem;
			Destroy (poof.gameObject, poof.startLifetime);
		}

		void BackToPreviousPos(Vector3 prevPos) {
			// Check if the magnitude of current position and last saved position is more than a certain value
			if (Vector3.Magnitude (transform.position - prevPos) > 0.5f) {
				// Move the object towards the last saved position
				Vector3 newPos = Vector3.Lerp (transform.position, prevPos, Time.deltaTime * speed);
				transform.position = newPos;
			} else {
				// Set to false to stop running this function
				backToPrevPos = false;
			}
		}

	#if UNITY_EDITOR
		void OnMouseDown() {
			spriteRenderer.sortingOrder = 1;
			pickedUp = true;
			lastPosition = transform.position;
			movement.SetGobballAction ((int)GobballMovementScript.GOBBALL_BEHAVIOR.FLOATING);

			//play audio
			audioSource.Play();

		}
		
		void OnMouseDrag() {
			Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 newPosition = new Vector3 (screenToWorld.x, screenToWorld.y, transform.position.y);
			transform.position = newPosition;
		}
		
		void OnMouseUp() {
			spriteRenderer.sortingOrder = 0;
			pickedUp = false;
			movement.SetGobballAction ((int)GobballMovementScript.GOBBALL_BEHAVIOR.DROPPING);
		}
	#endif

	}
}
