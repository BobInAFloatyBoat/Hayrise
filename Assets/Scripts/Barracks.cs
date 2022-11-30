using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour
{
    GameObject Manager;
	float timer;
	public GameObject top;
	
	public int unitCost;
	public GameObject bannerSpawn;
	Collider box;
	
	int team;
	
    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("Manager");
		box = GetComponent<Collider>();
		team = GetComponent<UnitTeamInfo>().team;
		
		top.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
		
		if (timer > 10)
		{
			if (box.enabled == true){
				checkArea();
			}
			timer = 0;
		}
    }
	
	void checkArea()
	{
		int layerMask = 1 << 8;
		RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.position+new Vector3(0,-100,0), out hit, Mathf.Infinity, layerMask))
        {
            //Debug.Log("Did Hit");
			
			top.SetActive(false);
			if (hit.transform.tag == "PlayerArea"){
				top.SetActive(true);
				if (Manager.GetComponent<PointManager>().currentPoints >= unitCost && Manager.GetComponent<PopulationManager>().currentPop[team] < Manager.GetComponent<PopulationManager>().allowedPop[team])
				{
					GameObject Units = (GameObject)Instantiate(bannerSpawn, transform.position + new Vector3(0,1,0), transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
					Manager.GetComponent<PointManager>().currentPoints -= unitCost;
					gameObject.layer = 7;
				}
			}
        }
        else
        {
            //Debug.Log("Did not Hit");
			top.SetActive(false);
        }
	}
}
