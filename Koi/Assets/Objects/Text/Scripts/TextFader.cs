using UnityEngine;
using System.Collections;

public class TextFader : MonoBehaviour {

	TextMesh Text;
	public float speed;
	public bool on, delete;
	// Use this for initialization
	void Start () {
		Text = this.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (on) {
			float alpha = Text.color.a;
			alpha -= Time.deltaTime / speed;
			Text.color = new Color (Text.color.r, Text.color.g, Text.color.b, alpha); 
			if (alpha <= 0 && delete) {
				Destroy (this.gameObject);
			}
		}
	}
	
	public void setDecay(int x){
		speed = x;
	}

	public void setText(string text){
		on = true;
		Text = this.GetComponent<TextMesh>();
		Text.color = new Color (Text.color.r, Text.color.g, Text.color.b, 1); 
		Text.text = text;
	}
}
