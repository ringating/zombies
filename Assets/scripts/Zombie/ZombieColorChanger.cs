using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieColorChanger : MonoBehaviour 
{
	public MeshRenderer zombMR;
	public Zombie zombScript;
	public Color defaultColor = Color.white;
	public Color aggroColor = Color.red;
	
	void Update()
	{
		zombMR.material.color = Color.Lerp(defaultColor, aggroColor, zombScript.GetHitTimePercent());
	}
}
