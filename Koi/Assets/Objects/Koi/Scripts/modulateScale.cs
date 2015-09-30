using UnityEngine;
using System.Collections;

public class modulateScale : MonoBehaviour {

	public float amplitude, translate;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		scale ();
	}

	void scale(){
		float value = (Mathf.Sin (Time.time)/amplitude) + translate;
		this.transform.localScale = new Vector3(1, value, value);
	}
}
