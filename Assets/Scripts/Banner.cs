using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banner : MonoBehaviour
{
	public GameObject spawnUnit;
	public int spawnCount;
	int currentUnits;
	
	public float radius;
	
	public float destroyAfterTime;
	float AfterTimer;
	List<GameObject> BannerUnits = new List<GameObject>();
	
    void Start()
    {
		for(int i = 0; i < spawnCount; i++)
		{
			GameObject PlacingObj = (GameObject)Instantiate(spawnUnit, transform.position, transform.rotation);
			PlacingObj.GetComponent<NPC>().spawn(radius, transform);
			BannerUnits.Add(PlacingObj);
			currentUnits ++;
		}
	}
	
	void Update()
	{
		if (destroyAfterTime > 0){
			AfterTimer += Time.deltaTime;
			if (AfterTimer > destroyAfterTime){
				AfterTimer = 0;
				foreach(GameObject Unit in BannerUnits){
					if (Unit != null){
						Unit.GetComponent<UnitTeamInfo>().damage(100);
					}
				}
			}
		}
	}
	
	void OnDrawGizmosSelected()
	{
		// makes a sphere around the character
		Gizmos.DrawWireSphere(transform.position, radius);
	}
	
	public void destroyedUnit(){
		currentUnits --;
		
		if (currentUnits <= 0){
			Destroy(gameObject);
		}
	}
	
	public void addUnit(){
		currentUnits ++;
	}
	
	public void addNPCUnit(GameObject unit){
		currentUnits ++;
		
		BannerUnits.Add(unit);
	}
}
