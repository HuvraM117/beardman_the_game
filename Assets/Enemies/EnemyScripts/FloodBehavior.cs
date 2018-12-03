using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodBehavior : MonoBehaviour {
	PlayerState playerHealth;        						  // Reference to the player's heatlh.
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject Squirrel;             // The enemy prefab to be spawned.
	[SerializeField] private GameObject Bird;                 // The enemy prefab to be spawned.
	[SerializeField] private GameObject StraightEdge;         // The enemy prefab to be spawned.
	[SerializeField] private GameObject ElectricRazor;        // The enemy prefab to be spawned.
	[SerializeField] private float spawnRadius = 5.0f;
	[SerializeField] private int maxEnemyCounter = 50;
	[SerializeField] private GameObject[] pillars;
	float timer;

	[SerializeField] private float spawnTime;          // How long between each spawn.
	[SerializeField] private float deathCounter;
	[SerializeField] private Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.

	[SerializeField] private GameObject cameraShakeController; // controlls camera shake

	private int enemyCounter;
	private int spawnPointIndex;

	private Transform playerPos;
	private float playerposition;
	private float spawn0, spawn1, spawn2, spawn3;
	private bool spawn0active, spawn1active, spawn2active, spawn3active;

	private bool currentlySpawning;


	Vector2 whereToSpawn;

	private bool toRespawn; //Stop & Start Checkers
	private bool toSpawn;

    private AudioSource backgroundMusic;
    private AudioSource sfxSource;

    private AudioClip initalSound;
    private AudioClip rumbleSound;
    private AudioClip battleSound;

	// Use this for initialization
	void Start () {

		currentlySpawning = false;
		toSpawn = true;

		spawn0 = spawnPoints [0].transform.position.x;
		spawn1 = spawnPoints [1].transform.position.x;
		spawn2 = spawnPoints [2].transform.position.x;
		spawn3 = spawnPoints [3].transform.position.x;

		playerPos = GameObject.FindGameObjectWithTag ("Player").transform;

		spawn0active = false;
		spawn1active = false;
		spawn2active = false;
		spawn3active = false;

		playerHealth = player.GetComponent <PlayerState> ();

		//removes pillars
		for(int i = 0; i < pillars.Length; i++) {
			pillars[i].active = false;
		}


        //Audio Things

        var beardman = GameObject.Find("Beard Man/BackgroundMusicSource");

        var b2 = GameObject.Find("Beard Man/MusicMaker");

        backgroundMusic = beardman.GetComponents<AudioSource>()[0];

        sfxSource = b2.GetComponents<AudioSource>()[0];

        initalSound = backgroundMusic.clip;

        AudioClip[] sounds = Resources.LoadAll<AudioClip>("Sound/FloodSounds");

        battleSound = sounds[0];

        rumbleSound = sounds[1];

    }


	// Update is called once per frame
	void FixedUpdate () {

		playerposition = playerPos.transform.position.x;

		if (playerposition - spawn0 > -10) {
			spawn0active = true;
			Debug.Log ("active 0");
		} else {
			spawn0active = false;
		}
		if (playerposition - spawn1 > -10){
			spawn1active = true;
			Debug.Log ("active 1");
		} else {
			spawn1active = false;
		}
		if (playerposition - spawn2 > -10){
			spawn2active = true;
			Debug.Log ("active 2");
		} else {
			spawn2active = false;
		}
		if (playerposition - spawn3 > -10){
			spawn3active = true;
			Debug.Log ("active 3");
		} else {
			spawn3active = false;
		}



		timer += Time.fixedDeltaTime;
		int minutes = (int) timer / 60;
		if (minutes == 1) { //check if done spawning
			//FLOOD OVER
			timer = 180;
			toSpawn = false;

			//Camera Shake
			CameraShake cameraShakeScript = cameraShakeController.GetComponent<CameraShake>();
			cameraShakeScript.StartCoroutine(cameraShakeScript.ShakeCamera());
            
            //stop fast music
            backgroundMusic.clip = initalSound;
            backgroundMusic.Play();

            //play rumble
            sfxSource.PlayOneShot(rumbleSound);

            //removes pillars
            for (int i = 0; i < pillars.Length; i++) {
				pillars[i].active = false;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject == player) {
			timer = 0.0f;
			if (!currentlySpawning) {
				//camera shake
				CameraShake cameraShakeScript = cameraShakeController.GetComponent<CameraShake>();
				cameraShakeScript.StartCoroutine(cameraShakeScript.ShakeCamera());
			}
            //play rumble
            backgroundMusic.PlayOneShot(rumbleSound);

            currentlySpawning = true;
			InvokeRepeating ("Spawn", spawnTime, deathCounter);

            //start fast music
            backgroundMusic.clip = battleSound;
            backgroundMusic.Play();

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

		spawnPointIndex = UnityEngine.Random.Range (0, 4);
		Debug.Log ("spawnPointIndex " + spawnPointIndex); 

		if (spawnPointIndex == 0 && spawn0active) {
			float randX = UnityEngine.Random.Range (-2f, 2f);
			whereToSpawn = new Vector2 ((spawnPoints [spawnPointIndex].position.x + (randX * spawnRadius)),
				(spawnPoints [spawnPointIndex].position.y + (randX * spawnRadius)));
			Instantiate (Squirrel, whereToSpawn, spawnPoints [spawnPointIndex].rotation);

		} else if (spawnPointIndex == 1 && spawn1active) {
			float randX = UnityEngine.Random.Range (-2f, 2f);
			whereToSpawn = new Vector2 ((spawnPoints [spawnPointIndex].position.x + (randX * spawnRadius)),
				(spawnPoints [spawnPointIndex].position.y + (randX * spawnRadius)));
			Instantiate (Bird, whereToSpawn, spawnPoints [spawnPointIndex].rotation);

		} else if (spawnPointIndex == 3 && spawn3active) {
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
			spawnPointIndex = UnityEngine.Random.Range (0, 4);
		}

		enemyCounter++;

	}
}
