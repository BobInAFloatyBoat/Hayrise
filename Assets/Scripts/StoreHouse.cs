using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreHouse : MonoBehaviour
{
    GameObject Manager;
	public GameObject top;
	
	public int MaxGain;
	
	AudioSource audioSource;
    public AudioClip[] audioSourcesSounds;
	
    // Start is called before the first frame update
    void Start()
    {
        top.SetActive(false);
        Manager = GameObject.Find("Manager");
		
		audioSource = Manager.GetComponent<AudioSource> ();
	}

    // Update is called once per frame
    void Update()
    {
		
    }
	
	public void checkArea()
	{
		RaycastHit hit;
        if (Physics.Raycast(transform.position+new Vector3(0,1,0), transform.position+new Vector3(0,-100,0), out hit, Mathf.Infinity, 1<<8))
        {
            //Debug.Log("Did Hit" + hit.transform.name);
			
			if (hit.transform.tag == "PlayerArea"){
				gameObject.tag = "Untagged";
				top.SetActive(true);
				Manager.GetComponent<PointManager>().maxPoints += MaxGain;
				PlaySounds(0);
			}
        }
        else
        {
            //Debug.Log("Did not Hit");
        }
	}
	
	public void destroyed()
	{
		Manager.GetComponent<PointManager>().maxPoints -= MaxGain;
	}
	
	void PlaySounds(int number)
	{
		audioSource.pitch = Random.Range(0.7f, 1.3f);
		audioSource.PlayOneShot(audioSourcesSounds[number], 1);
	}
}
