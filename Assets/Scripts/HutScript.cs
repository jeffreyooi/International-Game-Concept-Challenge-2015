using UnityEngine;
using System.Collections;

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

	// Use this for initialization
	void Start () 
	{
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag("Gobball")) {

			audioSource.PlayOneShot(audioClip[0]);

//			// Check if type of gobball matches the colour of the hut or it's a rainbow gobball
//			if (col.gameObject.GetComponent<GobballScript>().GetGobballType() == type || 
//			    col.gameObject.GetComponent<GobballScript>().GetGobballType() == (int)GobballSpawnerScript.GOBBALL_TYPE.GOBBALL_RAINBOW) {
//
//				audioSource.PlayOneShot(audioClip[Random.Range(2,5)]);
//
//			} else {
//				
//				audioSource.PlayOneShot(audioClip[1]);
//				//audioSource[1].PlayOneShot(audioSource[1].clip);
//			}
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
					co.gameObject.SetActive(false);
					audioSource.PlayOneShot(audioClip[Random.Range(2,5)]);
					gameplayObj.Count++;
				} else {

					co.gameObject.GetComponent<GobballScript>().SetBackToPrev(true);
					audioSource.PlayOneShot(audioClip[1]);
					//audioSource[1].Play();
				}
			}
		}
	}	
}
