using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Building Part", menuName = "Build Part")]
public class buildPart : ScriptableObject
{
	public string partName;
	public GameObject partPrefab;
}
