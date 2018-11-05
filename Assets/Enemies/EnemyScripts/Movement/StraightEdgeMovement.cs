//Caitlin 
/*QUESTION: are barber tools also stuck on platforms? Or can/do they jump sometimes?*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightEdgeMovement : MonoBehaviour {

	public int speed;
	public float timeBetweenAttacks;
	public int followRange; //NOTE: Only concerned with X distance
	public int attackRange; //NOTE: Only concerned with X distance
	public GameObject player;
    public Projectile projectile;

	private int movingRight;
	private bool following;
	private bool attacking;
	private Rigidbody2D m_rigidbody;
	private float timeSinceLastAttack; 
	private float myXscale;
	private float myYscale;

	// Use this for initialization
	void Start () {
		movingRight = 1;
		following = false;
		attacking = false;
		timeSinceLastAttack = 0.0f;
		m_rigidbody = GetComponent<Rigidbody2D>();
		myXscale = transform.localScale.x;
		myYscale = transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () { 
		if (!attacking) { //if not attacking, check if should be
			//check if should be attacking
			if (Mathf.Abs (player.transform.position.x - this.transform.position.x) <= attackRange) {
				//then attack! 
				Debug.Log ("Attacking the player!");
				StartCoroutine (attack ());
			} 
		}
		//check if should be following player
		if (!following && Mathf.Abs (player.transform.position.x - this.transform.position.x) <= followRange) {
			Debug.Log ("following!!");
			following = true;
			//adjusts direction of movement
			if (player.transform.position.x - this.transform.position.x > 0) {
				if (movingRight != 1) {
					movingRight = 1;//switch direction
					transform.localScale = new Vector2 (myXscale, myYscale);
				}
			} else {
				if(movingRight != -1) {
					movingRight = -1;
					transform.localScale = new Vector2 (-myXscale, myYscale);
				}
			}

			//check if should NOT be following player 
		} else if (following && Mathf.Abs (player.transform.position.x - this.transform.position.x) > followRange) {
			Debug.Log ("Not following");
			following = false;
		}

		//moves 
		Vector2 velocity = new Vector2(1f * movingRight * speed, 0.0f);
		m_rigidbody.MovePosition(m_rigidbody.position + Time.fixedDeltaTime * velocity);

	}

	//Reaches end of platform
	public void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log ("Collider happened!");

		if (collision.gameObject.CompareTag ("EnemyCollisionOnly")) {
			movingRight = -movingRight;
			Debug.Log ("Switched direction!");
		}
	}

	private IEnumerator attack() {
		attacking = true;
        
		var projectileGameObject = Instantiate (
			projectile,
			transform.position,
			transform.rotation);
	    projectileGameObject.gameObject.GetComponent<DoesDamage>().player = player;

        //Debug.Log("Creating projectile at: X:" + projectileGameObject.transform.position.x + " Y: " + projectileGameObject.transform.position.y);

		// Add velocity to the projectile. Avoids multiplying 0.
	    var velocityVect = new Vector3(movingRight * 6, 1);
		projectileGameObject.GetComponent<Rigidbody2D>().velocity = gameObject.transform.forward + velocityVect;
        
        //Debug.Log("Projectile speed: " + projectileGameObject.gameObject.GetComponent<Rigidbody2D>().velocity);

		yield return new WaitForSeconds(timeBetweenAttacks);

	    projectileGameObject.DestroyObjectDelayed();

		attacking = false;
	}
}
