using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quests : MonoBehaviour
{
	GameObject Manager;
	float timer;
	int QuestsDone;
	
	public GameObject banditCamp;
	bool banditsSpawn;
	float banditAttack;
	float banditTimer;
	
	public GameObject Monolith;
	bool monoSpawn;
	float monoAttack;
	float monoTimer;
	
	public GameObject Portal;
	
	List<Transform> TotalTiles = new List<Transform>();
	int RandomInt;
	List<Transform> TotalVillages = new List<Transform>();
	
	public GameObject commanderA;
	public GameObject commanderB;
	Transform AIBase;
	
	public GameObject WinUI;
	public GameObject LoseUI;
	public GameObject SpeedUI;
	public GameObject[] players;
	bool ended;
	
	public GameObject[] cards;
	public GameObject[] cardsText;
	public TMP_Text tradePointText;
	public int tradePoints;
	public GameObject traderCoins;
	
	public GameObject heroBanner;
	public GameObject partyBanner;
	public GameObject HeroAttackBanner;
	
	public Slider TimeSlider;
	bool updateBar;
	
	List<GameObject> heroUnits = new List<GameObject>();
	Transform portalObj;
	
	AudioSource audioSource;
    public AudioClip[] audioSourcesSounds;
	
	void Start()
    {
		Manager = GameObject.Find("Manager");
		audioSource = Manager.GetComponent<AudioSource> ();
		foreach(GameObject card in cards){
			card.SetActive(false);
		}
		foreach(GameObject cardText in cardsText){
			cardText.SetActive(false);
		}
		traderCoins.SetActive(false);
	}
	public void updateText(){
		tradePointText.text = "Trade Points = " + tradePoints;
	}
	
	public void QuestisDone()
	{
		if (QuestsDone == 0){
			traderCoins.SetActive(true);
		}
		Collider[] Villages = Physics.OverlapSphere(transform.position, 100, 1<<7);
		foreach(Collider Tile in Villages){
			if (Tile.transform.name.Contains("VillageOutpost")){
				TotalVillages.Add(Tile.transform);
			}
		}
		
		QuestsDone ++;
		tradePoints ++;
		updateBar = true;
		updateText();
		
		if (QuestsDone == 2){
			cards[0].SetActive(true);
			cardsText[0].SetActive(true);
		}
		if (QuestsDone >= 3){
			banditsSpawnCommand();
		}
		if (QuestsDone == 4){
			HeroSpawn();
			cards[3].SetActive(true);
			cardsText[3].SetActive(true);
		}
		if (QuestsDone == 5){
			cards[1].SetActive(true);
			cardsText[1].SetActive(true);
		}
		if (QuestsDone == 6){
			HeroPartySpawn();
		}
		if (QuestsDone == 7){
			PlaySounds(0);
			monoSpawnCommand();
		}
		if (QuestsDone == 8){
			HeroPartySpawn();
		}
		if (QuestsDone == 9){
			cards[2].SetActive(true);
			cardsText[2].SetActive(true);
		}
		if (QuestsDone == 10){
			PlaySounds(1);
			GameObject.Find("Music").GetComponent<MusicController>().startMusic(1);
			spawnPortal();
		}
		if (QuestsDone == 12){
			GameObject marker = (GameObject)Instantiate(HeroAttackBanner, heroUnits[0].transform.position+new Vector3(0,2f,0), transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			foreach(GameObject Unit in heroUnits){
				Unit.transform.position = portalObj.position;
			}
		}
	}
	
	public void banditsSpawnCommand(){
		banditsSpawn = true;
		if (GameObject.Find("AI2BaseOutpost(Clone)") != null){
			AIBase = GameObject.Find("AI2BaseOutpost(Clone)").transform;
			GameObject UnitA = (GameObject)Instantiate(commanderA, AIBase.position+new Vector3(0,0.1f,0), transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
		}
		if (GameObject.Find("AI3BaseOutpost(Clone)") != null){
			AIBase = GameObject.Find("AI3BaseOutpost(Clone)").transform;
			GameObject UnitB = (GameObject)Instantiate(commanderB, AIBase.position+new Vector3(0,0.1f,0), transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
		}
	}
	public void monoSpawnCommand(){
		monoSpawn = true;
	}
	
	void HeroSpawn(){
		RandomInt = Random.Range(0, TotalVillages.Count);
		GameObject hero = (GameObject)Instantiate(heroBanner, TotalVillages[RandomInt].position+new Vector3(0,0.1f,0), transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
		heroUnits.Add(hero);
	}
	void HeroPartySpawn(){
		RandomInt = Random.Range(0, TotalVillages.Count);
		GameObject hero = (GameObject)Instantiate(partyBanner, TotalVillages[RandomInt].position+new Vector3(0,0.1f,0), transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
		heroUnits.Add(hero);
	}
	
	void Update()
	{
		timer += Time.deltaTime;
		
		if (banditsSpawn){
			banditTimer += Time.deltaTime;
			if (banditTimer > banditAttack){
				banditTimer = 0;
				spawnBandits();
			}
		}
		
		if (monoSpawn){
			monoTimer += Time.deltaTime;
			if (monoTimer > monoAttack){
				monoTimer = 0;
				spawnMonolith();
			}
		}
		
		if (updateBar){
			if (TimeSlider.value < QuestsDone){
				TimeSlider.value += 0.05f;
			}
			if (TimeSlider.value >= QuestsDone){
				updateBar = false;
			}
		}
	}
	
	public void spawnBandits()
	{
		Collider[] inRangeTiles = Physics.OverlapSphere(transform.position, 100, 1<<8);
		foreach(Collider Tile in inRangeTiles)
		{
			if (Tile.transform.tag == "UnclaimedLand"){
				TotalTiles.Add(Tile.transform);
			}
		}
		if (TotalTiles.Count > 0)
		{
			RandomInt = Random.Range(0, TotalTiles.Count);
			GameObject build = (GameObject)Instantiate(banditCamp, TotalTiles[RandomInt].position+new Vector3(0,0.1f,0), transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			banditAttack = Random.Range(30,50);
			TotalTiles.Clear();
		}
	}
	
	public void spawnMonolith()
	{
		Collider[] inRangeTiles = Physics.OverlapSphere(transform.position, 100, 1<<8);
		foreach(Collider Tile in inRangeTiles)
		{
			if (Tile.transform.tag == "UnclaimedLand"){
				TotalTiles.Add(Tile.transform);
			}
		}
		if (TotalTiles.Count > 0)
		{
			RandomInt = Random.Range(0, TotalTiles.Count);
			GameObject build = (GameObject)Instantiate(Monolith, TotalTiles[RandomInt].position+new Vector3(0,0.1f,0), transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			monoAttack = Random.Range(20,50);
			TotalTiles.Clear();
		}
	}
	
	public void spawnPortal()
	{
		Collider[] inRangeTiles = Physics.OverlapSphere(transform.position, 100, 1<<8);
		foreach(Collider Tile in inRangeTiles)
		{
			if (Tile.transform.tag == "UnclaimedLand"){
				TotalTiles.Add(Tile.transform);
			}
		}
		if (TotalTiles.Count == 0){
			foreach(Collider Tile in inRangeTiles){
				if (Tile.transform.tag == "AIAArea"){
					TotalTiles.Add(Tile.transform);
				}
			}
		}
		if (TotalTiles.Count == 0){
			foreach(Collider Tile in inRangeTiles){
				TotalTiles.Add(Tile.transform);
			}
		}
		if (TotalTiles.Count > 0)
		{
			RandomInt = Random.Range(0, TotalTiles.Count);
			GameObject build = (GameObject)Instantiate(Portal, TotalTiles[RandomInt].position+new Vector3(0,0.1f,0), transform.rotation * Quaternion.Euler (0,Random.Range(0,360),0));
			portalObj = build.transform;
			TotalTiles.Clear();
		}
	}
	
	public void WIN()
	{
		if (!ended){
			PlaySounds(3);
			monoSpawn = false;
			endGame();
			WinUI.SetActive(true);
		}
	}
	
	public void Lose()
	{
		if (!ended){
			PlaySounds(2);
			endGame();
			LoseUI.SetActive(true);
		}
	}
	
	void endGame()
	{
		GameObject.Find("CameraController").GetComponent<shoodyCamera>().SetRotation();
		GetComponent<PointManager>().currentPoints = 0;
		GetComponent<DayNightController>().changeLighting = false;
		SpeedUI.SetActive(true);
		ended = true;
		foreach(GameObject play in players)
		{
			play.GetComponent<Animator>().enabled = false;
		}
		GameObject.Find("Music").GetComponent<MusicController>().DelayMusic();
	}
	
	void PlaySounds(int number)
	{
		audioSource.pitch = Random.Range(0.7f, 1.3f);
		audioSource.PlayOneShot(audioSourcesSounds[number], 1);
	}
	
}
