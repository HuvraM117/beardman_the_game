using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//movement for the shaved animals
//these move left to right

public class AnimalMovement : MonoBehaviour {

	private int movingRight; // 1 if animal is moving right, -1 if moving left
	private int speed; //determines speed of animal 
	private double jumpProbability; // determines % of time enemy will jump if at end of platform
									// e.g. jumpProbability = 0 means it will never jump
									// 		jumpProbability = .5 means it will jump half the time
	private Rigidbody2D m_rigidbody;

	// Use this for initialization
	void Start () {
		m_rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		//Moves animal
		Vector2 vector = new Vector2(1f * movingRight, 0f) * speed;
		m_rigidbody.AddForce(vector);
	}

	void reachEndOfPlatform() {
		double doIJump = Random.Range(0f, 1f);
		if (doIJump < jumpProbability) {
			StartCoroutine(jump);
		} else {
			movingRight = -movingRight;
		}
	}

	private IEnumerator jump() {
		//TODO JUMP
		return null;
	}

}
