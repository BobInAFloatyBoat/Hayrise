using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTeamInfo : MonoBehaviour
{
    public int team;
	
	public int maxHealth;
	float currentHealth;
	float healthTimer;
	bool moveDown;
	
	GameObject DamageIndicator;
	
	bool isEnableCoroutineExecuting;
	
	void Start()
	{
		currentHealth = maxHealth;
		DamageIndicator = gameObject.transform.GetChild(1).gameObject;
	}
	
	void Update()
	{
		//heal over time
		healthTimer = healthTimer + Time.deltaTime;
		if (healthTimer > 4)
		{
			if (currentHealth < maxHealth){
				currentHealth += Random.Range(1, 3);
			}
		}
		
		if (moveDown == true){
			transform.position += new Vector3(0,-0.4f * Time.deltaTime,0);
		}
	}
	
	public void damage(int damage)
	{
		healthTimer = 0;
		currentHealth -= damage;
		StartCoroutine(enableDamage(1));
		if (currentHealth <= 0)
		{
			gameObject.layer = 0;
			if (gameObject.name.Contains("Outpost")){
				if (gameObject.name.Contains("Player")){
					GetComponent<Outpost>().destroyed();
				}
				if (gameObject.name.Contains("AI")){
					if (transform.tag == "Outpost"){
						GetComponent<AIOutpost>().destroyed();
					}
					if (transform.tag == "Base"){
						GetComponent<AIController>().destroyed();
					}
				}
			}
			if (gameObject.name.Contains("Soldier")){
				GetComponent<NPC>().destroyedUnit();
				Destroy(GetComponent<UnityEngine.AI.NavMeshAgent>());
				Destroy(GetComponent<NPC>());
			}
			if (gameObject.name.Contains("AI5Portal")){
				GameObject.Find("Manager").GetComponent<Quests>().WIN();
			}
			if (gameObject.name.Contains("PlayerBaseOutpostTile")){
				GameObject.Find("Manager").GetComponent<Quests>().Lose();
			}
			if (gameObject.name.Contains("StoreHouse")){
				GetComponent<StoreHouse>().destroyed();
			}
			if (gameObject.name.Contains("PlayerTower")){
				GameObject.Find("Manager").GetComponent<TowerManager>().loseTower();
			}
			transform.tag = "Untagged";
			Invoke("destroyObj", 2);
			moveDown = true;
		}
	}
	
	void destroyObj()
	{
		Destroy(gameObject);
	}
	
	IEnumerator enableDamage(float time)
	{
		if (isEnableCoroutineExecuting){
			yield break;
		}
		isEnableCoroutineExecuting = true;
		if (DamageIndicator != null){
			DamageIndicator.SetActive(true);
		}
		yield return new WaitForSeconds(time);
		if (DamageIndicator != null){
			DamageIndicator.SetActive(false);
		}
		isEnableCoroutineExecuting = false;
	}
}
