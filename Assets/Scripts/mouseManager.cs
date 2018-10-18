using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseManager : MonoBehaviour {

	[SerializeField] private Camera theCamera;

	[SerializeField] private GameObject spawnPrefab;

	[SerializeField] private LayerMask snapPointLayerMask;
	[SerializeField] private LayerMask raycastLayerMask;

	void Start () {
		theCamera = Camera.main;
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) {  // IF LEFT MOUSE BUTTON WAS PRESSED

			// IS THE MOUSE OVER A BLOCK?
			Ray _ray = theCamera.ScreenPointToRay(Input.mousePosition); // Get ray from mouse position
			RaycastHit _hitInfo; // Raycast out parameter

			if (Physics.Raycast (_ray, out _hitInfo, raycastLayerMask)) { // Cast the ray and check for collision and OUT the data to _hitInfo, using the raycastLayerMask

				// IF RAYCAST HIT IS A SNAP POINT
				int _maskForHitObject = 1 << _hitInfo.collider.gameObject.layer; // MASKED VALUE FOR CLICKED LAYER		(00000000001 << 3rd Layer == 00000001000)
				if ( (_maskForHitObject & snapPointLayerMask) > 0)  // COMPARE BITWISE IF LAYER CLICKED IS SNAP POINT LAYER MASK  (00000001000 & 00110001000 > 0)
				{
					//CLICKED SPAWN SPOT
					Vector3 _spawnSpot = _hitInfo.transform.position;    // POSITION OF CLICK COLLIDER + NORMAL (DIRECTION)

					Quaternion _spawnRotation = _hitInfo.collider.transform.rotation;	// GET ROTATION FOR SPAWN
					Instantiate(spawnPrefab, _spawnSpot, _spawnRotation, _hitInfo.collider.transform);   // INSTATIATE SPAWN PREFAB
				}
			}
		}
	}

}
