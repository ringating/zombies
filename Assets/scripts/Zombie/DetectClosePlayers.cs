using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClosePlayers : MonoBehaviour 
{
	public Zombie self;

    void OnTriggerEnter(Collider other)
    {
        if(other.transform == LevelManager.Instance.player)
        {
            //print("player detected");
            self.playerIsClose = true;
            self.closePlayer = other.GetComponent<PlayerState>();
            self.closePlayer.closeZombieCount++;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.transform == LevelManager.Instance.player)
        {
            //print("player escaped");
            self.playerIsClose = false;
            self.closePlayer = null;
            other.GetComponent<PlayerState>().closeZombieCount--;
        }
    }
}
