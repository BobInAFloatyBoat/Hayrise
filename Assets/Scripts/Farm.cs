using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
	GameObject Manager;
	float timer;
	public GameObject hay;
	
	Collider box;
	
    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("Manager");
		box = GetComponent<Collider>();
	}

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
		
		if (timer > 5)
		{
			if (box.enabled == true && Manager.GetComponent<DayNightController>().isDayTime == true){
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
			
			hay.SetActive(false);
			if (hit.transform.tag == "PlayerArea"){
				hay.SetActive(true);
				Manager.GetComponent<PointManager>().gainPoints();
				gameObject.layer = 7;
			}
        }
        else
        {
            //Debug.Log("Did not Hit");
			hay.SetActive(false);
        }
	}
}
