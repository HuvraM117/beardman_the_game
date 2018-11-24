using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {

	public int max_health = 2; //one for squirrel + bird 
	public int currentHealth = 0;
	public bool alive = true;
    private bool isCollidingWithPlayer = false;
    public bool dropsPickUp = true;
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

        if(isCollidingWithPlayer)
            GameObject.FindWithTag("Player").BroadcastMessage("ManuallyDecrementTriggers");
        if (dropsPickUp) {
            DropHealthPickup();
        }
        Destroy(gameObject);
    }
    private void DropHealthPickup()
    {
        //create prefab programitically 
        GameObject pickUpPrefab = (GameObject)Resources.LoadAll("Player")[0];

        //sets its location to be a little higher than the enemy 
        Vector3 pos = gameObject.transform.position;

        pickUpPrefab.transform.position = new Vector3(pos.x, pos.y + 1f, pos.z);

        GameObject obj = Instantiate(pickUpPrefab);

        obj.SetActive(true);

    }

    // Update is called once per frame
    void Update () {

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isCollidingWithPlayer = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isCollidingWithPlayer = false;
    }

}
