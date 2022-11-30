using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int damage;
	public float damageCooldown;
	float damageTimer;
	float costTimer;
	public float lookRange;
	
	public bool NPCTower;
	public bool gainTowerPoint;
	
	int team;
	
	GameObject Manager;
	
    // Start is called before the first frame update
    void Start()
    {
		team = GetComponent<UnitTeamInfo>().team;
		Manager = GameObject.Find("Manager");
		
		if (gainTowerPoint){
			Manager.GetComponent<TowerManager>().gainTower();
		}
	}

    // Update is called once per frame
    void Update()
    {
		damageTimer += Time.deltaTime;
		if (damageTimer >= damageCooldown){
			damageTimer = 0;
			checkArea();
		}
		
		if (!NPCTower){
			costTimer += Time.deltaTime;
			if (costTimer >= 15){
				DoCost();
				costTimer = 0;
			}
		}
    }
	
	void checkArea()
	{
		int layerMask = 1 << 8;
		RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.position+new Vector3(0,-100,0), out hit, Mathf.Infinity, layerMask))
        {
            //Debug.Log("Did Hit");
			
			if (hit.transform.tag == "PlayerArea"){
				DoDamage();
			}
			if (NPCTower){
				DoDamage();
			}
        }
        else
        {
            //Debug.Log("Did not Hit");
        }
	}
	
	void DoCost()
	{
		int layerMask = 1 << 8;
		RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.position+new Vector3(0,-100,0), out hit, Mathf.Infinity, layerMask))
        {
            //Debug.Log("Did Hit");
			
			if (hit.transform.tag == "PlayerArea"){
				if (Manager.GetComponent<PointManager>().currentPoints >= 5){
					Manager.GetComponent<PointManager>().currentPoints -= 5;
				}
			}
        }
	}
	
	void DoDamage()
	{
		Collider[] inRangeEnemies = Physics.OverlapSphere(transform.position, lookRange, 1<<7);
		foreach(Collider Enemies in inRangeEnemies)
		{
			if (Enemies.GetComponent<UnitTeamInfo>().team != team && Enemies.GetComponent<UnitTeamInfo>().team != 0){
				Enemies.GetComponent<UnitTeamInfo>().damage(damage);
				break;
			}
		}
	}
	
	void OnDrawGizmosSelected()
	{
		// makes a sphere around the character
		Gizmos.DrawWireSphere(transform.position, lookRange);
	}
}
