using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawnSBuild : MonoBehaviour
{
    public int buildingNumber;
	public GameObject building;
	
	void Start()
	{
		for(int i = 0; i < buildingNumber; i++){
			spawn();
		}
	}
	
	void spawn()
	{
		Vector3 spawnLoc = new Vector3(Random.Range(-4,4),0,Random.Range(-4,4));
		float distance = Vector3.Distance(spawnLoc, transform.position);
		if (distance < 2){
			spawn();
			return;
		}
		
		Collider[] inRange = Physics.OverlapSphere(spawnLoc, 1, 1<<7);
		foreach(Collider Stuff in inRange)
		{
			if (Stuff.gameObject.tag == "Tower"){
				spawn();
				return;
			}
		}
		GameObject Units = (GameObject)Instantiate(building, transform.position + spawnLoc, transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
	}
}
