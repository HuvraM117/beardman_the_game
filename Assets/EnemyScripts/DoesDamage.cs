using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoesDamage : MonoBehaviour {

	public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
	public int damage = 1;               // The amount of health taken away per attack.

	public GameObject player;                          // Reference to the player GameObject.
	PlayerState playerHealth;                  // Reference to the player's health.
	Damagable enemyhealth;                    // Reference to this enemy's health.


	float timer; 

	// Use this for initialization
	void Start () {
		playerHealth = player.GetComponent <PlayerState> ();
		enemyhealth = player.GetComponent <Damagable> ();
		timer = timeBetweenAttacks; // makes so can immediatly attack without waiting cooldown period
	}

	void Update(){
		timer += Time.deltaTime;
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (timer >= timeBetweenAttacks) {
			timer = 0;
			Debug.Log("I hit the player!");

			if (other.gameObject == player) {
				StartCoroutine(Attack());
			}
		}
	}

	private IEnumerator Attack()
	{
		Debug.Log ("I attacked!!");

		if(playerHealth.currentHealth > 0)
		{
			playerHealth.TakeDamage(damage);
		}
		return null;
	}

	void OnCollisionExit2D(Collider2D collider)
	{
		
	}




}
