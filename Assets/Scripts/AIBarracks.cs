using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBarracks : MonoBehaviour
{
    GameObject Manager;
	
	public float lookRange;
	int team;
	
	float AttackTimer;
	float AttackTime;
	
	public GameObject attacker;
	public int attackerSpawnCount;
	
	public GameObject attackMarker;
	GameObject marker;
	
	List<Transform> TotalEnemies = new List<Transform>();
	int RandomInt;
	
    void Start()
    {
		AttackTime = Random.Range(40,100);
        Manager = GameObject.Find("Manager");
		team = GetComponent<UnitTeamInfo>().team;
    }

    void Update()
	{
		AttackTimer += Time.deltaTime;
		
		if (AttackTimer >= AttackTime){
			AttackArea();
			AttackTimer = 0;
		}
	}
	
	void AttackArea()
	{
		AttackTime = Random.Range(40,100);
		Collider[] inRangeEnemy = Physics.OverlapSphere(transform.position, lookRange, 1<<7);
		foreach(Collider Enemy in inRangeEnemy)
		{
			if (Enemy.tag != "Soldier"){
				if (Enemy.GetComponent<UnitTeamInfo>().team != team && Enemy.GetComponent<UnitTeamInfo>().team != 0){
					TotalEnemies.Add(Enemy.transform);
				}
			}
		}
		if (TotalEnemies.Count > 0)
		{
			RandomInt = Random.Range(0, TotalEnemies.Count);
			if (Manager.GetComponent<PopulationManager>().currentPop[team] < Manager.GetComponent<PopulationManager>().allowedPop[team]){
				GameObject Marker = (GameObject)Instantiate(attackMarker, TotalEnemies[RandomInt].transform.position + new Vector3(0,2,0), transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
				marker = Marker;
			}
			for(int i = 0; i < attackerSpawnCount; i++){
				if (Manager.GetComponent<PopulationManager>().currentPop[team] < Manager.GetComponent<PopulationManager>().allowedPop[team]){
					GameObject Units = (GameObject)Instantiate(attacker, transform.position, transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
					Units.GetComponent<NPC>().spawn(5, marker.transform);
					marker.GetComponent<Banner>().addNPCUnit(Units);
				}
			}
			TotalEnemies.Clear();
		}
	}
	
	void OnDrawGizmosSelected()
	{
		// makes a sphere around the character
		Gizmos.DrawWireSphere(transform.position, lookRange);
	}
}
