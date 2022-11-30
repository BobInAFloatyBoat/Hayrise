using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoodyCamera : MonoBehaviour
{
	public float panSpeed = 50f;
	
	float totalA;
	float totalB;

	public float borderWidth = 10f;
	
	Quaternion startRot;
	
	void Update()
	{
		if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - borderWidth)
		{
			if (totalA <= 0.1f){
				transform.Rotate(panSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
				totalA += Time.deltaTime;
				totalB -= Time.deltaTime;
			}
		}

		if (Input.GetKey("s") || Input.mousePosition.y <= borderWidth)
		{
			if (totalB <= 0.5f){
				transform.Rotate(-panSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
				totalA -= Time.deltaTime;
				totalB += Time.deltaTime;
			}
		}
		/**
		if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - borderWidth)
		{
			transform.Rotate(0.0f, panSpeed * Time.deltaTime, 0.0f, Space.World);
		}

		if (Input.GetKey("a") || Input.mousePosition.x <= borderWidth)
		{
			transform.Rotate(0.0f, -panSpeed * Time.deltaTime, 0.0f, Space.World);
		}
		**/
		
	}
	
	void Start(){
		startRot = transform.rotation;
	}
	public void SetRotation()
	{
		transform.rotation = startRot;
		Destroy(this);
	}
}
