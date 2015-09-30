using UnityEngine;
using System.Collections;

public class CameraLink : MonoBehaviour {

	void Awake(){
		Camera.main.transform.parent.gameObject.GetComponent<followObject>().target = gameObject;
		return;
	}
}
