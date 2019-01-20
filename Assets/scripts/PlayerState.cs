using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerState : MonoBehaviour 
{
	// constants
	public float maxHealth = 100f;
	public float healthRegenRate = 50f;
	public float healthRegenDelay = 3f;

	public float walkSpeed;		// idk good defaults for these yet
	public float runSpeed;
	public float acceleration; // in % of walkSpeed per sec

    public AxisHandler axes;
    public Rigidbody rigid;
    public NavMeshAgent nma;

	// variables
	private float health;
    private float timeSinceHit;
	private Vector2 vel;
    private Vector2 targetVel;

	void Start() 
	{
		// set starting values for variables
        timeSinceHit = healthRegenDelay;
		health = maxHealth;
		vel = Vector2.zero;
	}
	
	void Update()
	{
		// health regen
        timeSinceHit += Time.deltaTime;
        if(timeSinceHit > healthRegenDelay && health < maxHealth)
        {
            health += healthRegenRate * Time.deltaTime;
            if(health > maxHealth){ health = maxHealth; }
        }

        // movement
        if(Input.GetButton("sprint"))
        {
            targetVel = axes.leftAxis * runSpeed;
        }
        else
        {
            targetVel = axes.leftAxis * walkSpeed;
        }
        if(vel.x > targetVel.x)
        {
            vel.Set(vel.x-acceleration, vel.y);
            if(vel.x < targetVel.x){ vel.Set(targetVel.x, vel.y); }
        }
        else if(vel.x < targetVel.x)
        {
            vel.Set(vel.x+acceleration, vel.y);
            if(vel.x > targetVel.x){ vel.Set(targetVel.x, vel.y); }
        }
        if(vel.y > targetVel.y)
        {
            vel.Set(vel.x, vel.y-acceleration);
            if(vel.y < targetVel.y){ vel.Set(vel.x, targetVel.y); }
        }
        else if(vel.y < targetVel.y)
        {
            vel.Set(vel.x, vel.y+acceleration);
            if(vel.y > targetVel.y){ vel.Set(vel.x, targetVel.y); }
        }
        //rigid.position += new Vector3(vel.x * Time.deltaTime, 0, vel.y * Time.deltaTime);
        nma.velocity = new Vector3(vel.x, 0, vel.y);
	}
}
