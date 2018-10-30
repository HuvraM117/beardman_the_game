using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public bool moveRight { get; set; }
    public int speed { get; set; }

    private Rigidbody2D rigidbody;
    private int timeToSelfDestruct = 4;
    Vector2 velocity;

    public Projectile(bool moveRight)
    {
        this.moveRight = moveRight;
    }

	// Use this for initialization
	void Start ()
	{
	    if (moveRight)
	    {
	        velocity = Vector2.right * speed; //new Vector2(speed, 0);
	    }
	    else
	    {
	        velocity = Vector2.left * speed;
	    }

        rigidbody = GetComponent<Rigidbody2D>();
	    rigidbody.velocity = velocity;

        //DestroyObjectDelayed();
	}

    //TODO: Not working as expected..?
    void DestroyObjectDelayed()
    {
        Destroy(gameObject, (float)timeToSelfDestruct);
    }


    // Collision w/ player
    // TODO: Figure whether this can just be handled with the other script

    void FixedUpdate()
    {

    }
}
