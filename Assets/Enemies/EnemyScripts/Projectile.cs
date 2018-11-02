using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int moveRight { get; set; } //1 for moving right -1 for moving left
    public int speed { get; set; }

    private Rigidbody2D rigidbody;
    private int timeToSelfDestruct = 4;
    Vector2 velocity;

    public Projectile(int moveRight)
    {
        this.moveRight = moveRight;
    }

	// Use this for initialization
	void Start ()
	{
	    velocity = Vector2.right * speed * moveRight; //new Vector2(speed, 0);

        rigidbody = GetComponent<Rigidbody2D>();
	    rigidbody.velocity = velocity;

        //DestroyObjectDelayed();
	}

    //TODO: Not working as expected..?
    void DestroyObjectDelayed()
    {
        Destroy(gameObject, (float)timeToSelfDestruct);
    }
}
