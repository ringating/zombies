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
    public float slowScalar;

    public AxisHandler axes;
    public Rigidbody rb;
    public NavMeshAgent nma;

	// variables
	public float health;
    private float timeSinceHit;
	private Vector2 vel;
    private Vector2 targetVel;
    //[HideInInspector]
    public int closeZombieCount;
    private float speedScalar;

    private bool dead = false;

	void Start() 
	{
		// set starting values for variables
        timeSinceHit = healthRegenDelay;
		health = maxHealth;
		vel = Vector2.zero;
        closeZombieCount = 0;
        speedScalar = 1;
	}
	
	void Update()
	{
		// health regen
        timeSinceHit += Time.deltaTime;
        if(timeSinceHit > healthRegenDelay && health < maxHealth && !dead)
        {
            health += healthRegenRate * Time.deltaTime;
            if(health > maxHealth){ health = maxHealth; }
        }

        // set speed multiplier
        if(closeZombieCount > 0)
        {
            speedScalar = slowScalar;
        }
        else
        {
            speedScalar = 1;
        }

        if(dead){ speedScalar = 0; }

        // movement
        if(Input.GetButton("sprint"))
        {
            targetVel = axes.leftAxis * runSpeed * speedScalar;
        }
        else
        {
            targetVel = axes.leftAxis * walkSpeed * speedScalar;
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

        //rb.position += new Vector3(vel.x * Time.deltaTime, 0, vel.y * Time.deltaTime);
        nma.velocity = new Vector3(vel.x, 0, vel.y);
        //rb.velocity = new Vector3(vel.x, 0, vel.y);
	}

    public void Damage(float hpDamage)
    {
        health -= hpDamage;
        timeSinceHit = 0;
        //print("took " + hpDamage + " points of damage!");
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        dead = true;
        LevelManager.Instance.announcementHandler.Announce("YOU DIED", "major", 60, 5);
    }
}
