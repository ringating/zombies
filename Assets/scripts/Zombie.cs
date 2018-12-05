using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
	public float hitTimer;
	public float timeToHit;
	[HideInInspector]
	public NavMeshAgent nma;

	void Awake()
	{
		nma = GetComponent<NavMeshAgent>();
	}
}
