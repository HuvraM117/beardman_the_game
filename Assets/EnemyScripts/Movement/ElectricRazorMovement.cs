//Caitlin
//TODO: Make not bump into platforms?
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Will move horizontally back and forth (TODO: make not move around platforms)
*/
public class ElectricRazorMovement : MonoBehaviour {

	private int movingRight; // 1 if moving right, -1 if moving left
	private bool following;
	private float currentFloated; //amount of idleFloatRange that has floated

	public int followRange;
	public int speed;
	public int idleFloatRange; //how far it will float before turning around
	public GameObject player;

	// Use this for initialization
	void Start () {
		movingRight = 1;
		following = false;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 direction = new Vector2 (1 * movingRight, 0.0f);
		float distanceToPlayer = Mathf.Sqrt ((Mathf.Pow(player.transform.position.x - this.transform.position.x, 2.0f)) +
			Mathf.Pow(player.transform.position.y - this.transform.position.y, 2.0f));
		if (distanceToPlayer <= followRange) {  //NOTE: this method doesn't care if it's already following
												//the player, because it needs to update it's direction 
												//every frame b/c player moves
			//follow
			transform.Translate(direction * speed * Time.deltaTime);
			currentFloated = 0; // resets floating distance 
		} else {
			transform.Translate(direction * speed * Time.deltaTime);
			currentFloated = currentFloated + Mathf.Sqrt(Vector2.SqrMagnitude(direction * speed * Time.deltaTime));

			if (currentFloated >= idleFloatRange) {
				currentFloated = 0;
				movingRight = -movingRight;
			}
		}
	}
}
