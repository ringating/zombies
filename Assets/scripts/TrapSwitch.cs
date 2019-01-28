using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSwitch : MonoBehaviour 
{
	public Trap[] traps;
	public bool playerIsClose;

	void Start()
	{
		playerIsClose = false;
	}
	
	void Update()
	{
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
