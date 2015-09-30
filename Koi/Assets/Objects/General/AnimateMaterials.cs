using UnityEngine;
using System.Collections;

public class AnimateMaterials : MonoBehaviour {

	public Material[] Materials;
	public bool animate;
	public float interval, xOffset, yOffset, timeScale;
	float speed;
	float x, y; //alpha
	int index;


	// Use this for initialization
	void Start () {
		x = 0;
		y = 0;
		//alpha = 0;
		speed = interval;
		index = Random.Range (0, Materials.Length);
		Renderer r = this.GetComponentInChildren<Renderer> ();
		r.material = Materials[index];
	}
	
	// Update is called once per frame
	void Update () {
		moveMaterial ();
		//alpha = Mathf.Cos (Time.time*timeScale);
		if (animate && interval < 0) {
			index++;
			cycleMaterial ();
			interval = speed;
		} else {
			interval -= Time.deltaTime;
		}
	}

	void moveMaterial(){
		x += xOffset;
		y += yOffset;
		Materials [index].mainTextureOffset = new Vector2(x, y);
	}

	void cycleMaterial(){

		index = index % Materials.Length;
		//Materials [index].SetColor ("_EmissionColor", new Color (alpha, alpha,alpha, alpha));
		Renderer r = this.GetComponentInChildren<Renderer> ();
		r.material = Materials[index];

	}
}
