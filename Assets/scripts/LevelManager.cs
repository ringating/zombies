using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
	// this class handles spawning, zones, counting zombie kills

    public Transform player;
    public GameObject zombiePrefab;
	public const int MAXZOMBIES = 24;
	//[HideInInspector]
	public List<Zombie> zombs;

	// zone and spawn stuff
    public float spawnDelay = 0.05f;
    private float timeSinceLastSpawn = 0;
    private int lastSpawnIndex = -1;
	public SpawnZone[] spawnZones; // this might not be needed for anything
    public SpawnZone startingZone;
    public Transform[] activeSpawns;

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
		zombs = new List<Zombie>();
        UnityEngine.Object[] sz = FindObjectsOfType(typeof(SpawnZone));
        if(sz.Length < 1){ Debug.LogWarning("Scene has no SpawnZone objects!"); }
		spawnZones = new SpawnZone[sz.Length];
		for(int i = 0; i < sz.Length; i++){ spawnZones[i] = (SpawnZone)sz[i]; }

		if(startingZone)
        {
            startingZone.setActiveSpawns();
        }
	}
	
	void Update () 
	{
		// debug "kill all zombies" key
        if(Input.GetKeyDown("space"))
        {
            foreach(Zombie z in zombs)
            {
                Destroy(z.gameObject);
            }
            zombs.Clear();
        }

        // update each zombie's destination
        foreach(Zombie z in zombs)
        {
            z.nma.destination = player.position;
        }

        // spawn another zombie if necessary
        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn >= spawnDelay && zombs.Count < MAXZOMBIES)
        {
            timeSinceLastSpawn = 0;
            SpawnZomb();
        }
	}

    private bool SpawnZomb()
    {
        if(zombs.Count < MAXZOMBIES)
        {
            Zombie newZomb = Instantiate(zombiePrefab, activeSpawns[NextSpawnIndex()]).GetComponent<Zombie>();
            zombs.Add(newZomb);
            return true;
        }
        else{ return false; }
    }

    private int NextSpawnIndex()
    {
        if(lastSpawnIndex < 0 || lastSpawnIndex >= activeSpawns.Length-1)
        {
            lastSpawnIndex = 0;
        }
        else
        {
            ++lastSpawnIndex;
        }
        return lastSpawnIndex;
    }
}
