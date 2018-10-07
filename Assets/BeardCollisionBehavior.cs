using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeardCollisionBehavior : MonoBehaviour {

    private static LinkedList<GameObject> collisions = new LinkedList<GameObject>();

    // register any newly currently colliding objects with collisions and deal damage if it's an enemy
    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject collisionObject = collider.gameObject;

        // register any new collisions
        if(!collisions.Contains(collisionObject))
        {
            collisions.AddFirst(collisionObject);

            Enemy enemy = collisionObject.GetComponent<Enemy>();
            // if the newly collided object is an enemy
            if (enemy)
            {
                enemy.TakeDamage();
            }
        }
    }

    // unregister any currently colliding objects that have exited the collider
    private void OnTriggerExit2D(Collider2D collider)
    {
        GameObject collisionObject = collider.gameObject;
        collisions.Remove(collisionObject);
    }

}
