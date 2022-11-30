using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour
{
	GameObject Manager;
	
    public float timeNext;
	public GameObject marker;
	bool turnOn = true;
	
	public GameObject toSpawn;
	public float nextTime;
	
	void Start()
    {
		timeNext = Random.Range(timeNext,timeNext+10);
        Manager = GameObject.Find("Manager");
	}
	
	public void setTime()
	{
		timeNext = Random.Range(nextTime,nextTime+30);
		turnOn = true;
		marker.SetActive(false);
		Manager.GetComponent<Quests>().QuestisDone();
		
		float RandomInt = Random.Range(0,2);
		GameObject UnitA = (GameObject)Instantiate(toSpawn, transform.position+new Vector3(RandomInt,0.1f,RandomInt), transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
		
	}
	
	void Update()
	{
		timeNext -= Time.deltaTime;
		if (timeNext <= 0 && turnOn){
			marker.SetActive(true);
			turnOn = false;
		}
	}
}
