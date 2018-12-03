//Caitlin
//Controls movement for electricRazor
//Also controlls attack movement/attack pattern
//This is the flying enemy

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Will move horizontally back and forth, and follow player if in range
 */
public class ElectricRazor : MonoBehaviour {

	private float movingRight; // 1 if moving right, -1 if moving left
	private float currentFloated; //amount of idleFloatRange that has floated
	private bool attacking;

	public int attackRange = 5;
	public int followRange = 10;
	public int attackAbove = 5;
	public int speed = 2;
	public int idleFloatRange = 15; //how far it will float before turning around
	public GameObject player;
	private Rigidbody2D m_rigidbody;
	private Vector2 initialPosition;
	private float attackSpeedModifier = 4.0f; // makes so attacks are faster

    private Animator animator;

	// Use this for initialization
	void Start () {
		movingRight = 1.0f;
		attacking = false;

		m_rigidbody = GetComponent<Rigidbody2D>();
		initialPosition = transform.position;
		currentFloated = 0.0f;

	    animator = gameObject.GetComponent<Animator>();

	}

	// Update is called once per frame
	void Update () {
		if (!attacking) {
			Vector2 tempPosition = transform.position;
			Vector2 direction = new Vector2 (1.0f * movingRight, 0.0f);

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
				//transform.Translate(direction * speed * .5f * Time.fixedDeltaTime);
				m_rigidbody.MovePosition(tempPosition + Time.fixedDeltaTime * speed * direction);
				initialPosition = transform.position;
				currentFloated = 0; // resets floating distance 
			} else {
				tempPosition.x = tempPosition.x + (speed * Time.fixedDeltaTime * movingRight);

				m_rigidbody.MovePosition(tempPosition);
				currentFloated += Mathf.Abs(speed * Time.fixedDeltaTime * movingRight);

				if (currentFloated >= idleFloatRange) {
					currentFloated = 0;
					initialPosition = transform.position;
					movingRight = -movingRight;
				}
			}
		}
	}
	private IEnumerator AttackPlayer() {
		attacking = true;
        animator.SetBool("attacking", true);

		Vector2 tempPosition = transform.position;

		float xDistance = player.transform.position.x - this.transform.position.x;
		float yDistance = player.transform.position.y - this.transform.position.y;

		float xMove = 0;
		float yMove = 0; 

		if(xDistance > 0) { //moving right
			xMove = Mathf.Abs(xDistance / yDistance) * attackSpeedModifier * speed * Time.fixedDeltaTime;
		} else { // moving left
			xMove = - Mathf.Abs(xDistance / yDistance) * attackSpeedModifier * speed * Time.fixedDeltaTime;
		}

		if (yDistance > 0) { //moving up
			//If it needs to move up, it moves to be above the player 
			yMove = speed * 2.0f * attackSpeedModifier * Time.fixedDeltaTime;
			float yTarget = player.transform.position.y + attackAbove;
			while (this.transform.position.y < yTarget) {
				tempPosition.y += yMove;
				m_rigidbody.MovePosition(tempPosition);
				yield return new WaitForFixedUpdate ();
			}
			yDistance = player.transform.position.y - this.transform.position.y;
			if (yDistance > 0) {
				Debug.Log ("ERROR -- YOU SHOULD NOT BE GETTING THIS. CHECK THE ELECTRIC RAZOR MOVEMENT");
			}
		}

		//moving down
		yMove = - speed * attackSpeedModifier * Time.fixedDeltaTime;
		

		Vector2 direction = new Vector2 (xMove, yMove);
		float startX = transform.position.x;

		float xDistanceMoved = 0.0f;
		Debug.Log ("Goal: " + xDistance);
		//B line down
		while (xDistanceMoved <= xDistance) { 	//NOTE: only takes x distance into account
			//for easier/faster calculation
			tempPosition = transform.position;
			m_rigidbody.MovePosition(tempPosition + direction);
			xDistanceMoved += Mathf.Abs(direction.x);

			yield return new WaitForFixedUpdate ();
		}

		//swoop back up

		int amplitude = 2;
		float timeCount = 0;
		float yDistanceMoved = 0;
		float yInitial = transform.position.y;
		if (xDistance < 0) {
			xMove = - 1.0f * speed * Time.fixedDeltaTime;
		}
		xMove = attackSpeedModifier * speed * Time.fixedDeltaTime;
		yMove = attackSpeedModifier * speed * Time.fixedDeltaTime;
		Vector2 diagonal = new Vector2 (xMove, yMove);

		while(yDistanceMoved < amplitude) {
			tempPosition = transform.position;
			m_rigidbody.MovePosition(tempPosition + diagonal);

			yDistanceMoved += Mathf.Abs (yMove);

			yield return new WaitForFixedUpdate ();
		}

		//hover for a second
		timeCount = 0;
		int upOrDown = 0;
		Vector2 up = new Vector2 (0.0f, 1.0f);
		Vector2 down = new Vector2 (0.0f, -1.0f);
		while (timeCount < .75) {
			upOrDown = Mathf.RoundToInt((timeCount * 8)) % 2;
			if (upOrDown == 0) {
				//move up
				m_rigidbody.AddForce(up);
			} else {
				//move down
				m_rigidbody.AddForce(down);
			}

			timeCount += Time.fixedDeltaTime;
			yield return new WaitForFixedUpdate ();
		}

		initialPosition = transform.position;

		attacking = false;
        animator.SetBool("attacking", false);
	}

    public void Die()
    {

    }
}
