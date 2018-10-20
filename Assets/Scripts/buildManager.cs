using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildManager : MonoBehaviour {

	[SerializeField] private Camera theCamera;

	[SerializeField] public GameObject spawnPrefab;

	[SerializeField] private LayerMask snapPointLayerMask;
	[SerializeField] private LayerMask raycastLayerMask;

	void Start () {
		theCamera = Camera.main;
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0))
			CheckLeftClick();
		if (Input.GetMouseButtonDown(1))
			CheckRightClick();
	}

	void CheckRightClick()	// CHECKS LEFT CLICK FOR OBJECT SETTINGS
	{
		Collider _cl = doRaycast();
		if (_cl == null)    // We have clicked on something
			return;

		GameObject _shipPart = findBaseShipPart(_cl);
		if (_shipPart == null)	// If the parent is equal to null, we clicked on something that dosnt have a parent, not a valid part of our ship
			return;

		objectSettings _settings = _shipPart.GetComponent<objectSettings>();
		if (_settings)// If it is a thruster component open thruster window, otherwise open normal window.
			_settings.openDialog(); ;
	}

	void CheckLeftClick()	// CHECKS LEFT CLICK FOR BUILDING
	{
		// IF LEFT MOUSE BUTTON WAS PRESSED		
		// IS THE MOUSE OVER A BLOCK?
		Collider _cl = doRaycast();	// CAST RAYCAST
		if (_cl != null)	// IF SOMETHING WAS HIT
		{
			int _maskForHitObject = 1 << _cl.gameObject.layer; // MASKED VALUE FOR CLICKED LAYER		(00000000001 << 3rd Layer == 00000001000)			  // COMPARE BITWISE IF LAYER CLICKED IS SNAP POINT LAYER MASK  (00000001000 & 00110001000 > 0)
			if ((_maskForHitObject & snapPointLayerMask) > 0)
			{
				buildPart(spawnPrefab, _cl);   // BUILD PART
			}
		}
	}

	Collider doRaycast() {
		Ray _ray = theCamera.ScreenPointToRay(Input.mousePosition); // Get ray from mouse position
		RaycastHit _hitInfo; // Raycast out parameter

		if (Physics.Raycast(_ray, out _hitInfo, raycastLayerMask))
		{ // Cast the ray and check for collision and OUT the data to _hitInfo, using the raycastLayerMask
			return _hitInfo.collider;
		}
		return null;
	}

	void buildPart(GameObject _prefab, Collider _hit) {	// BUILD A PART GIVEN A RAYCAST HIT
		//CLICKED SPAWN SPOT, INSTATIATE OBJECT
		Vector3 _spawnSpot = _hit.transform.position;    // POSITION OF CLICK COLLIDER
		Quaternion _spawnRotation = _hit.transform.rotation;   // GET ROTATION FOR SPAWN
		GameObject _go = (GameObject)Instantiate(_prefab, _spawnSpot, _spawnRotation);   // INSTATIATE SPAWN PREFAB
		_go.transform.SetParent(_hit.transform, true);     // SET PARENT TO SNAP POINT CONSERVING POSITION
		_go.transform.name = _prefab.name;

		// DISABLE RENDERER AND COLLIDER ON SNAP POINT
		_hit.GetComponent<Renderer>().enabled = false;
		_hit.GetComponent<Collider>().enabled = false;
	}

	GameObject findBaseShipPart(Collider _col)  // FINDS BASE SHIP PART FROM A COLLIDER IF EXISTANT
	{
		Transform _curr = _col.transform;
		while (_curr != null)
		{
			if (_curr.gameObject.tag == "ShipPart")
				return _curr.gameObject;
			_curr = _curr.parent;
		}
		return null;
	}
}
