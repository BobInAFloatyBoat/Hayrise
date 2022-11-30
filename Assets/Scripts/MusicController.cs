using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] audioSources;
	public int musicToPlay;
	
	bool dropAudioBool;
	bool raiseAudioBool;
	
	void Start()
	{
		audioSource = GetComponent<AudioSource> ();
		raiseAudioBool = true;
	}
	
	void Update()
	{
		if (dropAudioBool == true){
			dropAudio();
		}
		if (raiseAudioBool == true){
			raiseAudio();
		}
	}
	
	void dropAudio()
	{
		if (audioSource.volume > 0){
			audioSource.volume -= Time.deltaTime*1;
		}
		if (audioSource.volume <= 0){
			dropAudioBool = false;
		}
	}
	void raiseAudio()
	{
		if (audioSource.volume < 1){
			audioSource.volume += Time.deltaTime*1;
		}
		if (audioSource.volume >= 1){
			raiseAudioBool = false;
		}
	}
	
	public void startMusic(int time)
	{
		musicToPlay = time;
		dropAudioBool = true;
		Invoke("startMusicCommand", 3);
	}
	void startMusicCommand()
	{
		raiseAudioBool = true;
		audioSource.clip = audioSources[musicToPlay];
		audioSource.Play(0);
	}
	
	public void DelayMusic()
	{
		dropAudioBool = true;
		Invoke("startMusicBackCommand", 220);
	}
	void startMusicBackCommand()
	{
		raiseAudioBool = true;
	}
}
