using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public bool moveRight;
    public int speed;

    private Rigidbody2D rigidbody;
    private int timeToSelfDestruct = 4;

    public Projectile(bool moveRight)
    {
        this.moveRight = moveRight;
    }

	// Use this for initialization
	void Start ()
	{
	    Vector2 velocity;
	    if (moveRight)
	    {
	        velocity = new Vector2(speed, 0);
        }
	    else
	    {
            velocity = new Vector2(-speed, 0);
	    }

        rigidbody = GetComponent<Rigidbody2D>();
	    rigidbody.velocity = velocity;

        Destroy(gameObject, timeToSelfDestruct);
    }
	

    // Collision w/ player
    // TODO: Figure whether this can just be handled with the other script
}
