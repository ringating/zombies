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
            print("player detected");
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.transform == LevelManager.Instance.player)
        {
            print("player escaped");
        }
    }
}
