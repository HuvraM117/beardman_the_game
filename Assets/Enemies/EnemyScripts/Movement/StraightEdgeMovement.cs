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
	public GameObject projectile;

	private int movingRight;
	private bool following;
	private bool attacking;
	private Rigidbody2D m_rigidbody;
	private float timeSinceLastAttack; 

	// Use this for initialization
	void Start () {
		movingRight = 1;
		following = false;
		attacking = false;
		timeSinceLastAttack = 0.0f;
		m_rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!attacking) { //if not attacking, move
			//check if should be attacking
			if (Mathf.Abs (player.transform.position.x - this.transform.position.x) <= attackRange) {
				//then attack! 
				StartCoroutine(attack());
			} else { //move
				//check if should be following player
				if (!following && Mathf.Abs (player.transform.position.x - this.transform.position.x) <= followRange) {

					following = true;
					//adjusts direction of movement
					if (player.transform.position.x - this.transform.position.x > 0) {
						movingRight = 1;
					} else {
						movingRight = -1;
					}

					//check if should NOT be following player 
				} else if (following && Mathf.Abs (player.transform.position.x - this.transform.position.x) > followRange) {
					following = false;
				}

				//moves 
				Vector2 vector = new Vector2(1f * movingRight, 0) * speed;
				m_rigidbody.AddForce(vector);
			}
		}
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

		var projectileGameObject = Instantiate(projectile, transform.position, transform.rotation);

		attacking = false;
		return null;
	}
}
