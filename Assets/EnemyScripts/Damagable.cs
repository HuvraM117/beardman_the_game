using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {

	public int max_health = 2; //one for squirrel + bird 
	public int currentHealth = 0;
	public bool alive = true; 

	// Use this for initialization
	void Start () {
		alive = true; 
		currentHealth = max_health;
	}

	public void TakeDamage(int amount){
		if (!alive){
			return; 
		}

		if (currentHealth <= 0){
			currentHealth = 0; 
			Destroy(gameObject);
			Debug.Log ("I am Dead");
			/* despawn need to add in some kind of animation with it */
		}
		currentHealth -= amount; 
		Debug.Log ("I got hit!");
	}

	// Update is called once per frame
	void Update () {

	}
		
}
