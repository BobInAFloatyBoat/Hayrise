using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
	public AudioMixer audioMixer;
	
	public TMP_Dropdown resolutionDropdown;
	public TMP_Dropdown qualityDropdown;
	
	Resolution[] resolutions;
	
	public bool isMainMenu = true;
	
	void Start()
	{
		Time.timeScale = 1;
		
		if (isMainMenu == true)
		{
			resolutions = Screen.resolutions;
			
			resolutionDropdown.ClearOptions();
			
			List<string> options = new List<string>();
			
			int currentResolutionIndex = 0;		
			for (int i = 0;i< resolutions.Length;i++)
			{
				string option = resolutions[i].width + " x " + resolutions[i].height;
				options.Add(option);
				
				if (resolutions[i].width == Screen.currentResolution.width &&	resolutions[i].height == Screen.currentResolution.height)
				{
					currentResolutionIndex = i;
				}
			}
			
			resolutionDropdown.AddOptions(options);
			resolutionDropdown.value = currentResolutionIndex;
			resolutionDropdown.RefreshShownValue();
			
			qualityDropdown.value = QualitySettings.GetQualityLevel();
			qualityDropdown.RefreshShownValue();
		}
	}
	
	public void SetResolution(int resolutionIndex)
	{
		Resolution resolution = resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}
	
	public void SetVolume (float volume)
	{
		audioMixer.SetFloat("volume", volume);
	}
	
	public void SetQuality (int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
	}
	
	public void SetFullscreen (bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}
	
	
	
	public void loadMainMenu()
	{
		SceneManager.LoadScene("mainMenu");
	}
	
	public void loadMap()
	{
		SceneManager.LoadScene("map");
	}
	
	public void quit()
	{
		Application.Quit();
	}
	
	public void pause()
	{
		Time.timeScale = 0;
	}
	public void UnPause()
	{
		Time.timeScale = 1;
	}
	
}