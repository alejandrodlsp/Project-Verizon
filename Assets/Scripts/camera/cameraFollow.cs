using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {

	[Header("Follow")]
	[SerializeField]private Transform target;
	[SerializeField] private bool followTarget = false;

	void FixedUpdate () {
		if(followTarget)
			follow();
	}

	void follow()
	{
		transform.position = target.position;
	}
}
