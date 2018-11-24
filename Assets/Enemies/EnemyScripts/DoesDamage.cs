using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoesDamage : MonoBehaviour {

	[SerializeField] private float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
	[SerializeField] private int damage = 1;               // The amount of health taken away per attack.

	public GameObject player;                   // Reference to the player GameObject.
	PlayerState playerHealth;                  // Reference to the player's health.
	Damagable enemyhealth;                    // Reference to this enemy's health.


	float timer; 

	// Use this for initialization
	void Start () {
		playerHealth = player.GetComponent <PlayerState> ();
		enemyhealth = player.GetComponent <Damagable> ();
		timer = timeBetweenAttacks; // makes so can immediatly attack without waiting cooldown period
	}

	void FixedUpdate(){
		timer += Time.deltaTime;
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (timer >= timeBetweenAttacks) {
			timer = 0;
			if (other.gameObject == player) {
				StartCoroutine(Attack());
			}
		}
	}

	private IEnumerator Attack()
	{
		if(playerHealth.Health > 0)
		{
			playerHealth.TakeDamage(damage);
		}
		yield return null;
	}

	void OnCollisionExit2D(Collision2D collider)
	{
		
	}




}
