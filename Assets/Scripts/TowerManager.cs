using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
	public GameObject SpawnPos;
    public GameObject towerGod;
	public int currentTowers;
	
	List<Transform> TotalTiles = new List<Transform>();
	int RandomInt;
	
	public GameObject[] warning;
	
	public bool[] doOnce;
	
	void Start()
	{
		warning[3].SetActive(false);
	}
	
	public void gainTower()
	{
		currentTowers ++;
		
		if (currentTowers == 5 && doOnce[0]){
			doOnce[0] = false;
			GameObject marker = (GameObject)Instantiate(warning[0], SpawnPos.transform.position+new Vector3(0,2f,0), transform.rotation);
		}
		if (currentTowers == 7 && doOnce[1]){
			doOnce[1] = false;
			GameObject marker = (GameObject)Instantiate(warning[1], SpawnPos.transform.position+new Vector3(0,2f,0), transform.rotation);
		}
		if (currentTowers == 9 && doOnce[2])
		{
			doOnce[2] = false;
			spawnTower();
			GameObject marker = (GameObject)Instantiate(warning[2], SpawnPos.transform.position+new Vector3(0,2f,0), transform.rotation);
		}
	}
	void Update()
	{
		SetMarker();
	}
	public void SetMarker()
	{
		if (currentTowers == 6 && doOnce[3]){
			doOnce[3] = false;
			doOnce[4] = true;
			warning[3].SetActive(false);
		}
		if (currentTowers == 7 && doOnce[4]){
			doOnce[4] = false;
			doOnce[3] = true;
			warning[3].SetActive(true);
		}
	}
	
	public void loseTower()
	{
		currentTowers --;
	}
	
	public void spawnTower()
	{
		Collider[] inRangeTiles = Physics.OverlapSphere(transform.position, 100, 1<<8);
		foreach(Collider Tile in inRangeTiles)
		{
			if (Tile.transform.tag == "UnclaimedLand"){
				TotalTiles.Add(Tile.transform);
			}
		}
		if (TotalTiles.Count == 0){
			foreach(Collider Tile in inRangeTiles){
				if (Tile.transform.tag == "AIAArea"){
					TotalTiles.Add(Tile.transform);
				}
			}
		}
		if (TotalTiles.Count == 0){
			foreach(Collider Tile in inRangeTiles){
				TotalTiles.Add(Tile.transform);
			}
		}
		if (TotalTiles.Count > 0)
		{
			RandomInt = Random.Range(0, TotalTiles.Count);
			GameObject build = (GameObject)Instantiate(towerGod, TotalTiles[RandomInt].position+new Vector3(0,0.1f,0), transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			TotalTiles.Clear();
		}
	}
}
