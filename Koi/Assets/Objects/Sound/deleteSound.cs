using UnityEngine;
using System.Collections;

public class deleteSound : MonoBehaviour {

	float volume;
	// Use this for initialization
	void Start () {
		volume = 3;
	}
	
	// Update is called once per frame
	void Update () {
		volume -= Time.deltaTime;
		fadeOut ();
	}
	
	void fadeOut(){
		if (volume < 0) {
			Destroy (this.gameObject);
		} else {
			GetComponent<AudioSource>().volume -= Time.deltaTime/10;
		}
	}
}