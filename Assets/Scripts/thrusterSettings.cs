using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class thrusterSettings : objectSettings {

	public KeyCode keyCode;
	protected KeyCode defaultKeyCode = KeyCode.Space;// DEFAULT KEYCODE FOR THRUSTER


	[Header("ObjectDialog")]
	[SerializeField] private GameObject dialogWindow;
	[SerializeField] private Text objectNameText;

	[Header("Logic")]
	public bool openMenu = false;
	[SerializeField] protected KeyCode[] restrictedKeyCodes;

	[Header("Thruster Dialog")]
	[SerializeField] private Text thrusterKeyBindText;
	[SerializeField] private Slider thrusterForceSlider;
	[SerializeField] private Text thrusterFoceText;

	private bool keyBinding = false;

	void Update()
	{
		if (keyBinding) // If activelly looking for a keybind
		{
			binding();
		}
	}

	override public void openDialog()
	{
		closeDialogWindow();   // Close all existing windows
		openMenu = true;//  Set menu state to open
		keyBinding = true; // Actively look for a keybind
		objectNameText.text = transform.name; // Set object name on dialog
		dialogWindow.SetActive(true);	// Instantiate canvas
		thrusterKeyBindText.text = keyCode.ToString(); // Set object keycode on dialog
	}

	override public void closeDialogWindow()
	{
		dialogWindow.SetActive(false);
		openMenu = false;
		keyBinding = false;
	}

	void binding()
	{
		if (Input.anyKeyDown)
		{
			foreach (KeyCode _key in Enum.GetValues(typeof(KeyCode)))
			{   // Loop through all possible keycodes to check if any is pressed
				if (Input.GetKeyDown(_key)) // IF PRESSING KEYCODE
				{
					foreach (KeyCode _k in restrictedKeyCodes)  // LOOP THROUGH ALL RESTRICTED KEYCODES
					{
						if (_key == _k) // IF A RESTRICTED KEYCODE, RETURN
							return;
					}
					keyCode = _key;
					closeDialogWindow();
					return;
				}
			}
		}
	}
}
