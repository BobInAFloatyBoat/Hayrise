using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    public int[] allowedPop;
	public int[] currentPop;
	
	public int cartNum;
	
	public GameObject overIndicator;
	public GameObject cartIndicator;
	
	void Start()
	{
		overIndicator.SetActive(false);
		cartIndicator.SetActive(false);
	}
	
	public void spawnedUnit(int team)
	{
		currentPop[team] ++;
		
		if (team == 1){
			if (currentPop[team] >= allowedPop[team]){
				overIndicator.SetActive(true);
			}
		}
	}
	
	public void destroyedUnit(int team)
	{
		currentPop[team] --;
		
		if (team == 1){
			if (currentPop[team] <= allowedPop[team]){
				overIndicator.SetActive(false);
			}
		}
	}
	
	public void spawnedCart(){
		cartNum ++;
		
		if (cartNum >= 1){
			cartIndicator.SetActive(true);
		}
	}
	
	public void destroyedCart(){
		cartNum --;
		
		if (cartNum < 1){
			cartIndicator.SetActive(false);
		}
	}
}
