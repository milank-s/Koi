using UnityEngine;
using System.Collections;

public class PlayOneSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (soundLength ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator soundLength (){
		yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
		GameObject.Destroy (this.gameObject);
	}
}
