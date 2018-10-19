using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildManager : MonoBehaviour {

	[SerializeField] private Camera theCamera;

	[SerializeField] private GameObject spawnPrefab;

	[SerializeField] private LayerMask snapPointLayerMask;
	[SerializeField] private LayerMask raycastLayerMask;

	void Start () {
		theCamera = Camera.main;
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0))	// BUILD PART
		{  // IF LEFT MOUSE BUTTON WAS PRESSED		

			// IS THE MOUSE OVER A BLOCK?
			Ray _ray = theCamera.ScreenPointToRay(Input.mousePosition); // Get ray from mouse position
			RaycastHit _hitInfo; // Raycast out parameter

			if (Physics.Raycast (_ray, out _hitInfo, raycastLayerMask)) { // Cast the ray and check for collision and OUT the data to _hitInfo, using the raycastLayerMask

				// IF RAYCAST HIT IS A SNAP POINT
				int _maskForHitObject = 1 << _hitInfo.collider.gameObject.layer; // MASKED VALUE FOR CLICKED LAYER		(00000000001 << 3rd Layer == 00000001000)
				if ( (_maskForHitObject & snapPointLayerMask) > 0)  // COMPARE BITWISE IF LAYER CLICKED IS SNAP POINT LAYER MASK  (00000001000 & 00110001000 > 0)
				{
					buildPart(spawnPrefab, _hitInfo);	// BUILD PART
				}
			}
		}
	}

	void buildPart(GameObject _prefab, RaycastHit _hit) {	// BUILD A PART GIVEN A HIT
		//CLICKED SPAWN SPOT, INSTATIATE OBJECT
		Vector3 _spawnSpot = _hit.collider.transform.position;    // POSITION OF CLICK COLLIDER
		Quaternion _spawnRotation = _hit.collider.transform.rotation;   // GET ROTATION FOR SPAWN
		GameObject _go = (GameObject)Instantiate(_prefab, _spawnSpot, _spawnRotation);   // INSTATIATE SPAWN PREFAB
		_go.transform.SetParent(_hit.collider.transform, true);     // SET PARENT TO SNAP POINT CONSERVING POSITION

		// DISABLE RENDERER AND COLLIDER ON SNAP POINT
		_hit.collider.GetComponent<Renderer>().enabled = false;
		_hit.collider.GetComponent<Collider>().enabled = false;
	}

}
