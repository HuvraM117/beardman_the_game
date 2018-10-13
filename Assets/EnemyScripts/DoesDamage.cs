using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoesDamage : MonoBehaviour {

	public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
	public int damage = 1;               // The amount of health taken away per attack.

	GameObject player;                          // Reference to the player GameObject.
	PlayerState playerState;                  // Reference to the player's state.
	Damagable enemyhealth;                    // Reference to this enemy's health.


	float timer; 

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerState = player.GetComponent <PlayerState> ();
		enemyhealth = player.GetComponent <Damagable> ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		timer += Time.deltaTime;

		if(timer >= timeBetweenAttacks && enemyhealth.currentHealth > 0){
			if (other.gameObject == player) {
				Attack ();
			}
		}
	}

	void Attack ()
	{
		// Reset the timer.
		timer = 0f;

		if(playerState.Health > 0)
		{
			playerState.TakeDamage(damage);
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		
	}

	void Update(){
		
	}


}
