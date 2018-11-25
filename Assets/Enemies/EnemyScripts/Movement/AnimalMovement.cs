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
    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		m_rigidbody = GetComponent<Rigidbody2D>();
		movingRight = 1;
		jumping = false;
		sinceLastFlipped = 0.0f;
	    spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Moves animal
		if(!jumping) {
			Vector2 vector = new Vector2 (speed * movingRight, 0f);
			m_rigidbody.AddForce(vector);
		}

	    if (IsGroundToFront())
	    {
	        Debug.Log("close to some ground, let's turn around");
	        movingRight = -movingRight;
	        spriteRenderer.flipX = !spriteRenderer.flipX;
        }

    }

    private bool IsGroundToFront()
    {
        // forward project a ray to check if there's ground or something
        // turn around after hitting it
        var vecDirection = Vector3.right * movingRight;
        var mask = LayerMask.GetMask("Ground", "Default");

        var colliderDirection = Physics2D.OverlapCircle(transform.position + vecDirection * 1.2f, 0.5f, mask);
        //Debug.Log("What's in front of me: " + colliderDirection);
        if (colliderDirection != null)
        {
            if(colliderDirection.gameObject.CompareTag("Untagged"))
            return true;
        }

        return false;
    }

	public void OnTriggerEnter2D(Collider2D collision) {
		//Debug.Log ("Collider happened!");

		/*if (collision.gameObject.CompareTag ("EnemyCollisionOnly") && !jumping) {
			movingRight = -movingRight;
		    spriteRenderer.flipX = !spriteRenderer.flipX;
            //Debug.Log ("Switched direction!");
		}*/

		/*
			double doIJump = Random.Range(0f, 1f);
			if (doIJump < jumpProbability) {
				StartCoroutine(jump());
			} else {
				movingRight = -movingRight;
			}
		}*/
	}

    /**
     * Enforce a direction change when an Enemy and Enemy interact, checking  for valid movement options
     */
    public void OnCollisionEnter2D(Collision2D collision)
    {
        var collidedObj = collision.gameObject;
        if (collidedObj.CompareTag("Enemy"))
        {
            Debug.Log("Hit another enemy. My direction is " + movingRight);

            // check if it is right next to an obstacle
            var vecOppositeDirection = Vector3.right * (-1.0f) * movingRight;
            var colliderOppositeDirection = Physics2D.Raycast(transform.position, vecOppositeDirection, 0.5f);
            //var enemyColliderLeftCast = Physics2D.Raycast(transform.position, Vector3.left, 1.0f);

            if (colliderOppositeDirection.collider.CompareTag("EnemyCollisionOnly"))
            {
                // keep our direction going away from it
                Debug.Log("At a wall, moving away from it");
            }
            else
            {
                movingRight = -movingRight;
                spriteRenderer.flipX = !spriteRenderer.flipX;
                m_rigidbody.velocity = Vector2.right * movingRight * 2.0f;
            }

            return;
        }
        // change direction
        //Debug.Log("Hit something");
        //movingRight = -movingRight;
        //spriteRenderer.flipX = !spriteRenderer.flipX;
        
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
