//Caitlin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Will move horizontally back and forth (TODO: make not move around platforms)
*/
public class ElectricRazorMovement : MonoBehaviour {

	private int movingRight; // 1 if moving right, -1 if moving left
	private bool following;

	public int followRange;
	public int speed;

	// Use this for initialization
	void Start () {
		movingRight = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
