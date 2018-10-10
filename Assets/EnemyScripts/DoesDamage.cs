using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoesDamage : MonoBehaviour {

	public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
	public int damage = 1;               // The amount of health taken away per attack.

	GameObject player;                          // Reference to the player GameObject.
	PlayerState playerHealth;                  // Reference to the player's health.
	Damagable enemyhealth;                    // Reference to this enemy's health.


	float timer; 

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerState> ();
		enemyhealth = player.GetComponent <Damagable> ();
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		timer += Time.deltaTime;

		Debug.Log("I hit the player!");

		if (other.gameObject == player) {
			Attack ();
		}
	}

	void Attack ()
	{
		// Reset the timer.
		timer = 0f;

		if(playerHealth.currentHealth > 0)
		{
			playerHealth.TakeDamage(damage);
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		
	}

	void Update(){
		
	}


}
