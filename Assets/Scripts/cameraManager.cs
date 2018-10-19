using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour {

	public Camera theCamera;
	private Transform cameraRig;
	private Vector3 lastMousePosition;

	[SerializeField] private float orbitSensitivity = 1f;
	[SerializeField] private float scrollSensitivity = 1f;
	[SerializeField] private bool infiniteOrbit = false;
	void Start () {
		if (theCamera == null)	// CHECK FOR CAMERA
			theCamera = GetComponent<Camera>();
		if (theCamera == null)
			theCamera = Camera.main;
		if (theCamera == null)
			Debug.LogError("COULD NOT FIND A CAMERA");

		cameraRig = theCamera.transform.parent;	// GET CAMERA RIG FROM MAIN CAM PARENT
	}

	private void Update()
	{
		OrbitCamera();
		ZoomCamera();
	}
	

	void ZoomCamera()
	{
		float delta = Input.GetAxis("Mouse ScrollWheel");   // SCROLL DIFFERENCE AXIS

		//Move camera backwards or foward based on valu of delta
		Vector3 _changeOfPosition = theCamera.transform.forward / scrollSensitivity * delta;
		theCamera.transform.Translate(_changeOfPosition);
	}

	void OrbitCamera () {

		if (Input.GetMouseButtonDown(1)) // MOUSE WAS PRESSED ON THIS FRAME
			lastMousePosition = Input.mousePosition;    // LOCK POSITION BEFORE MOVING

		// IF DOWN RIGHT MOUSE BUTTON
		if (Input.GetMouseButton(1)) {  
			// CURRENT POSITION OF THE MOUSE ON SCREEN
			Vector3 _currentMousePosition = Input.mousePosition;

			// CHANGE POSITION IN PIXELS
			Vector3 _mouseMovement = _currentMousePosition - lastMousePosition;

			// ORBIT CAMERA RIG AROUND FOCAL POINT
			Vector3 _rotationAngles = _mouseMovement / orbitSensitivity;
			if(infiniteOrbit)
				_rotationAngles *= Time.deltaTime;

			theCamera.transform.RotateAround( cameraRig.position, theCamera.transform.right, _rotationAngles.y);
			theCamera.transform.RotateAround( cameraRig.position, theCamera.transform.up, _rotationAngles.x);

			// ORIENTATE CAMERA TO LOOK AT FOCAL POINT
			Quaternion _lookRotation = Quaternion.LookRotation( - theCamera.transform.localPosition);
			theCamera.transform.rotation = _lookRotation;

			if(!infiniteOrbit)
				lastMousePosition = _currentMousePosition;	// RESET MOUSE POSITION
		}
	}
}
