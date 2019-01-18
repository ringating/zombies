using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCloseZombies : MonoBehaviour 
{
    public Zombie self;

    void OnTriggerEnter(Collider other)
    {
        ZombieCollider otherZombCol = other.GetComponent<ZombieCollider>();
        if(otherZombCol && otherZombCol.self != this.self)
        {
            this.self.closeZombies.Add(otherZombCol.self);
        }
    }

    void OnTriggerExit(Collider other)
    {
        ZombieCollider otherZombCol = other.GetComponent<ZombieCollider>();
        if(otherZombCol && otherZombCol.self != this.self)
        {
            this.self.closeZombies.Remove(otherZombCol.self);
        }
    }
}
