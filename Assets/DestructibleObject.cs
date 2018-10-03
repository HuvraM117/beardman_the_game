using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour {

    [SerializeField] int hitsNeeded;
    private int hitsTaken;

	void Start () {
        hitsTaken = 0;
	}

    void Update () {
        if (hitsTaken >= hitsNeeded)
        {
            DestroyObject(this.gameObject);
        }
	}

    void Hit()
    {
        hitsTaken++;
    }
}
