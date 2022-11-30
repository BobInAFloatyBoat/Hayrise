using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    GameObject Manager;
	
	public float lookRange;
	int team;
	
	float Timer;
	
	AudioSource audioSource;
    public AudioClip[] audioSourcesSounds;
	
	void Start()
    {
        Manager = GameObject.Find("Manager");
		audioSource = Manager.GetComponent<AudioSource> ();
		team = GetComponent<UnitTeamInfo>().team;
		
		Manager.GetComponent<PopulationManager>().spawnedCart();
	}
	
	void Update()
	{
		Timer += Time.deltaTime;
		
		if (Timer >= 3){
			CheckArea();
			Timer = 0;
		}
	}
	
	void CheckArea()
	{
		Collider[] inRange = Physics.OverlapSphere(transform.position, lookRange, 1<<7);
		foreach(Collider Building in inRange)
		{
			if (Building.transform.name.Contains("VillageOutpost")){
				if (Building.GetComponent<Village>().timeNext <= 0)
				{
					PlaySounds(0);
					Building.GetComponent<Village>().setTime();
					GetComponent<UnitTeamInfo>().damage(100);
					break;
				}
			}
		}
	}
	
	void OnDrawGizmosSelected()
	{
		// makes a sphere around the character
		Gizmos.DrawWireSphere(transform.position, lookRange);
	}
	
	void PlaySounds(int number)
	{
		audioSource.pitch = Random.Range(0.7f, 1.3f);
		audioSource.PlayOneShot(audioSourcesSounds[number], 1);
	}
}
