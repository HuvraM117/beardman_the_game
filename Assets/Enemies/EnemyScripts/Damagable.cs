using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {

	public int max_health = 2; //one for squirrel + bird 
	public int currentHealth = 0;
	public bool alive = true;

	int count = 0; 
	FloodBehavior enemyCounter; 

    //private Animator animator;

	// Use this for initialization
	void Start () {
		
		alive = true; 
		currentHealth = max_health;
	    //animator = GetComponent<Animator>();
	}

	public void TakeDamage(int amount){
		if (!alive){
			return; 
		}

		if (currentHealth <= 0){
			currentHealth = 0; 

			//enemyCounter = gameObject.GetComponent<FloodBehavior> ();
			//enemyCounter.Respawn ();

		    StartCoroutine(PlayDeathAnimation());

			Debug.Log ("I am Dead");
			/* despawn need to add in some kind of animation with it */
		}
		currentHealth -= amount; 
		Debug.Log ("I got hit!");
	}

    IEnumerator PlayDeathAnimation()
    {
        // If this has an animator (since enemies should will be damagable)
       // if (animator != null)
        //{
         //   animator.SetBool("isDead", true);
        //}

        // Figure out a more elegant way to disable movement
        //var movementController = GetComponent<AnimalMovement>();
        //movementController.speed = 0;
        // Let the animation play 
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {

	}
		
}
