using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * An animal enemy
 * Moves in a single direction from right to left
 * If on a platform, tries to not fall off
 */
public class Squirrel : BaseEnemy
{

    private Rigidbody2D m_rigidbody;

	// Use this for initialization
	void Start ()
	{
	    m_rigidbody = GetComponent<Rigidbody2D>();
	    MoveSpeed = 2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        MoveLeft();
    }
    void MoveLeft()
    {
        // The vector to move left
        var leftVector = new Vector2(-5, 0) * MoveSpeed;

        m_rigidbody.AddForce(leftVector);
    }

}
