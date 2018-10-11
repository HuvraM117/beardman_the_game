//Caitlin
/*
PATTERN: Bird moves in sin wave pattern with chosen amplitude, horizontal and vertical speed 
TODO: Take platforms into account
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour {

	private Vector2 tempPosition; // used to adjust position
	public float xSpeed; //horizontal speed
	public float ySpeed; //vertical speed
	public float amplitude; //amount of vertical movement 

	// Use this for initialization
	void Start () {
		tempPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		tempPosition.x += xSpeed;
		tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * ySpeed * amplitude);
		transform.position = tempPosition;
	}
}
