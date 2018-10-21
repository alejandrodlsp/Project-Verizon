using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cubeSettings : objectSettings
{

	[Header("ObjectDialog")]
	[SerializeField] private GameObject dialogWindow;
	[SerializeField] private Text objectNameText;


	private void Start()
	{
		buildManager.instance.clearPopupsCallback += closeDialogWindow; // SUBSCRIBE TO CLEAR DIALOG DELEGATE
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			closeDialogWindow();
	}

	override public void removeObject()
	{

	}

	override public void openDialog()
	{
		closeDialogWindow();   // Close all existing windows
		buildManager.instance.buildingAvailable = false;
		objectNameText.text = transform.name; // Set object name on dialog
		dialogWindow.SetActive(true);   // Instantiate canvas
	}

	override public void closeDialogWindow()
	{
		buildManager.instance.clearPopupsCallback -= closeDialogWindow;
		buildManager.instance.buildingAvailable = true;
		dialogWindow.SetActive(false);
	}
}
