using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cubeSettings : objectSettings
{

	[Header("ObjectDialog")]
	[SerializeField] private GameObject dialogWindow;
	[SerializeField] private Text objectNameText;

	[Header("Logic")]
	public bool openMenu = false;

	override public void openDialog()
	{
		closeDialogWindow();   // Close all existing windows
		openMenu = true;//  Set menu state to open
		objectNameText.text = transform.name; // Set object name on dialog
		dialogWindow.SetActive(true);   // Instantiate canvas
	}

	override public void closeDialogWindow()
	{
		dialogWindow.SetActive(false);
		openMenu = false;
	}
}
