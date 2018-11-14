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
	//[SerializeField] private float floodTime = 90000.0f; 			// How long the flood lasts 
	[SerializeField] private float spawnTime = 0.2f;            // How long between each spawn.
	[SerializeField] private float deathCounter = 0.2f; 
	[SerializeField] private Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

	private int enemyCounter; 
	Damagable Count; 
	private int maxEnemyCounter = 30;

	private float spawnRadius = 5.0f; 
	Vector2 whereToSpawn; 

	private bool toRespawn; //Stop & Start Checkers
	private bool toSpawn; 

	List<GameObject> enemies = new List<GameObject>();

	// Use this for initialization
	void Start () {

		toRespawn = false; 
		toSpawn = true; 

		playerHealth = player.GetComponent <PlayerState> (); 
		Count = player.GetComponent <Damagable> (); 

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
		var minutes = timer / 60;
		if (minutes == 1) {
			timer = 0; 
			Debug.Log ("toSpawn is False"); 
			toSpawn = false;
		}

		//if (toRespawn){
		//	Respawn(); 
		//}
	}

	void OnTriggerEnter2D (Collider2D other)
	{

		if (other.gameObject == player) {
			Debug.Log ("START FLOOD");
			InvokeRepeating ("Spawn", spawnTime, deathCounter);
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{

		if (other.gameObject == player) {
			Debug.Log("STOP FLOOD");
			toRespawn = false; 
			toSpawn = false;
		}
	}

	void Spawn ()
	{
		Debug.Log("Spawning");
		// If the player has no health left, if the max enemies have generated, or we have left the trigger...
		if (playerHealth.Health < 0 || enemyCounter == maxEnemyCounter || toSpawn == false)
		{
			toRespawn = true; 
			// ...exit the function.
			return;

		}

		// Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = UnityEngine.Random.Range (0, spawnPoints.Length);
		Debug.Log ("spawnPoint is" + spawnPointIndex);


		if (spawnPointIndex == 0) {
			float randX  = UnityEngine.Random.Range (-2f, 2f);
			whereToSpawn = new Vector2 ((spawnPoints [spawnPointIndex].position.x + (randX * spawnRadius)),(spawnPoints [spawnPointIndex].position.y + (randX * spawnRadius)));
			Instantiate (Squirrel, whereToSpawn, spawnPoints[spawnPointIndex].rotation);
			Debug.Log ("Squirrel");
		}  
		else if (spawnPointIndex == 1) {
			float randX  = UnityEngine.Random.Range (-2f, 2f);
			whereToSpawn = new Vector2 ((spawnPoints [spawnPointIndex].position.x + (randX * spawnRadius)),(spawnPoints [spawnPointIndex].position.y + (randX * spawnRadius)));
			Instantiate (Bird, whereToSpawn, spawnPoints[spawnPointIndex].rotation);
			Debug.Log ("Bird");
		} 
		else {
			float randX  = UnityEngine.Random.Range (-2f, 2f);
			whereToSpawn = new Vector2 ((spawnPoints [spawnPointIndex].position.x + (randX * spawnRadius)),(spawnPoints [spawnPointIndex].position.y + (randX * spawnRadius)));
			Instantiate (StraightEdge, whereToSpawn, spawnPoints[spawnPointIndex].rotation);
			Debug.Log ("StraightEdge");
		}

		enemyCounter++; 
		Debug.Log (enemyCounter); 

	}

	//public void Respawn()
	//{
	//	Debug.Log("Respawning");
	//	if (timer >= deathCounter) {
	//			timer = 0;
				//call spawn to generate a new enemy
	//			Spawn (); 

	//	}
	//}
}
