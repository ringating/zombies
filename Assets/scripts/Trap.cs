using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapState { on = 0, cooldown = 1, ready = 2 }

public class Trap : MonoBehaviour 
{
	public float cooldownTime = 10;
	public float activeTime = 10;
	public float timeSinceLastActivated;

	public TrapState state = TrapState.cooldown;

	void Start()
	{
		switch(state)
		{
			case TrapState.on:
				timeSinceLastActivated = 0;
				break;

			case TrapState.cooldown:
				timeSinceLastActivated = activeTime;
				break;
			
			case TrapState.ready:
				timeSinceLastActivated = activeTime + cooldownTime;
				break;

			default:
				break;
		}
	}
	
	void Update()
	{
		timeSinceLastActivated += Time.deltaTime;

		if(timeSinceLastActivated < activeTime)
		{
			state = TrapState.on;
		}
		else if(timeSinceLastActivated < activeTime + cooldownTime)
		{
			state = TrapState.cooldown;
		}
		else
		{
			state = TrapState.ready;
		}
	}

	private void OnTriggerStay(Collider other)
    {
        if(state == TrapState.on)
		{
			Zombie zomb = other.GetComponent<ZombieCollider>().self;
			if(zomb){ zomb.Die(); }
		}
    }

	public bool Activate()
	{
		// returns true upon successful activation

		if(state == TrapState.ready)
		{
			timeSinceLastActivated = 0;
			return true;
		}

		return false;
	}
}
