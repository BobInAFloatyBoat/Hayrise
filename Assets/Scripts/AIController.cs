using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    GameObject Manager;
	
	Material currentColor;
	public Material ChangeColor;
	MeshRenderer MeshRenderer;
	
	public float lookRange;
	int team;
	
	List<Transform> TotalTiles = new List<Transform>();
	int RandomInt;
	
	float TakeAreaTimer;
	float TakeAreaTime;
	
	public GameObject defender;
	public int defenderSpawnCount;
	
	public GameObject defendMarker;
	GameObject marker;
	
	public int towerNumber;
	public GameObject tower;
	
	public GameObject Outpost;
	
	void Start()
    {
        Manager = GameObject.Find("Manager");
		team = GetComponent<UnitTeamInfo>().team;
		
		if (team != 1){
			Manager.GetComponent<PointManager>().currentAreas[team] ++;
		}
		
		TakeAreaTime = Random.Range(60,140);
		
		RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.position+new Vector3(0,-100,0), out hit, Mathf.Infinity, 1<<8))
        {
			hit.transform.tag = "AIAArea";
			MeshRenderer = hit.transform.gameObject.GetComponent<MeshRenderer>();
			currentColor = MeshRenderer.materials[0];
			MeshRenderer.materials = new Material[1] {ChangeColor};
		}
		
		if (defenderSpawnCount > 0){
			GameObject Marker = (GameObject)Instantiate(defendMarker, transform.position, transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			marker = Marker;
		}
		for(int i = 0; i < defenderSpawnCount; i++){
			GameObject Units = (GameObject)Instantiate(defender, transform.position, transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			Units.GetComponent<NPC>().spawn(4, marker.transform);
			marker.GetComponent<Banner>().addUnit();
		}
		
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
	
	void Update()
	{
		TakeAreaTimer += Time.deltaTime;
		
		if (TakeAreaTimer >= TakeAreaTime){
			if (Manager.GetComponent<PointManager>().currentAreas[team] < 10){
				TakeArea();
			}
			TakeAreaTimer = 0;
		}
	}
	
	void TakeArea()
	{
		TakeAreaTime = Random.Range(60,110);
		Collider[] inRangeTiles = Physics.OverlapSphere(transform.position, lookRange, 1<<8);
		foreach(Collider Tile in inRangeTiles)
		{
			if (Tile.transform.tag == "UnclaimedLand"){
				TotalTiles.Add(Tile.transform);
			}
		}
		if (TotalTiles.Count > 0)
		{
			RandomInt = Random.Range(0, TotalTiles.Count);
			GameObject build = (GameObject)Instantiate(Outpost, TotalTiles[RandomInt].position+new Vector3(0,0.1f,0), transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			TotalTiles.Clear();
		}
	}
	
	
	
	public void destroyed()
	{
		MeshRenderer.materials = new Material[1] {currentColor};
		
		if (team != 1){
			Manager.GetComponent<PointManager>().currentAreas[team] --;
		}
	}
	
	void OnDrawGizmosSelected()
	{
		// makes a sphere around the character
		Gizmos.DrawWireSphere(transform.position, lookRange);
	}
}
