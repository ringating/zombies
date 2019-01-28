using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapState { on = 0, cooldown = 1, ready = 2 }

public class Trap : MonoBehaviour 
{
	public float cooldownTime = 10;
	public float activeTime = 10;
	public float timeSinceLastActivated;

	public TrapState state;

	void Start()
	{
		timeSinceLastActivated = 0;
		state = TrapState.cooldown;
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
        if(timeSinceLastActivated < activeTime)
		{
			Zombie zomb = other.GetComponent<Zombie>();
			if(zomb){ zomb.Die(); }
		}
    }

	public bool Activate()
	{
		// returns true upon successful activation

		if(timeSinceLastActivated > activeTime + cooldownTime)
		{
			timeSinceLastActivated = 0;
			return true;
		}

		return false;
	}
}
