//based on Animal Movement 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//movement for the bear
//TODO: Test jumping 

public class BearMovement : MonoBehaviour {

	private int movingRight; // 1 if animal is moving right, -1 if moving left
	public int speed; //determines speed of animal 
	private bool jumping; // boolean that is true if character is jumping, false otherwise
	private float sinceLastFlipped;
	private Rigidbody2D m_rigidbody;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		m_rigidbody = GetComponent<Rigidbody2D>();
		movingRight = 1;
		jumping = false; // set it to false to begin with 
		sinceLastFlipped = 0.0f;
		spriteRenderer = GetComponent<SpriteRenderer>();
		//StartCoroutine(jumpUp());
	}

	// Update is called once per frame
	void Update () {
		//Moves animal
		if (!jumping) {
			Vector2 vector = new Vector2 (speed * movingRight, 0f);
			m_rigidbody.AddForce (vector);
		}
	}

	public void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log ("Collider happened!");


		int random = UnityEngine.Random.Range(0,100); 
		if (random < 25) {
			jumping = true;
		}

		Debug.Log (jumping); 

		if (collision.gameObject.CompareTag ("EnemyCollisionOnly") && !jumping) {
			movingRight = -movingRight;
			spriteRenderer.flipX = !spriteRenderer.flipX;
			Debug.Log ("Switched direction!");
		}

		if (collision.gameObject.CompareTag ("EnemyCollisionOnly") && jumping) {
			StartCoroutine(jump());
			Debug.Log ("Jump!");
		}
	}

	//Jumping over a distance once jumping is set to true. sets to false afterwards.
	private IEnumerator jump() {
		float amountToMove = speed * Time.deltaTime;
		float amountUp = UnityEngine.Random.Range (200f, 500f);
		Vector2 movement = new Vector2(movingRight * amountToMove, amountUp);
		m_rigidbody.AddForce(movement);
		jumping = false;
		movingRight = -movingRight;
		spriteRenderer.flipX = !spriteRenderer.flipX;
		Debug.Log ("Switched direction!");
		return null;
	} 
	//behavior so far: It gets around the collider but I don't see it jump up? 

	// if we want to switch to jumping up and down instead of from platform to platform 
	private IEnumerator jumpUp() {
		jumping = true; 
		yield return new WaitForSeconds (UnityEngine.Random.Range (2, 4)); 
		float amountUp = UnityEngine.Random.Range (500f, 800f); //the upwards momentum. might be a little high.
		m_rigidbody.AddForce(new Vector2(0f , amountUp));
		yield return new WaitForSeconds (1.5f); // wait for a few seconds after jump 
		StartCoroutine(jumpUp());
	}

}
