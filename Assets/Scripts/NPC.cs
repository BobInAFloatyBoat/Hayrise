using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
	public int damage;
	public float damageCooldown;
	float damageTimer;
	public float lookRange;
	
	Transform follow;
	float distToFollow;
	float radius;
	
	int team;
	float unstuckTimer;
	
	public bool takesUpPop = true;
	
	GameObject Manager;
	
	private UnityEngine.AI.NavMeshAgent agent;
	Animator anim;
	
    // Start is called before the first frame update
    void Start()
    {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		anim = GetComponentInChildren<Animator> ();
		team = GetComponent<UnitTeamInfo>().team;
		Manager = GameObject.Find("Manager");
		if (takesUpPop == true){
			Manager.GetComponent<PopulationManager>().spawnedUnit(team);
		}
    }

    // Update is called once per frame
    void Update()
    {
		//animation when moving
		if (agent.velocity != Vector3.zero){
			anim.SetFloat ("speedPercent", 0.5f, 0.1f, Time.deltaTime);
		}
		if (agent.velocity == Vector3.zero) {
			anim.SetFloat ("speedPercent", 0, 0.1f, Time.deltaTime);
		}
		
		if (follow == null){
			Destroy(gameObject);
		}
		distToFollow = Vector3.Distance(follow.position, transform.position);
		if (distToFollow >= radius){
			agent.destination = follow.position + new Vector3(Random.Range(radius,-radius),0,Random.Range(radius,-radius));
		}
		
		if (distToFollow < radius){
			unstuckTimer += Time.deltaTime;
			if (!agent.pathPending && agent.remainingDistance < 0.1f || unstuckTimer > 5){
				agent.destination = follow.position + new Vector3(Random.Range(radius,-radius),0,Random.Range(radius,-radius));
				unstuckTimer = 0;
			}
		}
		
		damageTimer += Time.deltaTime;
		if (damageTimer >= damageCooldown){
			Collider[] inRangeEnemies = Physics.OverlapSphere(transform.position, lookRange, 1<<7);
			foreach(Collider Enemies in inRangeEnemies)
			{
				if (Enemies.GetComponent<UnitTeamInfo>().team != team && Enemies.GetComponent<UnitTeamInfo>().team != 0){
					Enemies.GetComponent<UnitTeamInfo>().damage(damage);
					damageTimer = 0;
				}
			}
		}
    }
	
	public void spawn(float radiusC, Transform followC)
	{
		radius = radiusC;
		follow = followC;
	}
	
	public void destroyedUnit()
	{
		if (Manager != null && takesUpPop == true){
			Manager.GetComponent<PopulationManager>().destroyedUnit(team);
		}
		if (gameObject.name.Contains("Cart")){
			Manager.GetComponent<PopulationManager>().destroyedCart();
		}
		follow.gameObject.GetComponent<Banner>().destroyedUnit();
	}
	
	void OnDrawGizmosSelected()
	{
		// makes a sphere around the character
		Gizmos.DrawWireSphere(transform.position, lookRange);
	}
}
