using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//movement for the shaved animals
//these move left to right

public class AnimalMovement : MonoBehaviour {

	//TODO: make these editable in screen/not hardcoded
	private int movingRight; // 1 if animal is moving right, -1 if moving left
	private int speed; //determines speed of animal 
	private double speedModifier = 1; //used to scale speed of all animals
	private double jumpProbability; // determines % of time enemy will jump if at end of platform
									// e.g. jumpProbability = 0 means it will never jump
									// 		jumpProbability = .5 means it will jump half the time

	// Use this for initialization
	void Start () {
		//assign all variables
		//make sure it's on a platform
	}
	
	// Update is called once per frame
	void Update () {
		//TODO: determine way to check for playform edges 
		//if at the edge of a platform
			//if jumps 
				//start jump subroutine
				//TODO: code jumps
			//else 
			movingRight = -movingRight;
		//else
		//transform.Translate (Vector2.right * speed * Time.deltaTime * speedModifier); //moves animal
	}
}
