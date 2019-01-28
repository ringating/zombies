using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapSwitchState { noneReady = 0, someReady = 1, allReady = 2 }

public class TrapSwitch_Status : MonoBehaviour 
{
	public TrapSwitch tSwitch;
	public TrapSwitchState state;
	
	void Start()
	{
		state = TrapSwitchState.noneReady;
	}

	void Update()
	{
		int nReady = 0;
		foreach(Trap t in tSwitch.traps)
		{

		}
	}
}
