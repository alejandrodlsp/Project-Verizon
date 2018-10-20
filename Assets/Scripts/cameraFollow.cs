﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {

	[Header("Follow")]
	[SerializeField]private Transform target;
	[SerializeField] private float smoothSpeed = 10f;
	[SerializeField] private bool followTarget = false;

	void FixedUpdate () {
		if(followTarget)
			follow();
	}

	void follow()
	{
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, target.position, smoothSpeed * Time.deltaTime);
		transform.position = smoothedPosition;
	}
}
