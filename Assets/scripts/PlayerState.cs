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

    public float staminaMax = 100f;
    public float staminaRegenRate = 50f;
    public float staminaRegenDelay = 0.25f;
    public float staminaRunRate = 25f;
    public float staminaCameraRate = 25f;
    public float staminaOveruseTimeout = 1f;

    public AxisHandler axes;
    public Rigidbody rb;
    public NavMeshAgent nma;

	// variables
	public float health;
    private float timeSinceHit;
	private Vector2 vel;
    private Vector2 targetVel;
    [HideInInspector]
    public int closeZombieCount;
    private float speedScalar;
    private bool sprintHeld;
    private float stamina;
    [HideInInspector]
    public bool zoomedOut;
    private float staminaTimeSinceLastUsed;
    private bool staminaOverused;

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
		sprintHeld = Input.GetButton("sprint");
        timeSinceHit += Time.deltaTime;
        staminaTimeSinceLastUsed += Time.deltaTime;
        
        // health regen
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
        if(sprintHeld && CanUseStamina())
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

        nma.velocity = new Vector3(vel.x, 0, vel.y);


        // stamina
        if(CanUseStamina())
        {
            if(sprintHeld)
            {
                stamina -= staminaRunRate * Time.deltaTime;
                staminaTimeSinceLastUsed = 0f;
            }

            if(zoomedOut)
            {
                stamina -= staminaCameraRate * Time.deltaTime;
                staminaTimeSinceLastUsed = 0f;
            }
        }

        if(staminaTimeSinceLastUsed > staminaRegenDelay && stamina < staminaMax)
        {
            // regen stamina
            stamina += staminaRegenRate * Time.deltaTime;
            if(stamina > staminaMax){ stamina = staminaMax; }
        }

        LevelManager.Instance.announcementHandler.Announce(GetStaminaBar(30, "-"), "stamina-debug", 1, 1);
        LevelManager.Instance.announcementHandler.SetAnnouncementColor("stamina-debug", new Color(0,0.53f,0,1));

        LevelManager.Instance.announcementHandler.Announce(GetHealthBar(30, "-"), "health-debug", 1, 1);
        LevelManager.Instance.announcementHandler.SetAnnouncementColor("health-debug", new Color(0.53f,0,0,1));
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
        LevelManager.Instance.announcementHandler.SetAnnouncementColor("major", new Color(0.8f,0,0,1));
    }

    public bool CanUseStamina()
    {
        // can use stamina when it's greater than 0 and not locked out from overuse
        bool ret;

        if(stamina < 0)
        {
            staminaOverused = true;
            ret = false;
        }
        else
        {
            if(!staminaOverused || staminaTimeSinceLastUsed > staminaOveruseTimeout)
            {
                staminaOverused = false;
                ret = true;
            }
            else
            {
                ret = false;
            }
        }

        return ret;
    }

    private string GetStaminaBar(int unitLength, string unit)
    {
        string staminaBar = "";
        int numChars = Mathf.CeilToInt(stamina / (staminaMax / unitLength));
        for(int i = 0; i < numChars; ++i)
        {
            staminaBar += unit;
        }
        return staminaBar;
    }

    private string GetHealthBar(int unitLength, string unit)
    {
        string healthBar = "";
        int numChars = Mathf.CeilToInt(health / (maxHealth / unitLength));
        for(int i = 0; i < numChars; ++i)
        {
            healthBar += unit;
        }
        return healthBar;
    }
}
