//Caitlin
//Controls movement for electricRazor
//Also controlls attack movement/attack pattern

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Will move horizontally back and forth, and follow player if in range
 * 
 * TODO: take platforms into account or something
*/
public class ElectricRazor : MonoBehaviour {

	private int movingRight; // 1 if moving right, -1 if moving left
	private bool following;
	private float currentFloated; //amount of idleFloatRange that has floated
	private bool attacking;

	public int attackRange;
	public int followRange;
	public int speed;
	public int idleFloatRange; //how far it will float before turning around
	public GameObject player;

	// Use this for initialization
	void Start () {
		movingRight = 1;
		following = false;
		attacking = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!attacking) {
			Vector2 direction = new Vector2 (1 * movingRight, 0.0f);
			float xDistance = player.transform.position.x - this.transform.position.x;
			float yDistance = player.transform.position.y - this.transform.position.y;
			float distanceToPlayer = Mathf.Sqrt ((Mathf.Pow(xDistance, 2.0f)) + Mathf.Pow(yDistance, 2.0f));
			if (distanceToPlayer <= attackRange) {
				StartCoroutine (AttackPlayer());
			} else if (distanceToPlayer <= followRange) {  //NOTE: this method doesn't care if it's already following
				//the player, because it needs to update it's direction 
				//every frame b/c player moves
				//follow
				direction = new Vector2(xDistance, yDistance);
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
	private IEnumerator AttackPlayer() {
		attacking = true;

		float xDistance = player.transform.position.x - this.transform.position.x;
		float yDistance = player.transform.position.y - this.transform.position.y;

		Vector2 tempPosition = transform.position;

		float xMove = xDistance * speed * Time.deltaTime;
		float yMove = yDistance * speed * Time.deltaTime;

		float xDistanceMoved = 0.0f;

		//B line down
		while (xDistanceMoved <= xDistance) { 	//NOTE: only takes x distance into account
												//for easier/faster calculation
			tempPosition.x += xMove;
			tempPosition.y += yMove;
			transform.position = tempPosition;
			xDistanceMoved += xMove;
		}

		//swoop back up
		//TODO: make go back up 

		int amplitude = 2;
		float timeCount = 0;
		float yUp = 0;

		while(yUp < amplitude) {
			tempPosition.x += Mathf.Cos (amplitude * timeCount);
			tempPosition.y += Mathf.Sin (amplitude * timeCount);
			transform.position = tempPosition;
			yUp += Mathf.Sin (amplitude * timeCount);
			timeCount += Time.fixedDeltaTime;
		}

		attacking = false;
		return null;
	} 
}
