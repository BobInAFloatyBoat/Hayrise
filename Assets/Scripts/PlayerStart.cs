using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
	public GameObject cartCard;
	public GameObject cartCardText;
	public GameObject StartText;
	
	void Start()
	{
		cartCard.SetActive(false);
		cartCardText.SetActive(false);
	}
	
    void Update()
	{
		int layerMask = 1 << 8;
		RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.position+new Vector3(0,-100,0), out hit, Mathf.Infinity, layerMask))
        {
            //Debug.Log("Did Hit");
			
			if (hit.transform.tag == "PlayerArea"){
				cartCard.SetActive(true);
				cartCardText.SetActive(true);
				StartText.SetActive(false);
			}
        }
        else
        {
            //Debug.Log("Did not Hit");
        }
	}
}
