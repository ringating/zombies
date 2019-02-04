using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSwitchColorChanger : MonoBehaviour 
{
	public MeshRenderer trapMR;
	public TrapSwitch trapSwitchScript;
	public Color noneReadyColor = Color.red;
	public Color someReadyColor = new Color(1f, 0.5f, 0f, 1); // orange
	public Color allReadyColor = Color.green;
	
	void Update()
	{
		switch(trapSwitchScript.state)
		{
			case TrapSwitchState.noneReady:
				trapMR.material.color = noneReadyColor;
				break;
			case TrapSwitchState.someReady:
				trapMR.material.color = someReadyColor;
				break;
			case TrapSwitchState.allReady:
				trapMR.material.color = allReadyColor;
				break;
		}
	}
}
