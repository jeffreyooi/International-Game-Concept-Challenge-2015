using UnityEngine;
using System.Collections;
namespace Farming_Gobball {
	public class HutScript : MonoBehaviour {

		public enum HUT_TYPE
		{
			HUT_CYAN,
			HUT_ORANGE,
			HUT_PINK
		}

		public int type;
		public GameplayScript gameplayObj;

		//audio sources
		private AudioSource audioSource;
		public AudioClip[] audioClip;

		private Animator anim;

		// Use this for initialization
		void Start () 
		{
			audioSource = GetComponent<AudioSource> ();
			anim = GetComponent<Animator> ();
		}

		void OnTriggerEnter2D(Collider2D col)
		{
			// If object collided is gobball
			if (col.gameObject.CompareTag("Gobball")) {
				// play audio and highlight the hut
				audioSource.PlayOneShot(audioClip[0]);
				anim.SetBool("Highlight", true);
			}
		}

		void OnTriggerStay2D(Collider2D co) {
			// Check if object is gobball, and if the gobball is touched / clicked
			if (co.gameObject.CompareTag("Gobball")) {
				//audioSource.PlayOneShot(audioClip[0]);
				if (!co.gameObject.GetComponent<GobballScript>().GetPickedUp()) {
					// Check if type of gobball matches the colour of the hut or it's a rainbow gobball
					if (co.gameObject.GetComponent<GobballScript>().GetGobballType() == type || 
					    co.gameObject.GetComponent<GobballScript>().GetGobballType() == (int)GobballSpawnerScript.GOBBALL_TYPE.GOBBALL_RAINBOW) {
						// If it is, deactivate the gameobject, play a sound and set highlight of the hut to false and play hut bouncing animation
						co.gameObject.SetActive(false);
						audioSource.PlayOneShot(audioClip[Random.Range(2,5)]);
						anim.SetBool("Highlight", false);
						anim.SetTrigger("doBounce");
						gameplayObj.Count++;
					} else {
						// Move gobball back to previous position where it is picked up, vibrate
						co.gameObject.GetComponent<GobballScript>().SetBackToPrev(true);
						audioSource.PlayOneShot(audioClip[1]);
						Handheld.Vibrate();
					}
				}
			}
		}	

		void OnTriggerExit2D(Collider2D co) {
			// If object exit from collided is gobball
			if (co.gameObject.CompareTag("Gobball")) {
				// unhighlight the hut
				anim.SetBool("Highlight", false);
			}
		}
	}
}
