using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thruster : thrusterSettings{

	Rigidbody theRb;
	[SerializeField] private float thrusterForce = 10f;

	void Start () {
		keyCode = defaultKeyCode;	

		theRb = this.transform.root.GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
		if (theRb.isKinematic)
			return;

		if (Input.GetKey(keyCode))
		{
			Vector3 _theForce =  - this.transform.forward * thrusterForce;
			theRb.AddForceAtPosition(_theForce , this.transform.position, ForceMode.Force);
		}
	}

}
