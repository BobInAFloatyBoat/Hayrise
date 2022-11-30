using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
	public GameObject[] AIBases;
	
	public List<Transform> TotalTiles;
	int RandomInt;
	
    void Start()
    {
		foreach(GameObject Base in AIBases)
		{
			RandomInt = Random.Range(0, TotalTiles.Count);
			GameObject Units = (GameObject)Instantiate(Base, TotalTiles[RandomInt].position + new Vector3(0,0.2f,0), transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			TotalTiles.Remove(TotalTiles[RandomInt]);
		}
		Destroy(this);
	}
}
