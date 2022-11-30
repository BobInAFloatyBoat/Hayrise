using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemPickup : MonoBehaviour
{
	RaycastHit hit;
    Vector3 mousePositon;
	bool followCursor;
	int number;
	
	public GameObject selectedItem;
	
	AudioSource audioSource;
    public AudioClip[] audioSourcesSounds;
	GameObject Manager;
	
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mousePositon = Input.mousePosition;
            //Create a ray from the camera to our space
            var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Shoot that ray and get the hit data
            if(Physics.Raycast(camRay, out hit))
            {
                //Do something with that data 
                //Debug.Log(hit.transform.tag);
                if (hit.transform.tag.Contains("PickUp"))
                {
					SelectUnit(hit.transform.gameObject);
                }
            }
        }
		
		if (Input.GetMouseButtonUp(0))
        {
			DeselectUnits();
		}
		
		if (followCursor == true && selectedItem != null){
			ItemFollowCursor();
		}
    }
	
	private void SelectUnit(GameObject item)
    {
        DeselectUnits();
		
		selectedItem = item;
		//selectedItem.GetComponent<Rigidbody>().isKinematic = true;
		selectedItem.GetComponent<Collider>().enabled = false;
		followCursor = true;
		PlaySounds(0);
		
    }
	
	private void DeselectUnits()
    {
		if (selectedItem != null){
			checkLocation();
			if (selectedItem.transform.tag.Contains("Outpost")){
				selectedItem.GetComponent<Outpost>().checkArea();
			}
			if (selectedItem.transform.tag.Contains("StoreHouse")){
				selectedItem.GetComponent<StoreHouse>().checkArea();
			}
			//selectedItem.GetComponent<Rigidbody>().isKinematic = false;
			selectedItem.GetComponent<Collider>().enabled = true;
			selectedItem.transform.position = new Vector3(selectedItem.transform.position.x,33.4f,selectedItem.transform.position.z);
			
			if (!selectedItem.name.Contains("Banner")){
				selectedItem.GetComponent<CheckPlacedArea>().checkArea();
			}
			selectedItem = null;
			followCursor = false;
		}
    }
	
	void checkLocation()
	{
		if (selectedItem.name.Contains("Banner")){
			return;
		}
		Collider[] inRange = Physics.OverlapSphere(selectedItem.transform.position, 1.5f, 1<<7);
		if (inRange.Length > 0){
			foreach(Collider Thing in inRange){
				if (Thing.gameObject.tag == "Soldier"){
					number ++;
				}
			}
			if (inRange.Length != number){
				if (selectedItem.transform.tag.Contains("Outpost") || selectedItem.transform.tag.Contains("StoreHouse")){
					selectedItem.transform.position = GameObject.Find("SpawnPos").transform.position;
					return;
				}
				if (selectedItem.transform.tag.Contains("Tower")){
					return;
				}
				selectedItem.transform.position += new Vector3(Random.Range(-1,1),0,Random.Range(-1,1));
				checkLocation();
			}
			number = 0;
		}
	}
	
	void ItemFollowCursor()
	{
		var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(camRay, out hit))
		{
			Vector3 moveTo = hit.point + new Vector3(0,0.1f,0);
			selectedItem.transform.position = moveTo;
		}
		
		if (Input.GetAxis("Mouse ScrollWheel") > 0) {
			selectedItem.transform.Rotate(new Vector3(0, -4f, 0), Space.Self);
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0) {
			selectedItem.transform.Rotate(new Vector3(0, 4f, 0), Space.Self);
		}
	}
	
	void Start()
	{
        Manager = GameObject.Find("Manager");
		audioSource = Manager.GetComponent<AudioSource> ();
    }
	
	void PlaySounds(int number)
	{
		audioSource.pitch = Random.Range(0.7f, 1.3f);
		audioSource.PlayOneShot(audioSourcesSounds[number], 1);
	}
}