using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class objectSettings : MonoBehaviour {

	abstract public void openDialog();

	abstract public void closeDialogWindow();

	abstract public void removeObject();
}
