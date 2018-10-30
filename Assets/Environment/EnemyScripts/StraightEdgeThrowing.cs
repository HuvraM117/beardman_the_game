using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StraightEdgeThrowing : MonoBehaviour
{
    public bool facingRight;
    public BoxCollider collider;
    private GameObject projectile;
    
	// Use this for initialization
	void Start () {
		
        // set the projectile object

	}
	
	// Normalized update interval
	void FixedUpdate () {
		
	}

    // Check if player is within current range
    bool CheckPlayerPosition()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left);

        if (hit.collider != null)
        {
            if (hit.GetType().Equals("Beard Man"))
            {
                return true;
            }
        }

        return false;
    }

    /**
     * 'Fires' a projectile: initializes it with direction to fly
     */
    void FireProjectile()
    {
        var projectileGameObject = Instantiate(projectile, transform.position, transform.rotation);
    }

}
