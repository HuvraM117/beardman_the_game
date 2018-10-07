using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {

	public int max_health = 1; //one for squirrel + bird 
	public int current_health = 0;
	public bool alive = true; 

	// Use this for initialization
	void Start () {
		alive = true; 
		current_health = max_health;
	}

	public void TakeDamage(int amount){
		if (!alive){
			return; 
		}

		if (current_health <= 0){
			current_health = 0; 
			alive = false; 
			gameObject.SetActive(false); 
			/* despawn need to add in some kind of animation with it */
		}
		current_health -= amount; 
	}

	// Update is called once per frame
	void Update () {

	}

	/* put something like this on the beard to take damage 
	void OnTriggerEnter(Collider other){
	other.gameObject.GetComponent(<Damagable>) ().TakeDamage(damage);
	} 

	Damage = value of attack power */ 
}
