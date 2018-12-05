using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour 
{
	public Transform[] spawnPoints;
	public SpawnZone[] adjacentZones;
	
	[SerializeField]
	private int zoneIndex;
	[SerializeField]
	private int[] adjZones;
	
	void OnCollisionEnter2D(Collision2D col)
	{
		LevelManager.Instance.activeZone = this.zoneIndex;
		LevelManager.Instance.adjacentZones = this.adjZones;
	}

	public void SetZoneIndex(int index)
	{
		this.zoneIndex = index;
	}

	public void SetAdjZones(int[] adjacentZoneIndeces)
	{
		this.adjZones = adjacentZoneIndeces;
	}
}
