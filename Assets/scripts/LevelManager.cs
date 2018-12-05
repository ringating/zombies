using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
	// this class handles spawning, zones, counting zombie kills

	public const int MAXZOMBIES = 20;
	[HideInInspector]
	public Zombie[] zombs = new Zombie[MAXZOMBIES];

	// zone and spawn stuff
	public SpawnZone[] spawnZones;
	public int activeZone;
	public int[] adjacentZones;

	// allow this object to be globally accessible
	private static LevelManager _instance;
	public static LevelManager Instance{ get{ return _instance; } }

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			// if this isn't the only instance, destroy this
			Destroy(this.gameObject);
		} 
		else
		{
			// otherwise set the sole instance to this
			_instance = this;
		}
    }

	void Start () 
	{
		UnityEngine.Object[] sz = FindObjectsOfType(typeof(SpawnZone));

		spawnZones = new SpawnZone[sz.Length];

		for(int i = 0; i < sz.Length; i++)
		{
			spawnZones[i] = (SpawnZone)sz[i];
		}

		foreach(SpawnZone z in spawnZones)
		{
			int[] indecesOfAdjZones = new int[z.adjacentZones.Length];
			for(int j = 0; j < z.adjacentZones.Length; j++)
			{
				// populate temp with the indeces corresponding to the zone's adjacent zones
				indecesOfAdjZones[j] = Array.IndexOf(spawnZones, z.adjacentZones[j]);
			}
			z.SetAdjZones(indecesOfAdjZones);
			z.SetZoneIndex(Array.IndexOf(spawnZones, z));
		}
	}
	
	void Update () 
	{
		
	}
}
