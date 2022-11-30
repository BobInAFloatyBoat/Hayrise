using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buying : MonoBehaviour
{
	public Transform spawnPoint;
	public Transform PlayerBase;
	GameObject Manager;
	
    public GameObject farm;
	public GameObject outPost;
	public GameObject barracks;
	public GameObject castle;
	public GameObject tower;
	public GameObject cart;
	
	public int farmPrice;
	public int outpostPrice;
	public int barracksPrice;
	public int castlePrice;
	public int towerPrice;
	public int cartPrice;
	
	public GameObject storeHouse;
	public GameObject market;
	public GameObject guildHall;
	
	void Start()
    {
        Manager = GameObject.Find("Manager");
    }
	
	public void buyFarm()
	{
		if (Manager.GetComponent<PointManager>().currentPoints >= farmPrice){
			GameObject PlacingObj = (GameObject)Instantiate(farm, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			Manager.GetComponent<PointManager>().currentPoints -= farmPrice;
		}
	}
	
	public void buyOutpost()
	{
		if (Manager.GetComponent<PointManager>().currentPoints >= outpostPrice){
			GameObject PlacingObj = (GameObject)Instantiate(outPost, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			Manager.GetComponent<PointManager>().currentPoints -= outpostPrice;
		}
	}
	
	public void buyBarracks()
	{
		if (Manager.GetComponent<PointManager>().currentPoints >= barracksPrice){
			GameObject PlacingObj = (GameObject)Instantiate(barracks, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			Manager.GetComponent<PointManager>().currentPoints -= barracksPrice;
		}
	}
	
	public void buyCastle()
	{
		if (Manager.GetComponent<PointManager>().currentPoints >= castlePrice){
			GameObject PlacingObj = (GameObject)Instantiate(castle, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			Manager.GetComponent<PointManager>().currentPoints -= castlePrice;
		}
	}
	
	public void buyTower()
	{
		if (Manager.GetComponent<PointManager>().currentPoints >= towerPrice){
			GameObject PlacingObj = (GameObject)Instantiate(tower, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			Manager.GetComponent<PointManager>().currentPoints -= towerPrice;
		}
	}
	
	public void buyCart()
	{
		if (Manager.GetComponent<PointManager>().currentPoints >= cartPrice && Manager.GetComponent<PopulationManager>().cartNum < 1){
			GameObject build = (GameObject)Instantiate(cart, PlayerBase.position+new Vector3(0,0.1f,0), transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			Manager.GetComponent<PointManager>().currentPoints -= cartPrice;
		}
		
	}
	//TRADE POINTS
	
	public void buyStoreHouse()
	{
		if (Manager.GetComponent<Quests>().tradePoints >= 2){
			GameObject PlacingObj = (GameObject)Instantiate(storeHouse, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			Manager.GetComponent<Quests>().tradePoints -= 2;
			Manager.GetComponent<Quests>().updateText();
		}
	}
	
	public void buyMarket()
	{
		if (Manager.GetComponent<Quests>().tradePoints >= 2){
			GameObject PlacingObj = (GameObject)Instantiate(market, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			Manager.GetComponent<Quests>().tradePoints -= 2;
			Manager.GetComponent<Quests>().updateText();
		}
	}
	
	public void buyGuildHall()
	{
		if (Manager.GetComponent<Quests>().tradePoints >= 2){
			GameObject build = (GameObject)Instantiate(guildHall, spawnPoint.position, spawnPoint.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			Manager.GetComponent<Quests>().tradePoints -= 2;
			Manager.GetComponent<Quests>().updateText();
		}
		
	}
}
