using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scaleController: MonoBehaviour {

	public Material[] materials;
	public AudioClip sound;
	public GameObject scale;
	public bool onAutomatically;
	public int fallOffAmount;
	public float detachSpeed;
	GameObject[] scales;
	Transform[] scalePositions;
	Vector3[] positions;
	int scaleAmount;
	float interval;
	public GameObject masterTransform;

	// Use this for initialization
	void Start () {
		interval = detachSpeed;
		createArray ();	

	}
	
	// Update is called once per frame
	void Update () {
	}

	void drawLine(){
		for (int i = 1; i < scales.Length; i++) {
			if (Vector3.Distance (scales [i].transform.position, scalePositions [i].position) < 10) {
				scales [i].GetComponent<LineRenderer> ().SetPosition (0, scales [i].transform.position);
				scales [i].GetComponent<LineRenderer> ().SetPosition (1, scalePositions [i].position);
			} else {
				scales[i].GetComponent<LineRenderer> ().enabled = false;
			}
		}
	}

	public void reattach(){
		if (scaleAmount >= scalePositions.Length -1) {
			return;
		}
		GameObject curScale = scales [scaleAmount];

		Mesh snapshot = new Mesh ();
		scalePositions [scaleAmount].gameObject.GetComponent<SkinnedMeshRenderer> ().BakeMesh (snapshot);
		curScale.GetComponent<MeshFilter> ().mesh = snapshot;
		curScale.transform.localScale = scalePositions [scaleAmount].localScale;

		curScale.GetComponent<Rigidbody> ().useGravity = false;
		curScale.GetComponent<Rigidbody> ().isKinematic = true;
		curScale.GetComponent<Rigidbody> ().freezeRotation = false;
		curScale.transform.parent = scalePositions [scaleAmount].gameObject.GetComponent<SkinnedMeshRenderer> ().rootBone;
		curScale.transform.position = scalePositions [scaleAmount].position;
		curScale.transform.rotation = scalePositions [scaleAmount].rotation;
		curScale.GetComponent<LineRenderer> ().enabled = false;

		scaleAmount++;
	}


	public void fallOff(){
		//int value = Random.Range (0, scales.Length);
		if (scaleAmount <= 0) {
			return;
		}
		if (interval < 0) {
			GameObject curScale = scales [scaleAmount];
			curScale.transform.parent = null;
			curScale.GetComponent<Rigidbody> ().isKinematic = false;
			curScale.GetComponent<Rigidbody> ().freezeRotation = false;
			//curScale.GetComponent<Rigidbody> ().useGravity = true;
			//curScale.GetComponentInChildren<TextFader>().setText(scaleAmount.ToString() + ".");
			curScale.GetComponentInChildren<TextFader> ().setText (GetComponent<SceneText> ().getText ());
			addRepellingForce (curScale, scaleAmount);
			scaleAmount--;
			interval = detachSpeed;
		} else {
			interval -= Time.deltaTime;
		}
	}

	void addCenteringForce(GameObject scale, int value){

			Vector3 direction = (scalePositions[value].position - scales[value].transform.position);
			scales[value].GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized, transform.position);

		}

	void addRepellingForce(GameObject scale, int value)	{
		
		Vector3 direction = (scales[value].transform.position - scalePositions[value].position);
		scales[value].GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized*100, transform.position);

	}
	

	void createArray(){
	
		scalePositions = this.GetComponentsInChildren<Transform>();
		scales = new GameObject[scalePositions.Length];
		scaleAmount = scalePositions.Length-1;
		for(int i = 1; i < scalePositions.Length; i++){

			scales[i] = GameObject.Instantiate(scale);
			scales[i].transform.position = scalePositions[i].position;
			//scales[i].GetComponent<Renderer>().material = materials[i%materials.Length];
			scales[i].transform.rotation = scalePositions[i].rotation;
			scales[i].transform.parent = scalePositions[i].GetComponent<SkinnedMeshRenderer>().rootBone;	
		}
	}
	
}
