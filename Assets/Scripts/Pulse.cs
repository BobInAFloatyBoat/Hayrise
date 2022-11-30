using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
	public float minScale; // Minimum scale value
	public float maxScale; // Maximum scale value

	void Update()
	{
		float scale = Mathf.PingPong(Time.time /5, maxScale - minScale) + minScale;
		transform.localScale = new Vector3(scale, scale, scale);
	}
	
	void OnEnable()
	{
		transform.localScale = new Vector3(1,1,1);
	}
}
