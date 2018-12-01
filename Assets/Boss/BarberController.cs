using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarberController : MonoBehaviour {

    [SerializeField] private BarberMovement movement;
    [SerializeField] private Damagable damageable;
    private bool isSwooping = false; // TODO: we can probably remove this when we implement the actual behavior
    [SerializeField] Vector3[] waypoints = { new Vector3(39f, 23f, 0f), new Vector3(21f, 23f, 0f) };
    [SerializeField] GameObject[] traps = new GameObject[0];
    int currentWaypoint = 0;
    int currentTrap = 0;

    private int stage = 0;
    [SerializeField] private float timePerTrap = 2f; // the time a given trap stays active before cycling to the next one
    private float trapCyclePeriod; // the total time it takes to cycle between all traps

	private bool spawn0active, spawn1active, spawn2active;
	[SerializeField] private float spawnTime;          // How long between each spawn.
	[SerializeField] private float deathCounter; 
	[SerializeField] private Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	Vector2 whereToSpawn; 
	PlayerState playerHealth;        						  // Reference to the player's heatlh.
	[SerializeField] private GameObject player; 
	[SerializeField] private GameObject StraightEdge;         // The enemy prefab to be spawned.
	[SerializeField] private GameObject ElectricRazor;        // The enemy prefab to be spawned.
	[SerializeField] private float spawnRadius = 5.0f; 
	[SerializeField] private int maxEnemyCounter = 50;
	private int enemyCounter;

    private void Start()
    {
        trapCyclePeriod = traps.Length * timePerTrap;
        movement.MoveTo(waypoints[currentWaypoint]);
		InvokeRepeating ("Spawn", spawnTime, deathCounter);
    }

    private void FixedUpdate()
    {

        //update stage
        if (damageable.currentHealth == 7)
        {
            movement.SetSpeedIncreased();
            stage = 1;
			spawn0active = true;
			SpawnArray [0] = 0;

        }
        else if(damageable.currentHealth == 3)
        {
            stage = 2;
			SpawnArray[1] = 1;
			SpawnArray[2] = 2;
			spawn1active = true;
			spawn2active = true;
        }

        // behave according to which stage
        if(stage == 0)
        {
            StageOne();
        }
        else if(stage == 1)
        {
            StageTwo();
        }
        else
        {
            StageThree();
        }
  
    }

    // move through each waypoint in a cycle
    private void CycleWaypoints()
    {
        if (movement.IsWaypointReached())
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            if (isSwooping)
                movement.MoveTo(waypoints[currentWaypoint]);
            else
                movement.SwoopTo(waypoints[currentWaypoint]);
            isSwooping = !isSwooping;
        }
    }

    // cycle through activating and deactivating traps
    private void CycleTraps()
    {
        Debug.Log((Time.timeSinceLevelLoad % trapCyclePeriod));
        if((Time.timeSinceLevelLoad % trapCyclePeriod) > (timePerTrap * (currentTrap+1)) || (currentTrap == traps.Length-1 && (Time.timeSinceLevelLoad % trapCyclePeriod) < timePerTrap))
        {
            Debug.Log("olo");
            traps[currentTrap].SetActive(false);
            currentTrap = (currentTrap + 1) % traps.Length;
            traps[currentTrap].SetActive(true);
        }
    }

    private void StageOne()
    {
        CycleWaypoints();
    }

    private void StageTwo()
    {
        CycleWaypoints();
    }

    private void StageThree()
    {
        CycleWaypoints();
        CycleTraps();
    }

	void Spawn ()
	{
		Debug.Log("Spawning");
		// If the player has no health left, if the max enemies have generated, or we have left the trigger...
		if (enemyCounter == maxEnemyCounter)
		{
			return;

		}

		if (enemyCounter > 5 && spawn1active == false && spawn2active == false) {
			spawn0active == false; 
		}

		if (enemyCounter > 10 && spawn1active == true && spawn2active == false) {
			spawn0active == false; 
			spawn1active == false; 
		}

		int spawnPointIndex = UnityEngine.Random.Range (0, 3); 
		Debug.Log ("Spawn Point is " + spawnPointIndex); 

		if (spawnPointIndex == 0 && spawn0active) {
			float randX = UnityEngine.Random.Range (-2f, 2f);
			whereToSpawn = new Vector2 ((spawnPoints [spawnPointIndex].position.x + (randX * spawnRadius)),
				(spawnPoints [spawnPointIndex].position.y + (randX * spawnRadius)));
			Instantiate (ElectricRazor, whereToSpawn, spawnPoints [spawnPointIndex].rotation);

		} else if (spawnPointIndex == 1 && spawn1active) {
			float randX = UnityEngine.Random.Range (-2f, 2f);
			whereToSpawn = new Vector2 ((spawnPoints [spawnPointIndex].position.x + (randX * spawnRadius)),
				(spawnPoints [spawnPointIndex].position.y + (randX * spawnRadius)));
			Instantiate (ElectricRazor, whereToSpawn, spawnPoints [spawnPointIndex].rotation);

		} else if (spawn2active) {
			float randX = UnityEngine.Random.Range (-2f, 2f);
			whereToSpawn = new Vector2 ((spawnPoints [spawnPointIndex].position.x + (randX * spawnRadius)),
				(spawnPoints [spawnPointIndex].position.y + (randX * spawnRadius)));
			Instantiate (StraightEdge, whereToSpawn, spawnPoints [spawnPointIndex].rotation);

		} else {
			spawnPointIndex = UnityEngine.Random.Range (0, 3);
		}

		enemyCounter++; 

	}
}
