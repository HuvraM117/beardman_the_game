using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {

    public int Health { get; set; }
    public float Damage { get; set; }
    public float MoveSpeed { get; set; }

    // The player's Game Object we wish to track
    public GameObject playerObject;

    public BaseEnemy()
    {
        // Empty Constructor
    }

    public BaseEnemy(int health, float damage, float moveSpeed)
    {
        Health = health;
        Damage = damage;
        MoveSpeed = moveSpeed;
    }

    // These methods are virtual and can be overriden in child classes
    public virtual void Attack()
    {

    }

    public virtual void MoveLeft()
    {

    }

    public virtual void MoveRight()
    {

    }

}
