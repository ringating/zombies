using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour 
{
	public const int MAXZOMBIES = 20;
	[HideInInspector]
	public Zombie[] zombs = new Zombie[MAXZOMBIES];
	public Transform[] spawners;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}
}
