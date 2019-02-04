using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapColorChanger : MonoBehaviour
{
	public MeshRenderer trapMR;
	public Trap trapScript;
	public Color onColor = new Color(0f, 0.48f, 0.8f, 1f); // electric blue
	public Color standbyColor = Color.gray;
	public Color cooldownColor = Color.red;
	
	void Update()
	{
		switch(trapScript.state)
		{
			case TrapState.on:
				trapMR.material.color = onColor;
				break;
			case TrapState.cooldown:
				trapMR.material.color = cooldownColor;
				break;
			case TrapState.ready:
				trapMR.material.color = standbyColor;
				break;
		}
	}
}
