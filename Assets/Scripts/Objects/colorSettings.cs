using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colorSettings : MonoBehaviour {

	[SerializeField] Renderer rootGraphics;
	[SerializeField] GameObject layoutGroup;
	[SerializeField] GameObject colorPrefab;
	[SerializeField] Color[] availableColors;

	public void Start()
	{
		for (int _col = 0; _col < availableColors.Length; _col++)
		{
			int _in = _col;
			GameObject _c = (GameObject)Instantiate(colorPrefab, layoutGroup.transform);
			_c.GetComponentInChildren<Image>().color = new Color(availableColors[_col].r, availableColors[_col].g, availableColors[_col].b);
			_c.GetComponentInChildren<Button>().onClick.AddListener(() => { this.changeColor(_in); });
			_c.GetComponentInChildren<Button>().onClick.AddListener(() => { this.GetComponent<cubeSettings>().closeDialogWindow(); });
		}
	}

	public void changeColor(int index)
	{
		rootGraphics.material.color = availableColors[index];
	}
}
