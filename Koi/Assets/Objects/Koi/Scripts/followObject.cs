using UnityEngine;
using System.Collections;

public class followObject : MonoBehaviour {

	public GameObject target;
	public float x,y,z;
	public float smoothTime = 0.3f;
	private Vector3 velocity = Vector3.zero;
	Quaternion oldRotation,targetRotation, currentRotation;

	// Use this for initialization
	void Start () {
		//oldRotation = target.transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(target == null){return;}
		followTarget ();
	}
	
	void followTarget(){


		Vector3 targetPosition = target.transform.TransformPoint(new Vector3(x, y, z));
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

		Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position + transform.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime/smoothTime);

		/*targetRotation = target.transform.rotation; 
		Quaternion currentRotation = Quaternion.Lerp (oldRotation, targetRotation, 0.01f); 
		oldRotation = currentRotation; 
		transform.position = target.transform.position;
		transform.position += currentRotation * Vector3.left * x;
		transform.LookAt(target.transform, target.transform.TransformDirection(Vector3.up));*/
	}
}
