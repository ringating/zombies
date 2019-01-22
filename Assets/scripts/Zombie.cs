using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
	public float hitTimer;
	public float timeToHit;
	public NavMeshAgent nma;
    public DetectCloseZombies dcz;
    public GameObject physicalCollider;
    public List<Zombie> closeZombies;
    public int maxCloseZombies;

    public float minRandomRunTime;
    public float maxRandomRunTime;
    public float minRandomRunCooldown;
    public float maxRandomRunCooldown;
    private float randomRunTime;
    private float randomRunTimer;
    private Vector3 randomVel;
    private float timeSinceRandomRun;
    private float randomRunCooldown;

    //public bool testDie;

    void Start()
    {
        //testDie = false;

        closeZombies = new List<Zombie>();
        randomRunTimer = -1; 
        randomRunCooldown = minRandomRunCooldown + (Random.value * (maxRandomRunCooldown - minRandomRunCooldown));
    }

    void Update()
    {
        if(closeZombies.Count > maxCloseZombies)
        {
            Vector3 avgZombPos = Vector3.zero;
            for(int i = 0; i < closeZombies.Count; ++i)
            {
                if(closeZombies[i])
                {
                    avgZombPos += closeZombies[i].transform.position;
                }
                else
                {
                    closeZombies.RemoveAt(i);
                    --i;
                }
            }
            avgZombPos = avgZombPos / closeZombies.Count;
            
            if(randomRunTimer < 0 && timeSinceRandomRun > randomRunCooldown)
            {
                randomRunTime = minRandomRunTime + (Random.value * (maxRandomRunTime - minRandomRunTime));
                randomRunTimer = 0;
                Vector2 temp = Random.insideUnitCircle.normalized;
                randomVel = new Vector3(temp.x, 0, temp.y) * nma.desiredVelocity.magnitude;
                if(Vector3.Angle(nma.desiredVelocity, randomVel) > 90)
                {
                    randomVel *= -1;
                }
            }
        }

        if(randomRunTimer >= 0)
        {
            if(randomRunTimer >= randomRunTime)
            {
                randomRunTimer = -1;
                nma.velocity = nma.desiredVelocity;
                randomRunCooldown = minRandomRunCooldown + (Random.value * (maxRandomRunCooldown - minRandomRunCooldown));
                timeSinceRandomRun = 0;
            }
            else
            {
                randomRunTimer += Time.deltaTime;
                nma.velocity = randomVel;
            }
        }
        else
        {
            timeSinceRandomRun += Time.deltaTime;
        }

        //if(testDie){testDie=false;this.Die();}
    }

    public void Die()
    {
        if(LevelManager.Instance)
        {
            LevelManager.Instance.zombs.Remove(this);
        }
        Destroy(dcz.gameObject);
        Destroy(physicalCollider);
        Destroy(this.gameObject);
    }
}
