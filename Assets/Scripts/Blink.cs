using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
	private UnityEngine.AI.NavMeshAgent agent;
	Animator anim;
	
	float blinkTimer;
	float blinkTime;
	
    // Start is called before the first frame update
    void Start()
    {
		anim = GetComponentInChildren<Animator> ();
		blinkTime = Random.Range(10,15);
    }
	
	void Update()
	{
		blinkTimer += Time.deltaTime;
		if (blinkTimer > blinkTime){
			anim.SetTrigger ("blink");
			blinkTime = Random.Range(10,15);
			blinkTimer = 0;
		}
	}
}
