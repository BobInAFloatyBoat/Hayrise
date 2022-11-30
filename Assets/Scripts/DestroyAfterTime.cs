using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
	public float time;
	public GameObject obj;
	
    void Start()
    {
        Invoke("destroyIt", time);
    }
    void destroyIt()
    {
        Destroy(obj);
    }
}
