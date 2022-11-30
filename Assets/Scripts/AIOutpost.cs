using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOutpost : MonoBehaviour
{
    GameObject Manager;
	
	Material currentColor;
	public Material ChangeColor;
	MeshRenderer MeshRenderer;
	Transform currentArea;
	
	int team;
	
	public GameObject defender;
	public int defenderSpawnCount;
	
	public GameObject defendMarker;
	GameObject marker;
	
	public int towerNumber;
	public GameObject tower;
	
	public GameObject Barracks;
	
	void Start()
    {
        Manager = GameObject.Find("Manager");
		team = GetComponent<UnitTeamInfo>().team;
		
		RaycastHit hit;
        if (Physics.Raycast(transform.position+new Vector3(0,2,0), transform.position+new Vector3(0,-100,0), out hit, Mathf.Infinity, 1<<8))
        {
			hit.transform.tag = "AIAArea";
			currentArea = hit.transform;
			MeshRenderer = hit.transform.gameObject.GetComponent<MeshRenderer>();
			currentColor = MeshRenderer.materials[0];
			MeshRenderer.materials = new Material[1] {ChangeColor};
			if (team != 1){
				Manager.GetComponent<PointManager>().currentAreas[team] ++;
			}
		}
		StartCoroutine(SpawnBuildings());
		StartCoroutine(SetLayer());
	}
	
	IEnumerator SetLayer()
	{
		yield return new WaitForSeconds(1);
		gameObject.layer = 7;
    }
	
	IEnumerator SpawnBuildings()
	{
		yield return new WaitForSeconds(3);
		
		if (defenderSpawnCount > 0){
			GameObject Marker = (GameObject)Instantiate(defendMarker, transform.position, transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			marker = Marker;
		}
		for(int i = 0; i < defenderSpawnCount; i++){
			GameObject Units = (GameObject)Instantiate(defender, transform.position, transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			Units.GetComponent<NPC>().spawn(4, marker.transform);
			marker.GetComponent<Banner>().addUnit();
		}
		
		yield return new WaitForSeconds(5);
		
		if (Barracks != null){
			spawnBarracks();
		}
		
		yield return new WaitForSeconds(5);
		
		for(int i = 0; i < towerNumber; i++){
			spawnTower();
		}
	}
	
	void spawnTower()
	{
		Vector3 spawnLoc = new Vector3(Random.Range(-4,4),0,Random.Range(-4,4));
		float distance = Vector3.Distance(spawnLoc, transform.position);
		if (distance < 1){
			spawnTower();
			return;
		}
		
		Collider[] inRange = Physics.OverlapSphere(spawnLoc, 1, 1<<7);
		foreach(Collider Stuff in inRange)
		{
			if (Stuff.gameObject.tag == "Tower"){
				spawnTower();
				return;
			}
		}
		GameObject Units = (GameObject)Instantiate(tower, transform.position + spawnLoc, transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
	}
	
	void spawnBarracks()
	{
		Vector3 spawnLoc = new Vector3(Random.Range(-4,4),0,Random.Range(-4,4));
		float distance = Vector3.Distance(spawnLoc, transform.position);
		if (distance < 3){
			spawnBarracks();
			return;
		}
		
		Collider[] inRange = Physics.OverlapSphere(spawnLoc, 1, 1<<7);
		foreach(Collider Stuff in inRange)
		{
			if (Stuff.gameObject.tag == "Tower"){
				spawnBarracks();
				return;
			}
		}
		GameObject Units = (GameObject)Instantiate(Barracks, transform.position + spawnLoc, transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
	}
	
	public void destroyed()
	{
		currentArea.transform.tag = "UnclaimedLand";
		MeshRenderer.materials = new Material[1] {currentColor};
		if (team != 1){
			Manager.GetComponent<PointManager>().currentAreas[team] --;
		}
	}
}
