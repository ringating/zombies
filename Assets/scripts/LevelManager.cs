using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
	// this class handles spawning, zones, counting zombie kills

    public Transform player;
    public PlayerState playerScript;

    public Announcements announcementHandler;

    public GameObject zombiePrefab;
	public int maxConcurrentZombies = 24; // max zombies alive at once
    public int totalZombies = 120; // total zombies for the entire level (not counting the final wave)
    public float bonusWave = 24;
    public int killedZombies;
	//[HideInInspector]
	public List<Zombie> zombs;

	// zone and spawn stuff
    public float spawnDelay = 0.05f;
    private float timeSinceLastSpawn = 0;
    private int lastSpawnIndex = -1;
	public SpawnZone[] spawnZones; // this might not be needed for anything
    public SpawnZone startingZone;
    public Transform[] activeSpawns;

    private bool announcedHalf = false;
    private float completionTime = -1f;
    private float timer = 0f;

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

	void Start()
	{
		killedZombies = 0;
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
	
	void Update()
	{
		// debug "kill all zombies" key
        // if(Input.GetKeyDown("space"))
        // {
        //     while(zombs.Count > 0)
        //     {
        //         zombs[0].Die();
        //     }
        // }

        // update each zombie's destination

        timer += Time.deltaTime;

        foreach(Zombie z in zombs)
        {
            z.nma.destination = player.position;
        }

        // spawn another zombie if necessary
        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn >= spawnDelay && zombs.Count < maxConcurrentZombies)
        {
            timeSinceLastSpawn = 0;
            SpawnZomb();
        }

        // announcements
        if(killedZombies >= totalZombies/2 && !announcedHalf)
        {
            announcementHandler.Announce("HALFWAY", "minor", 2f, 0.5f);
            announcedHalf = true;
        }
        if(killedZombies >= totalZombies)
        {
            if(completionTime < 0){ completionTime = timer;}
            announcementHandler.Announce("SUCCESS", "major", 1f, 0f);
            announcementHandler.Announce("completion time: " + Mathf.FloorToInt(completionTime / 60) + ":" + Math.Round(completionTime % 60, 1), "minor", 1f, 0f);
        }
	}

    private bool SpawnZomb()
    {
        if(zombs.Count < maxConcurrentZombies && killedZombies + zombs.Count < totalZombies)
        {
            int spawnIndex = NextSpawnIndex();
            Zombie newZomb = Instantiate(zombiePrefab, activeSpawns[spawnIndex].position, activeSpawns[spawnIndex].rotation).GetComponent<Zombie>();
            zombs.Add(newZomb);
            return true;
        }
        else{ return false; }
    }

    private int NextSpawnIndex()
    {
        if(lastSpawnIndex >= activeSpawns.Length-1 || lastSpawnIndex < 0)
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
