//Caitlin 
/*QUESTION: are barber tools also stuck on platforms? Or can/do they jump sometimes?*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarberToolMovement : MonoBehaviour {

	public int speed;
	public int followRange; //NOTE: Only is concerned with X distance
	private int movingRight;
	private bool following;
	public GameObject player;

	private Rigidbody2D m_rigidbody;

	// Use this for initialization
	void Start () {
		movingRight = 1;
		following = false;
		m_rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
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

	//Reaches end of platform
	public void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log ("Collider happened!");

		if (collision.gameObject.CompareTag ("EnemyCollisionOnly")) {
			movingRight = -movingRight;
			Debug.Log ("Switched direction!");
		}
	}
}
