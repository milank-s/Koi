using UnityEngine;
using System.Collections;

public class playSound : MonoBehaviour {

	public GameObject SoundObject;
	public AudioClip Sound;
	public float scale, volume;
	float pitch, curTime;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		curTime += Time.deltaTime;
	}

	public void play(){
		GameObject newSound = GameObject.Instantiate (SoundObject);
		newSound.GetComponent<AudioSource>().clip = Sound;
		SoundObject.transform.position = this.transform.position;
		newSound.GetComponent<AudioSource>().pitch = (Random.Range (1, scale) / scale);
		newSound.GetComponent<AudioSource>().PlayOneShot(Sound);
		newSound.GetComponent<AudioSource>().volume = volume;
		pitch ++;
		curTime = 0f;
		if (pitch >= scale) {
			pitch = 1;
		}
	}
}
