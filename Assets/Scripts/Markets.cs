using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Markets : MonoBehaviour
{
    GameObject Manager;
	float timer;
	public GameObject top;
	
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
		
		if (timer > 6)
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
				for(int i = 0; i < 5; i++){
					gameObject.layer = 7;
					Manager.GetComponent<PointManager>().gainPoints();
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
