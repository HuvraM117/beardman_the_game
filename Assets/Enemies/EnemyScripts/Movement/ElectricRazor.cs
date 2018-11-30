//Caitlin
//Controls movement for electricRazor
//Also controlls attack movement/attack pattern
//This is the flying enemy

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
	private float currentFloated; //amount of idleFloatRange that has floated
	private bool attacking;

	public int attackRange = 5;
	public int followRange = 10;
	public int speed = 2;
	public int idleFloatRange = 15; //how far it will float before turning around
	public GameObject player;

    private Animator animator;

	// Use this for initialization
	void Start () {
		movingRight = 1;
		attacking = false;
	    animator = gameObject.GetComponent<Animator>();
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
				transform.Translate(direction * speed * .5f * Time.fixedDeltaTime);
				currentFloated = 0; // resets floating distance 
			} else {
				transform.Translate(direction * speed * Time.fixedDeltaTime);
				currentFloated = currentFloated + Mathf.Sqrt(Vector2.SqrMagnitude(direction * speed * Time.fixedDeltaTime));

				if (currentFloated >= idleFloatRange) {
					currentFloated = 0;
					movingRight = -movingRight;
				}
			}
		}
	}
	private IEnumerator AttackPlayer() {
		attacking = true;
        animator.SetBool("attacking", true);

		float xDistance = player.transform.position.x - this.transform.position.x;
		float yDistance = player.transform.position.y - this.transform.position.y;

		Vector2 tempPosition = transform.position;

		float xMove = 0;
		float yMove = 0; 

		if(xDistance > 0) { //moving right
			xMove = Mathf.Abs(xDistance / yDistance) * 2.0f * speed * Time.fixedDeltaTime;
		} else { // moving left
			xMove = - Mathf.Abs(xDistance / yDistance) * 2.0f * speed * Time.fixedDeltaTime;
		}

		if(yDistance > 0) { //moving right
			yMove = speed * 2.0f * Time.fixedDeltaTime;
		} else { // moving left
			yMove = - speed * 2.0f * Time.fixedDeltaTime;
		}

		float xDistanceMoved = 0.0f;
		Debug.Log ("Goal: " + xDistance);
		//B line down
		while (xDistanceMoved <= xDistance) { 	//NOTE: only takes x distance into account
												//for easier/faster calculation
			tempPosition.x += xMove;
			tempPosition.y += yMove;
			transform.position = tempPosition;
			xDistanceMoved += xMove;

			yield return new WaitForFixedUpdate ();
		}

		//swoop back up

		int amplitude = 2;
		float timeCount = 0;
		float yUp = 0;

		while(yUp < amplitude) {
			if (xDistance > 0) { //moving right
				tempPosition.x += Mathf.Cos (amplitude * timeCount);
			} else { //moving left
				tempPosition.x -= Mathf.Cos (amplitude * timeCount);
			}

			tempPosition.y += Mathf.Sin (amplitude * timeCount);
			transform.position = tempPosition;
			yUp += Mathf.Sin (amplitude * timeCount);
			timeCount += Time.fixedDeltaTime;
			yield return new WaitForFixedUpdate ();
		}

		//hover for a second
		timeCount = 0;
		int upOrDown = 0;
		while (timeCount < 1) {
			upOrDown = Mathf.RoundToInt((timeCount * 8)) % 2;
			if (upOrDown == 0) {
				//move up
				tempPosition.y += Time.fixedDeltaTime * speed;
			} else {
				//move down
				tempPosition.y -= Time.fixedDeltaTime * speed;
			}

			transform.position = tempPosition;

			timeCount += Time.fixedDeltaTime;
			yield return new WaitForFixedUpdate ();
		}

		attacking = false;
        animator.SetBool("attacking", false);
	}

    public void Die()
    {

    }
}
