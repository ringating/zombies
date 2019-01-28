using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum TrapState { on = 0, cooldown = 1, ready = 2 }

public class Trap_Status : MonoBehaviour 
{
	public Trap trap;
	public TrapState state;
	
	void Start()
	{
		state = TrapState.ready;
	}

	void Update()
	{
		if(trap.timeSinceLastActivated < trap.activeTime)
		{
			state = TrapState.on;
		}
		else if(trap.timeSinceLastActivated < trap.activeTime + trap.cooldownTime)
		{
			state = TrapState.cooldown;
		}
		else
		{
			state = TrapState.ready;
		}
	}
}