using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CursorNavMeshTarget : MonoBehaviour 
{
	public NavMeshAgent[] nmAgents;
	public Camera cam;
	
	void Update () 
	{
		this.transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
		//nmAgent.destination = this.transform.position;

		foreach (NavMeshAgent nma in nmAgents)
		{
			nma.destination = this.transform.position;
		}
	}
}
