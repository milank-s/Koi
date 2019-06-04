using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

public class movement : MonoBehaviour
{

	[Header("PLAYER CONTROL")] public bool playerControl;
	[Space(10)]
	
	public float xLimit, yLimit, zLimit, speed, sinePeriod;
	[Space(10)]
	
	GameObject[] spine, tail;
	public GameObject[] bones;
	GameObject root;// head;
	Vector3[] startRotation, curRotation;
	scaleBehaviour ScaleBehaviour;
	[Space(10)]
	float x, y, z, turnRadius, turnAngle, flipAngle, spinAngle, curSpeed, curTime, phase, frequency, spin, amplitude, spinRadius;
	bool isDead;

	void Start () {
		
		spinRadius 		= 90;
		spin 			= 0;
		spinAngle 		= spin;
		curSpeed 		= speed;
		x  				= 0;
		y				= 0;
		z 				= 0;
		phase 			= 0;
		frequency 		= speed;
		tail			= GameObject.FindGameObjectsWithTag("Tail");
		ScaleBehaviour	= this.GetComponentInChildren<scaleBehaviour> ();
		startRotation 	= new Vector3 [bones.Length];
		root 			= GameObject.Find ("root");
		//head 			= GameObject.Find ("head");
		amplitude 		= yLimit;
		isDead = false;

		for (int i = 0; i < bones.Length; i++) {
			startRotation [i] = bones [i].transform.localEulerAngles;
			curRotation = startRotation;
		}


		for (int i = 0; i < tail.Length; i++) {
			//tail [i].transform.parent = null;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		//if(!ni.isLocalPlayer){return;} //Only Local client controls this koi

		if (playerControl)
		{
			move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		}
		else
		{
			float xVal = 1;
			float yVal = 0;
			move(xVal, yVal);
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			isDead = true;
		}
		if (isDead) {
			die ();
		}

		amplitude = Mathf.Clamp(yLimit/curSpeed, 5f, 20f);
	}

	public void die(){
		spin = 75;
		speed = 0;
		GameObject.Find ("Master").GetComponent<followObject> ().smoothTime = Mathf.Lerp(GameObject.Find ("Master").GetComponent<followObject> ().smoothTime, 0, Time.deltaTime/10);
		ScaleBehaviour.dropScale();
	}

	
	void move(float xVal, float yVal){
	
		bool turning = false;
		turnRadius = curSpeed * 0.75f;

		if (yVal > 0) {
			turning = true;
			flipAngle = Mathf.Lerp (flipAngle, turnRadius, Time.deltaTime * curSpeed);
			z = Mathf.Lerp (z, -zLimit, Time.deltaTime * curSpeed);
		} else {
			flipAngle = Mathf.Lerp (flipAngle, 0, Time.deltaTime * curSpeed);
			z = Mathf.Lerp (z, 0, Time.deltaTime * curSpeed);
		}

		if (yVal < 0) {
			turning = true;
			flipAngle = Mathf.Lerp (flipAngle, -turnRadius, Time.deltaTime * curSpeed);
			z = Mathf.Lerp (z, zLimit, Time.deltaTime * curSpeed);
		} else {
			flipAngle = Mathf.Lerp (flipAngle, 0, Time.deltaTime * curSpeed);
			z = Mathf.Lerp (z, 0, Time.deltaTime * curSpeed);
		}


		if (xVal > 0) {
			turning = true;
			turnAngle = Mathf.Lerp (turnAngle, turnRadius, Time.deltaTime  * curSpeed);
			x = Mathf.Lerp (x, -xLimit, Time.deltaTime);

		} else {
			turnAngle = Mathf.Lerp (turnAngle, 0, Time.deltaTime  * curSpeed);
			x = Mathf.Lerp(x, 0, Time.deltaTime);
		}

		if (xVal < 0) {
			turning = true;
			turnAngle = Mathf.Lerp (turnAngle, -turnRadius, Time.deltaTime  * curSpeed);
			x = Mathf.Lerp (x, xLimit, Time.deltaTime);

		} else {
			turnAngle = Mathf.Lerp (turnAngle, 0, Time.deltaTime  * curSpeed);
			x = Mathf.Lerp(x, 0, Time.deltaTime);
		}

		spinAngle = Mathf.Lerp (spinAngle, spin, Time.deltaTime/5);

		if(Input.GetKeyDown(KeyCode.LeftShift)){
			spin += spinRadius;
			spinRadius = -spinRadius;
		}

		if (Input.GetKey (KeyCode.Space)) {
			turning = true;
		}
		if (Input.GetKey (KeyCode.RightShift)) {
			ScaleBehaviour.reattach ();
		}

		if (turning) {
			curSpeed = Mathf.Lerp (curSpeed, speed, Time.deltaTime * curSpeed);

		} else {
			curSpeed = Mathf.Lerp (curSpeed, speed/4, Time.deltaTime * curSpeed);
		}

		for (int i = 1; i < bones.Length; i++) {

			newFrequency ();
			y = Mathf.Sin (-Time.time * frequency + phase + (i / sinePeriod)) * amplitude;


//			curRotation [i] = new Vector3 ((x * (i + 1)), (y * (i+1f))/10, (z * (i + 1)));
			curRotation[i] = bones[i - 1].transform.eulerAngles + new Vector3(x, y, z);
			//(i + 1)
			// Vector3.Lerp (curRotation... , Time.deltaTime * curSpeed)
			bones [i].transform.eulerAngles = curRotation [i];
		}

		//this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, this.transform.localEulerAngles.y + turnAngle, this.transform.localEulerAngles.z + flipAngle);
		
		//root.transform.localEulerAngles = new Vector3(spinAngle, turnAngle, root.transform.localEulerAngles.z);
		this.transform.position += root.transform.right * Time.deltaTime * curSpeed*5;
		root.transform.localEulerAngles = new Vector3(root.transform.localEulerAngles.x, root.transform.localEulerAngles.y + turnAngle, root.transform.localEulerAngles.z + flipAngle);
	}


	void newFrequency(){
		float curr = (-Time.time * frequency + phase) % (2.0f * Mathf.PI);
		float next = (-Time.time * (curSpeed*2)) % (2.0f * Mathf.PI);
			phase = curr - next;
			frequency = curSpeed*2;
	}
}
