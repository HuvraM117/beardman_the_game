using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeardCollisionBehavior : MonoBehaviour {

	PlayerState playerHealth;                  // Reference to the player's health.
	Damagable enemyhealth;                    // Reference to this enemy's health.
	int damage = 1; 
	bool hitSwitch; 


	// register any newly currently colliding objects with collisions and deal damage if it's an enemy
	private void OnTriggerEnter2D(Collider2D other)
	{
        Debug.Log(other.gameObject);
		playerHealth = other.GetComponent <PlayerState> ();
		enemyhealth = other.GetComponent <Damagable> ();

		if (other.gameObject.CompareTag("Enemy")) {
			enemyhealth.TakeDamage (damage);
			hitSwitch = false;
		}

		if (other.gameObject.CompareTag("Boss")){
			enemyhealth.TakeDamage (damage);
			hitSwitch = false;
		}
	}

	// unregister any currently colliding objects that have exited the collider
	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag("Enemy"))
		{
			hitSwitch = true;
		}

	}

}
