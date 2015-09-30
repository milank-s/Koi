using UnityEngine;
using System.Collections;

public class AssignMaterialsToFaces : MonoBehaviour {

	public Material[] materials;
	// Use this for initialization
	void Start () {
		assignMaterials ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void assignMaterials(){

		Renderer[] faces = this.GetComponentsInChildren <Renderer>();

		foreach (Renderer r in faces) {
			r.material = materials [Random.Range (0, materials.Length)];
		}
	}
}
