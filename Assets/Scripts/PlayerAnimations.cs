using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public int PlayerNumber;
	
	Animator anim;
	
	void Start()
	{
		anim = GetComponentInChildren<Animator> ();
		anim.SetInteger ("PlayerNumber", PlayerNumber);
	}
}
