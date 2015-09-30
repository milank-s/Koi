using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {
		
	float angle, speed, limit;
	Vector3 localAngle;

	void Start () {
		angle = 0;
		localAngle = this.transform.localEulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		spin ();
	}

	void spin(){

		localAngle = Vector3.Lerp (localAngle, new Vector3 (360, localAngle.y, localAngle.z), Time.deltaTime * speed);
		this.transform.localEulerAngles = localAngle;

		if (localAngle.x < 0) {
			this.enabled = false;
		}
	}

	public void setSpeed(float mult){
		speed = mult;

	}
}
