using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointManager : MonoBehaviour
{
	public int[] currentAreas;
	public float currentPoints;
	public float maxPoints;
	
	bool doOnce;
	bool doOnceB;
	bool doOnceC;
	bool doOnceD;
	float timer;
	
	List<Transform> TotalTiles = new List<Transform>();
	int RandomInt;
	
	public TMP_Text pointsText;
	
	public GameObject areaMarker;
	public GameObject SpawnPos;
	public GameObject UnionBase;
	
    void Update()
    {
        pointsText.text = "Points = " + currentPoints;
		
		if (currentAreas[1] == 4 && !doOnceD){
			doOnceD = true;
			GameObject marker = (GameObject)Instantiate(areaMarker, SpawnPos.transform.position+new Vector3(0,2f,0), transform.rotation);
		}
		if (currentAreas[1] >= 5 && !doOnce){
			doOnce = true;
			GetComponent<Quests>().banditsSpawnCommand();
		}
		if (currentAreas[1] >= 9 && !doOnceB){
			doOnceB = true;
			GetComponent<Quests>().monoSpawnCommand();
		}
		
		timer += Time.deltaTime;
		if (timer > 3){
			if (currentAreas[2] <= 0 && currentAreas[3] <= 0 && !doOnceC){
				doOnceC = true;
				SpawnBase();
			}
		}
    }
	
	public void gainPoints()
	{
		if (currentPoints < maxPoints){
			currentPoints += 1;
		}
	}
	
	void SpawnBase()
	{
		Collider[] inRangeTiles = Physics.OverlapSphere(transform.position, 100, 1<<8);
		foreach(Collider Tile in inRangeTiles)
		{
			if (Tile.transform.tag == "UnclaimedLand"){
				TotalTiles.Add(Tile.transform);
			}
		}
		if (TotalTiles.Count > 0)
		{
			RandomInt = Random.Range(0, TotalTiles.Count);
			GameObject build = (GameObject)Instantiate(UnionBase, TotalTiles[RandomInt].position+new Vector3(0,0.1f,0), transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			TotalTiles.Clear();
		}
	}
}
