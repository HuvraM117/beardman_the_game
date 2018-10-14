//Caitlin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//movement for the shaved animals
//these move left to right
//TODO: Test jumping 

public class AnimalMovement : MonoBehaviour {

	private int movingRight; // 1 if animal is moving right, -1 if moving left
	public int speed; //determines speed of animal 
	public double jumpProbability; // determines % of time enemy will jump if at end of platform
									// e.g. jumpProbability = 0 means it will never jump
									// 		jumpProbability = .5 means it will jump half the time
	private bool jumping; // boolean that is true if character is jumping, false otherwise
	private float sinceLastFlipped;
	private Rigidbody2D m_rigidbody;

	// Use this for initialization
	void Start () {
		m_rigidbody = GetComponent<Rigidbody2D>();
		movingRight = 1;
		jumping = false;
		sinceLastFlipped = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		//Moves animal
		if(!jumping) {
			Vector2 vector = new Vector2 (speed * movingRight, 0f);
			m_rigidbody.AddForce(vector);
		}
	}

	public void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log ("Collider happened!");

		if (collision.gameObject.CompareTag ("EnemyCollisionOnly") && !jumping) {
			movingRight = -movingRight;
			Debug.Log ("Switched direction!");
		}

		/*
			double doIJump = Random.Range(0f, 1f);
			if (doIJump < jumpProbability) {
				StartCoroutine(jump());
			} else {
				movingRight = -movingRight;
			}
		}*/
	}

	private IEnumerator jump() {
		jumping = true;
		float amountToMove = speed * Time.deltaTime;
		Vector2 movement = new Vector2(movingRight * amountToMove, amountToMove);
		m_rigidbody.AddForce(movement);

		jumping = false;
		return null;
	}

}
