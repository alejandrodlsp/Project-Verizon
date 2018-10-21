using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateSettings : MonoBehaviour {

	Vector3[] rotations = new Vector3[16];
	[SerializeField] Transform graphicsComponent;
	int index = 0;

	private void Start()
	{
		for (int x = 0; x < 4; x++)
		{
			for (int y = 0; y < 4; y++)
			{
				rotations[index] = new Vector3(x * 90, graphicsComponent.rotation.y, y * 90);
				index++;
			}
		}
		index = 0;
	}
	public void rotate() {
		graphicsComponent.rotation = Quaternion.Euler(rotations[index].x,rotations[index].y,rotations[index].z);
		if (index + 1 >= rotations.Length)
			index = 0;
		else
			index++;
	}
}
