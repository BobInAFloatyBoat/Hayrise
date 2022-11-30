using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlacedArea : MonoBehaviour
{
	public void checkArea()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position+new Vector3(0,1,0), transform.position+new Vector3(0,-100,0), out hit, Mathf.Infinity, 1<<8))
		{
			//Debug.Log("Did Hit" + hit.transform.name);
			
			if (hit.transform.tag == "PlayerArea"){
				gameObject.layer = 7;
			}
			else{
				gameObject.layer = 0;
			}
		}
		else
		{
			//Debug.Log("Did not Hit");
		}
	}
}
