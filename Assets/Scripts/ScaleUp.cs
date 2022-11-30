using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUp : MonoBehaviour
{
    bool isScaling = false;
	bool doOnce = true;
	
	Vector3 StartScale;
	
	public bool Pulse = true;
	
	void Start()
	{
		StartScale = transform.localScale;
		transform.localScale = new Vector3(0,0,0);
		StartCoroutine(scaleOverTime(StartScale, 1));
	}
	
	void Update()
	{
		if (!isScaling && doOnce){
			doOnce = false;
			if (Pulse){
				GetComponent<Pulse>().enabled = true;
			}
			Destroy(this);
		}
	}
	
	IEnumerator scaleOverTime(Vector3 toScale, float duration)
	{
		if (isScaling)
		{
			yield break;
		}
		isScaling = true;

		float counter = 0;

		//Get the current scale of the object to be moved
		Vector3 startScaleSize = transform.localScale;

		while (counter < duration)
		{
			counter += Time.deltaTime;
			transform.localScale = Vector3.Lerp(startScaleSize, toScale, counter / duration);
			yield return null;
		}

		isScaling = false;
	}
}
