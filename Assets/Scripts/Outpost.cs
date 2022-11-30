using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outpost : MonoBehaviour
{
	GameObject Manager;
	public GameObject top;
	Transform currentArea;
	
	Material currentColor;
	public Material ChangeColor;
	MeshRenderer MeshRenderer;
	int team;
	
	AudioSource audioSource;
    public AudioClip[] audioSourcesSounds;
	
    // Start is called before the first frame update
    void Start()
    {
        top.SetActive(false);
        Manager = GameObject.Find("Manager");
		team = GetComponent<UnitTeamInfo>().team;
		
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
			
			if (hit.transform.tag == "UnclaimedLand"){
				hit.transform.tag = "PlayerArea";
				currentArea = hit.transform;
				MeshRenderer = hit.transform.gameObject.GetComponent<MeshRenderer>();
				currentColor = MeshRenderer.materials[0];
				MeshRenderer.materials = new Material[1] {ChangeColor};
				gameObject.tag = "Untagged";
				top.SetActive(true);
				Manager.GetComponent<PointManager>().currentAreas[team] ++;
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
		MeshRenderer.materials = new Material[1] {currentColor};
		Manager.GetComponent<PointManager>().currentAreas[team] --;
		currentArea.transform.tag = "UnclaimedLand";
	}
	
	void PlaySounds(int number)
	{
		audioSource.pitch = Random.Range(0.7f, 1.3f);
		audioSource.PlayOneShot(audioSourcesSounds[number], 1);
	}
}
