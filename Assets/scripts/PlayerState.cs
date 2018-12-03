using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour 
{
	// constants
	public float maxHealth = 100f;
	public float healthRegenRate = 50f;
	public float healthRegenDelay = 3f;

	public float moveSpeed;		// idk good defaults for these yet
	public float sprintSpeed;
	public float acceleration;

	// variables
	private float health;
	private float speed;	// maybe unnecessary
	private Vector2 vel;


	void Start () 
	{
		// set starting values for variables
		health = maxHealth;
		speed = 0f;
		vel = Vector2.zero;
	}
	
	void Update () 
	{
		
	}
}
