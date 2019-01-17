using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour 
{
	public Transform[] spawnPoints;
	
	void OnTriggerEnter(Collider col)
	{
		if(col.GetComponent<PlayerState>())
        {
            this.setActiveSpawns();
        }
	}

    public void setActiveSpawns()
    {
        LevelManager.Instance.activeSpawns = this.spawnPoints;
    }
}
