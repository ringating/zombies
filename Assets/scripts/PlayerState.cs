using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour 
{
	// constants
	public float maxHealth = 100f;
	public float healthRegenRate = 50f;
	public float healthRegenDelay = 3f;

	public float walkSpeed;		// idk good defaults for these yet
	public float runSpeed;
	public float acceleration;

    public AxisHandler axes;
    public Rigidbody rigid;

	// variables
	private float health;
	private float speed;	// maybe unnecessary
	private Vector2 vel;
    private float timeSinceHit;


	void Start () 
	{
		// set starting values for variables
        timeSinceHit = healthRegenDelay;
		health = maxHealth;
		speed = 0f;
		vel = Vector2.zero;
	}
	
	void Update () 
	{
		// health regen
        timeSinceHit += Time.deltaTime;
        if(timeSinceHit > healthRegenDelay && health < maxHealth)
        {
            health += healthRegenRate * Time.deltaTime;
            if(health > maxHealth){ health = maxHealth; }
        }

        // movement
        vel = axes.leftAxis * walkSpeed * Time.deltaTime;
        rigid.position += new Vector3(vel.x, 0, vel.y);
	}
}
