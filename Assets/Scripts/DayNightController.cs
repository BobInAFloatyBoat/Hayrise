using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
	public Light sun;
    float sunInitialIntensity;
	
	public bool isDayTime;
	float timeTimer;
	public float fullDayTime;
	public float fullNightTime;
	public bool changeLighting = true;
	
	bool lightingDone;
	float lightFadeTime = 1;
	float initialReflection;
	float initialAmbient;
	
	float ambient = 1;
	float reflection = 1;
	float intensityMultiplier = 1;
	float nightLight = 0.7f;
    
    void Start() {
        sunInitialIntensity = sun.intensity;
		initialAmbient = RenderSettings.ambientIntensity;
		initialReflection = RenderSettings.reflectionIntensity;
		
		if (isDayTime){
			RenderSettings.ambientIntensity = 1;
			RenderSettings.reflectionIntensity = 1;
			sun.intensity = 1;
		}
		if (!isDayTime){
			ambient = nightLight;
			reflection = nightLight;
			intensityMultiplier = 0;
			
			RenderSettings.ambientIntensity = nightLight;
			RenderSettings.reflectionIntensity = nightLight;
			sun.intensity = 0;
		}
    }
    
    void Update() {
		
		if (isDayTime){
			timeTimer += Time.deltaTime;
			if (timeTimer >= fullDayTime){
				isDayTime = false;
				timeTimer = 0;
				lightingDone = false;
			}
		}
		
		if (!isDayTime){
			timeTimer += Time.deltaTime;
			if (timeTimer >= fullNightTime){
				isDayTime = true;
				timeTimer = 0;
				lightingDone = false;
			}
		}
		if (lightingDone == false && changeLighting == true){
			updateLighting();
		}
    }
	
	void updateLighting()
	{
		if (isDayTime){
			if (ambient < initialAmbient){
				ambient += lightFadeTime * Time.deltaTime;
			}
			if (reflection < initialReflection){
				reflection += lightFadeTime * Time.deltaTime;
			}
			if (intensityMultiplier < sunInitialIntensity){
				intensityMultiplier += lightFadeTime * Time.deltaTime;
			}
			if (intensityMultiplier >= sunInitialIntensity){
				lightingDone = true;
			}
		}
		if (!isDayTime){
			if (ambient > nightLight){
				ambient -= lightFadeTime * Time.deltaTime;
			}
			if (reflection > nightLight){
				reflection -= lightFadeTime * Time.deltaTime;
			}
			if (intensityMultiplier > 0){
				intensityMultiplier -= lightFadeTime * Time.deltaTime;
			}
			if (intensityMultiplier <= 0){
				lightingDone = true;
			}
		}
 
		RenderSettings.ambientIntensity = ambient;
		RenderSettings.reflectionIntensity = reflection;
        sun.intensity = intensityMultiplier;
    }
}
