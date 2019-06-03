using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scaleBehaviour: MonoBehaviour {

	public GameObject 	text;
	public Material[] 	materials;
	public float 		detachSpeed;
	GameObject[] 		scales;
	Transform[] 		scalePositions;
	Vector3[] 			positions;
	int 				scaleAmount;
	//float 				interval;
	string 				word;

	// Use this for initialization
	void Start () {
		//interval = detachSpeed;
		createArray ();
		word = "";
	}

	// Update is called once per frame
	void Update () {
		foreach (char c in Input.inputString) {
			if (c == "\b" [0]) {
				if (word.Length != 0) {
					word = word.Substring (0, word.Length - 1);
					}
				} else {

				if (c == "\n" [0] || c == "\r" [0] || c == " "[0] ) {
					// dropWord (word);
						word = "";
					} else {
						dropScale();
						word += c;
					}
				}
			}
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

	void createArray(){

		scalePositions = this.GetComponentsInChildren<Transform>();
		scales = new GameObject[scalePositions.Length];
		scaleAmount = scalePositions.Length-1;
		for(int i = 1; i < scalePositions.Length; i++){
			scales[i] = scalePositions[i].gameObject;
			scales[i].transform.parent = scales[i].GetComponent<SkinnedMeshRenderer>().rootBone;
			scales[i].GetComponent<MeshFilter>().mesh = scales[i].GetComponent<SkinnedMeshRenderer>().sharedMesh;
			Destroy(scales[i].GetComponent<SkinnedMeshRenderer>());
			scales[i].AddComponent<MeshRenderer>();
			scales[i].GetComponent<MeshRenderer>().material = materials[i%materials.Length];
		}
	}

	public void reattach(){
		if (scaleAmount >= scalePositions.Length -1) {
			return;
		}
		GameObject curScale = scales [scaleAmount];

		//Mesh snapshot = new Mesh ();
		curScale.transform.parent = scalePositions [scaleAmount].parent;
		curScale.transform.localScale = scalePositions [scaleAmount].localScale;
		curScale.GetComponent<Rigidbody> ().useGravity = false;
		curScale.GetComponent<Rigidbody> ().isKinematic = true;
		curScale.GetComponent<Rigidbody> ().freezeRotation = false;
		curScale.transform.position = scalePositions [scaleAmount].position;
		curScale.transform.rotation = scalePositions [scaleAmount].rotation;
		curScale.GetComponent<LineRenderer> ().enabled = false;
		scaleAmount++;
	}

	public void dropWord(string t){
		if (scaleAmount <= 0) {
			return;
		}
		GameObject curScale = scales [scaleAmount];
		curScale.transform.parent = null;
		curScale.GetComponent<Rigidbody> ().isKinematic = false;
		curScale.GetComponent<Rigidbody> ().freezeRotation = false;
		curScale.GetComponent<Rigidbody> ().useGravity = false;
		addRepellingForce (curScale, scaleAmount);
		scaleAmount--;
		GameObject newText = (GameObject)Instantiate(text, curScale.transform.position, new Quaternion(0, 0, 0, 0));
		newText.transform.parent = curScale.transform;
		newText.GetComponentInChildren<TextFader>().setText(t);
		GameObject.Find ("Master").GetComponent<playSound>().play();
	}
	public void dropScale(){

		if (scaleAmount <= 0) {
			return;
		}
			GameObject curScale = scales [scaleAmount];
			curScale.transform.parent = null;
			curScale.GetComponent<Rigidbody> ().isKinematic = false;
			curScale.GetComponent<Rigidbody> ().freezeRotation = false;
			curScale.GetComponent<Rigidbody> ().useGravity = false;
			addRepellingForce (curScale, scaleAmount);
			scaleAmount--;
			//interval = Random.Range(0f, detachSpeed);
	}

	void addCenteringForce(GameObject scale, int value){

		Vector3 direction = (scalePositions[value].position - scales[value].transform.position);
		scales[value].GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized, transform.position);

	}

	void addRepellingForce(GameObject scale, int value)	{
		Vector3 direction = (scales[value].transform.forward);
		scales[value].GetComponent<Rigidbody>().AddForceAtPosition(direction.normalized*100, transform.position);

	}
}
