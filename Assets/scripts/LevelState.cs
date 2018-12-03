using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelState : MonoBehaviour 
{
	// constants
	public float maxHealth;
	public float healthRegenRate;
	public float healthRegenDelay;

	// variables
	public float health;


	void Start () 
	{
		health = maxHealth;
	}
	
	void Update () 
	{
		
	}
}
