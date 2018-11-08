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
	[SerializeField] private float spawnTime = 2f;            // How long between each spawn.
	[SerializeField] private float deathCounter = 4f;       // How long before respawn
	[SerializeField] private Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

	private int enemyCounter; 
	private int maxEnemyCounter = 10;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
		if (enemyCounter < maxEnemyCounter){
			Respawn(); 
		}

	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject == player) {
			InvokeRepeating ("Spawn", spawnTime, spawnTime);
			enemyCounter++; 

			//note: edit Damaged to minus enemyCounter value
		}
	}

	void Spawn ()
	{
		// If the player has no health left...
		if(playerHealth.Health < 0)
		{
			// ... exit the function.
			return;
		}

		// Find a random index between zero and one less than the number of spawn points.

		int spawnPointIndex = UnityEngine.Random.Range (0, spawnPoints.Length);

		// Create an instance of one of our randomly choosen enemy prefabs 

		List<GameObject> enemies = new List<GameObject>();
		enemies.Add(Squirrel);
		enemies.Add(Bird);
		//enemies.Add(Bear);
		enemies.Add(StraightEdge);
		//enemies.Add(ElectricRazor);
		//enemies.Add(HairSpray);

		int random = UnityEngine.Random.Range(0, 2);
		GameObject choosen = enemies[random];

		Instantiate (choosen, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}

	void Respawn()
	{

		if (timer >= deathCounter) {
			timer = 0;
			Spawn(); 
		}

	}
}
