using UnityEngine;
using System.Collections;

public class alphaCutoff : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<SkinnedMeshRenderer>().material.SetFloat ("_Cutoff", Mathf.Abs(Mathf.Sin(Time.time)/1.25f));
	}
}
