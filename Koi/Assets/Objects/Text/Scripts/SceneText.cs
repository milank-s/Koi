using UnityEngine;
using System.Collections;

public class SceneText : MonoBehaviour {

	public string 	text;
	string[]		words;
	int 			wordCount;

	void Start () {
		Cursor.visible = false;
		words = text.Split(new char[] {' '});
	}
	
	// Update is called once per frame
	void Update () {
	}

	public string getText(){
		wordCount++;
		return words[wordCount % words.Length];
	}

}
