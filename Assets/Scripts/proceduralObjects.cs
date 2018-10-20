using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proceduralObjects : MonoBehaviour {

	[Header("Spawning")]
	[SerializeField] private Transform chunkLoaderParent;
	[SerializeField] private GameObject[] models;   // SPAWNABLE GAME OBJECTS
	[SerializeField] float modelMinimumOffset; // MINIMUM DISTANCE FROM MODELS 
	[SerializeField] float chunkUpdateRate;	// Rate to check if a new chunk is needed
	[SerializeField] float chunkCleanerRate;	// Rate to check all chunks for cleaning

	[Header("Chunks")]
	[SerializeField] private GameObject chunkParent; // CHUNK PARENT PREFAB
	private Dictionary<Vector3, GameObject> chunkMap = new Dictionary<Vector3, GameObject>();  // LOADED CHUNKS
	[SerializeField] private float chunkSize;	// CHUNK SIZE IN WORLD UNITS (X,Y,Z are shared)
	[SerializeField] private float chunkOffset; // Distance between chunks
	[SerializeField] private int maxChunkCapacity;
	[SerializeField] private int minChunkCapacity;
	[SerializeField] private float chunkCleanerDistanceTrigger;
	[SerializeField] private float chunkCompoundSize = 3;



	// Use this for initialization
	void Start () {
		createChunkCompound(chunkLoaderParent.position);
		//StartCoroutine("chunkCleaner");
	}

	void createChunkCompound(Vector3 _pos)
	{
		for (int _chunkX = -1; _chunkX < chunkCompoundSize-1; _chunkX++)
		{
			for (int _chunkY = -1; _chunkY < chunkCompoundSize-1; _chunkY++)
			{
				for (int _chunkZ = -1; _chunkZ < chunkCompoundSize-1; _chunkZ++)
				{
					float x = (_chunkX * chunkSize * 2) + (_chunkX < 0?-chunkOffset:chunkOffset) + _pos.x;
					float y = (_chunkY * chunkSize * 2) + (_chunkY < 0 ? -chunkOffset : chunkOffset) + _pos.y;
					float z = (_chunkZ * chunkSize * 2) + (_chunkZ < 0 ? -chunkOffset : chunkOffset) + _pos.z;

					createChunk(new Vector3(x,y,z));
				}
			}
		}
	}
	void createChunk(Vector3 position) {
		//if (chunkMap.ContainsKey(position) == false)	// If no chunk is already at that position
		//{
			GameObject _chunkParent = (GameObject)Instantiate(chunkParent, position, Quaternion.identity);
			//chunkMap.Add(position, _chunkParent);
			Vector3[] spawnedModels = new Vector3[maxChunkCapacity];
			for (int _model = 0; _model < Random.Range(minChunkCapacity, maxChunkCapacity); _model++)
			{
				while (true)
				{
					bool rm = false;
					float _x = Random.Range(-(chunkSize / 2), (chunkSize / 2));
					float _y = Random.Range(-(chunkSize / 2), (chunkSize / 2)); ;
					float _z = Random.Range(-(chunkSize / 2), (chunkSize / 2)); ;
					Vector3 _modelPosition = new Vector3(_x, _y, _z);
					foreach (Vector3 _sm in spawnedModels)
					{
						if (Vector3.Distance(_sm, _modelPosition) < modelMinimumOffset)
						{
							rm = true;
						}
					}
					if (rm == false)
					{
						GameObject _m = (GameObject)Instantiate(models[Random.Range(0, models.Length)], _modelPosition, Quaternion.identity);
						_m.transform.SetParent(_chunkParent.transform);
						break;
					}
				}
			}
		//}
	}

	IEnumerator chunkUpdate()
	{
		while (true)
		{
			bool _build = true;
			if (chunkMap.Count > 0)
			{
				foreach (Vector3 _lChunks in chunkMap.Keys)
				{
					if (Vector3.Distance(_lChunks, chunkLoaderParent.position) < chunkSize + chunkOffset)
					{
						_build = false;
					}
				}
			}
			

			yield return new WaitForSeconds(chunkCleanerRate);
		}
	}

	/*IEnumerator chunkCleaner()
	{
		while (true)
		{
			if(chunkMap.Count > 0)
			foreach (GameObject _lChunks in chunkMap.Values)
			{
				if (Vector3.Distance(_lChunks.transform.position, chunkLoaderParent.position) > chunkCleanerDistanceTrigger)
				{
					chunkMap.Remove(_lChunks.transform.position);
					Destroy(_lChunks);
				}
			}
			yield return new WaitForSeconds(chunkCleanerRate);
		}
	}*/
}
