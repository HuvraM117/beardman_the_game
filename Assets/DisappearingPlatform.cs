using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour {

    private static BoxCollider2D boxCol;
    private int timesHit;

	// Use this for initialization
	void Start () {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerState>() != null)
        {
            timesHit++;
        }
    }

    private void shakePlatform()
    {
        //shake only for a few seconds
    }

    // Update is called once per frame
    void Update () {
        if (timesHit >= 3) {
            DestroyObject(this.gameObject);
        }
	}
}
