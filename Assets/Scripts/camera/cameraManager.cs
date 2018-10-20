using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour {

	private Camera theCamera;
	private Transform cameraRig;
	private Vector3 lastMousePosition;

	[Header("Orbital")]
	[SerializeField] private float orbitSensitivity = 1f;
	[SerializeField] private bool infiniteOrbit = false;
	[Header("Zoom")]
	[SerializeField] private float maxZoomDistance;
	[SerializeField] private float minZoomDistance;
	[SerializeField] private float scrollSensitivity = 1f;
	[Header("Pan")]
	[SerializeField] private float panSpeed = 0.1f;
	[SerializeField] private bool panCamera = false;

	void Start () {
		if (theCamera == null)	// CHECK FOR CAMERA
			theCamera = GetComponent<Camera>();
		if (theCamera == null)
			theCamera = Camera.main;
		if (theCamera == null)
			Debug.LogError("COULD NOT FIND A CAMERA");

		cameraRig = theCamera.transform.parent;	// GET CAMERA RIG FROM MAIN CAM PARENT
	}

	private void LateUpdate()
	{
		OrbitCamera();
		ZoomCamera();
		if (panCamera)
			PanCamera();		
	}


	void ZoomCamera()
	{
		float _delta = -Input.GetAxis("Mouse ScrollWheel");   // SCROLL DIFFERENCE AXIS

		//Move camera backwards or foward based on valu of delta
		Vector3 _posChange = theCamera.transform.localPosition  / scrollSensitivity * _delta;
		Vector3 _pos = theCamera.transform.localPosition + _posChange;

		_pos = _pos.normalized * Mathf.Clamp(_pos.magnitude, minZoomDistance, maxZoomDistance);	// CLAMP ZOOM DIASTANCE
		theCamera.transform.localPosition = _pos;
	}
	 
	void PanCamera()
	{
		Vector3 _input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

		//Move camera
		Vector3 _posChange = _input * panSpeed;
		_posChange = Quaternion.Euler(0, theCamera.transform.rotation.eulerAngles.y, 0) * _posChange;

		Vector3 _pos = cameraRig.transform.localPosition + _posChange;
		cameraRig.transform.position = _pos;
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
