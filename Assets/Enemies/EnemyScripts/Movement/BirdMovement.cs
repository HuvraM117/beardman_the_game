//Caitlin
/*
PATTERN: Bird moves in sin wave pattern with chosen amplitude, horizontal and vertical speed 
		Also, has a fixed range of movement. 
TODO: Take platforms into account. May not be necessary if horizontal movement is fixed?
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour {

	private Vector2 tempPosition; // used to adjust position
	private float xMoved; //counter for how far it's moved horizontally 
	private float yModifier; //used so bird doesn't do crazy things
	private float myXscale;
	private float myYscale;

	public float maxHorizontalRange; //range of horizontal movement
	public float xVelocity; //horizontal velocity (positive or negative)
	public float ySpeed; //vertical speed
	public float amplitude; //amount of vertical movement 

	// Use this for initialization
	void Start () {
		tempPosition = transform.position;
		yModifier = 20f;

		myXscale = transform.localScale.x;
		myYscale = transform.localScale.y;
		if (xVelocity > 0) { //flips direction if starts moving right
			transform.localScale = new Vector2 (-myXscale, myYscale);
		}
	}

	// Update is called once per frame
	void Update () {
		tempPosition.x += xVelocity * Time.deltaTime;
		tempPosition.y = Mathf.Cos(Time.realtimeSinceStartup * ySpeed * amplitude * yModifier * Time.fixedDeltaTime);
		transform.position = tempPosition;
		xMoved += Mathf.Abs(xVelocity * Time.deltaTime);

		if (xMoved >= maxHorizontalRange) { //if bird passes some fixed point
			xVelocity = -xVelocity; //switch direction
			if (xVelocity > 0) { // moving right
				transform.localScale = new Vector2 (-myXscale, myYscale);
			} else { //moving left
				transform.localScale = new Vector2 (myXscale, myYscale);
			}
			xMoved = 0;
		}
	}

	/* 
		Pseudocode for collision: 
			- use oncollision or something
			- have a boolean or 1/-1 value that affects Sin/Y movement 
			- Flip that boolean if hit something, and flip x direction 
			Will need to take XMoved into account -- maybe reset if turn around?
	*/
}
