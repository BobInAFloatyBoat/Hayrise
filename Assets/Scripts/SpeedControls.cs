using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedControls : MonoBehaviour
{
    public void speedA()
	{
		Time.timeScale = 1;
	}
	public void speedB()
	{
		Time.timeScale = 10;
	}
	public void speedC()
	{
		Time.timeScale = 100;
	}
	public void speedD()
	{
		Time.timeScale = 1000;
	}
}
