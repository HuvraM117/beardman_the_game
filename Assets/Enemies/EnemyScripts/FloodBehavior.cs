using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodBehavior : MonoBehaviour {
	[SerializeField] private GameObject player; 
	PlayerState playerHealth;        						  // Reference to the player's heatlh.
	[SerializeField] private GameObject Squirrel;             // The enemy prefab to be spawned.
	[SerializeField] private GameObject Bird;                 // The enemy prefab to be spawned.
	[SerializeField] private GameObject StraightEdge;         // The enemy prefab to be spawned.
	[SerializeField] private GameObject ElectricRazor;        // The enemy prefab to be spawned.
	[SerializeField] private GameObject[] pillars;
	float timer; 
	[SerializeField] private float spawnTime;          // How long between each spawn.
	[SerializeField] private float deathCounter; 
	[SerializeField] private Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	private int enemyCounter; 
	Damagable Count; 
	private int maxEnemyCounter = 30;

	private float p_distance; 

	private float spawnRadius = 5.0f; 
	Vector2 whereToSpawn; 

	private bool toRespawn; //Stop & Start Checkers
	private bool toSpawn; 

	// Use this for initialization
	void Start () {

		toSpawn = true; 

		playerHealth = player.GetComponent <PlayerState> (); 
		Count = player.GetComponent <Damagable> (); 

		//removes pillars
		for(int i = 0; i < pillars.Length; i++) {
			pillars[i].active = false;
		}

	}
		

	// Update is called once per frame
	void FixedUpdate () {

		p_distance = player.transform.position.x;

		timer += Time.deltaTime;
		var minutes = timer / 60;
		if (minutes == 1) {
			timer = 0; 
			Debug.Log ("toSpawn is False"); 
			toSpawn = false;
			//removes pillars
			for(int i = 0; i < pillars.Length; i++) {
				pillars[i].active = false;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject == player) {
			Debug.Log ("START FLOOD");
			InvokeRepeating ("Spawn", spawnTime, deathCounter);

			//Adds pillars
			for (int i = 0; i < pillars.Length; i++) {
				pillars [i].active = true;
			}
		}
	}

	void Spawn ()
	{
		Debug.Log("Spawning");
		// If the player has no health left, if the max enemies have generated, or we have left the trigger...
		if (playerHealth.Health < 0 || enemyCounter == maxEnemyCounter || toSpawn == false)
		{
			return;

		}

		// Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = UnityEngine.Random.Range (0, spawnPoints.Length);
		Debug.Log ("spawnPoint is" + spawnPointIndex);


		if (spawnPointIndex == 0) {
			float randX  = UnityEngine.Random.Range (-2f, 2f);
			whereToSpawn = new Vector2 ((spawnPoints [spawnPointIndex].position.x + (randX * spawnRadius)),
				(spawnPoints [spawnPointIndex].position.y + (randX * spawnRadius)));
			Instantiate (Squirrel, whereToSpawn, spawnPoints[spawnPointIndex].rotation);
			Debug.Log ("Squirrel");
		}  
		else if (spawnPointIndex == 1) {
			float randX  = UnityEngine.Random.Range (-2f, 2f);
			whereToSpawn = new Vector2 ((spawnPoints [spawnPointIndex].position.x + (randX * spawnRadius)),
				(spawnPoints [spawnPointIndex].position.y + (randX * spawnRadius)));
			Instantiate (Bird, whereToSpawn, spawnPoints[spawnPointIndex].rotation);
			Debug.Log ("Bird");
		} 
		else if (spawnPointIndex == 3) {
			float randX  = UnityEngine.Random.Range (-2f, 2f);
			whereToSpawn = new Vector2 ((spawnPoints [spawnPointIndex].position.x + (randX * spawnRadius)),
				(spawnPoints [spawnPointIndex].position.y + (randX * spawnRadius)));
			Instantiate (ElectricRazor, whereToSpawn, spawnPoints[spawnPointIndex].rotation);
			Debug.Log ("Electric Razor");
		} 
		else {
			float randX  = UnityEngine.Random.Range (-2f, 2f);
			whereToSpawn = new Vector2 ((spawnPoints [spawnPointIndex].position.x + (randX * spawnRadius)),
				(spawnPoints [spawnPointIndex].position.y + (randX * spawnRadius)));
			Instantiate (StraightEdge, whereToSpawn, spawnPoints[spawnPointIndex].rotation);
			Debug.Log ("StraightEdge");
		}

		enemyCounter++; 
		Debug.Log (enemyCounter); 

	}
}
