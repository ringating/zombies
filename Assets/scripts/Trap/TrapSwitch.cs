using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapSwitchState { noneReady = 0, someReady = 1, allReady = 2 }

public class TrapSwitch : MonoBehaviour 
{
	public Trap[] traps;
	public bool playerIsClose;
	public TrapSwitchState state = TrapSwitchState.noneReady;

	private int nReady;

	void Start()
	{
		playerIsClose = false;
		state = TrapSwitchState.noneReady;
	}
	
	void Update()
	{
		// check whether to activate traps
		if(playerIsClose)
		{
			if(Input.GetButtonDown("activate"))
			{
				if(ActivateTraps() > 0)
				{
					print("at least 1 trap activated");
				}
				else
				{
					print("no traps activated");
				}
			}
		}

		// set state
		nReady = 0;
		foreach(Trap t in traps)
		{
			if(t.state == TrapState.ready)
			{
				++nReady;
			}
		}
		if(nReady == 0)
		{
			state = TrapSwitchState.noneReady;
		}
		else if(nReady < traps.Length)
		{
			state = TrapSwitchState.someReady;
		}
		else
		{
			state = TrapSwitchState.allReady;
		}
	}

	private void OnTriggerEnter(Collider other)
    {
        if(other.transform == LevelManager.Instance.player)
        {
			playerIsClose = true;
		}
    }

	private void OnTriggerExit(Collider other)
    {
        if(other.transform == LevelManager.Instance.player)
        {
			playerIsClose = false;
		}
    }

	private int ActivateTraps()
	{
		// returns the number of traps that successfully activate
		int nActivated = 0;
		foreach(Trap t in traps)
		{
			if(t.Activate())
			{
				++nActivated;
			}
		}
		return nActivated;
	}
}
