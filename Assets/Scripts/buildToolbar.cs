using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buildToolbar : MonoBehaviour {

	public GameObject buildButtonPrefab;
	public buildPart[] buildPartsPrefabs;

	buildManager theBuildManager;

	void Start () {
		theBuildManager = GameObject.FindObjectOfType<buildManager>();	// GET BUILD MANAGER FROM SCENE

		populateButtonList();
	}

	void populateButtonList() {	// POPULATE BUTTON TOOLBAR WITH ALL AVAILABLE PARTS
		foreach(buildPart part in buildPartsPrefabs)
		{
			GameObject buttonGameObject = (GameObject) Instantiate(buildButtonPrefab, this.transform);
			buttonGameObject.GetComponentInChildren<Text>().text = part.partName;

			buttonGameObject.GetComponent<Button>().onClick.AddListener( () => { theBuildManager.spawnPrefab = part.partPrefab; });	// ADD LISTENER TO BUTTON - ADD ANONYMOUS FUNCTION
		}
	}
	
}
