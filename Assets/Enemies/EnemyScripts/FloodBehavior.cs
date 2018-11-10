using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodBehavior : MonoBehaviour {
	[SerializeField] private GameObject player; 
	PlayerState playerHealth;        // Reference to the player's heatlh.
	[SerializeField] private GameObject Squirrel;             // The enemy prefab to be spawned.
	[SerializeField] private GameObject Bird;                 // The enemy prefab to be spawned.
	[SerializeField] private GameObject StraightEdge;         // The enemy prefab to be spawned.
	float timer; 
	[SerializeField] private float spawnTime = .2f;            // How long between each spawn.
	[SerializeField] private float deathCounter = .4f;       // How long before respawn
	[SerializeField] private Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

	private int enemyCounter; 
	private int maxEnemyCounter = 10;

	private bool toRespawn; //Stop & Start Checkers
	private bool toSpawn; 

	List<GameObject> enemies = new List<GameObject>();

	// Use this for initialization
	void Start () {

		toRespawn = false; 
		toSpawn = true; 

		playerHealth = player.GetComponent <PlayerState> (); 

		enemies.Add(Squirrel);
		enemies.Add(Bird);
		//enemies.Add(Bear);
		enemies.Add(StraightEdge);
		//enemies.Add(ElectricRazor);
		//enemies.Add(HairSpray);

	}

	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
		if (enemyCounter < maxEnemyCounter){
			toRespawn = true; 
			Respawn(); 
		}

	}

	void OnCollisionEnter2D (Collision2D other)
	{
		Debug.Log("START FLOOD");
		if (other.gameObject == player) {
			InvokeRepeating ("Spawn", spawnTime, spawnTime);

			//note: edit Damaged to minus enemyCounter value
		}
	}

	void OnCollisionExit2D (Collision2D other)
	{
		Debug.Log("STOP FLOOD");
		if (other.gameObject == player) {
			toRespawn = false; 
			toSpawn = false;
		}
	}

	void Spawn ()
	{
		// If the player has no health left, if the max enemies have generated, or we have left the trigger...
		if(playerHealth.Health < 0 || enemyCounter == maxEnemyCounter || toSpawn == false)
		{
			// ...exit the function.
			return;
		}

		// Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = UnityEngine.Random.Range (0, spawnPoints.Length);

		// Create an instance of one of our randomly choosen enemy prefabs 
		int random = UnityEngine.Random.Range(0, 2);
		GameObject choosen = enemies[random];

		Instantiate (choosen, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		enemyCounter++; 
	}

	void Respawn()
	{
		if (toRespawn == true) {
			if (timer >= deathCounter) {
				timer = 0;
				//call spawn to generate a new enemy
				Spawn (); 
			}
		}

	}
}
