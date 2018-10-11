using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour {

    private static BoxCollider2D boxCol;
    private int timesHit;
    private Sprite[] sprites;

    // Use this for initialization
    void Start () {
        timesHit = 0;
        //need to find a way to get the assets programatically
        //get neede assets 
        sprites = Resources.LoadAll<Sprite>("Environment");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerState>() != null)
        {
            timesHit++;
        }

        if (timesHit == 1)
        {
            shakePlatform();
            this.GetComponent<SpriteRenderer>().sprite = sprites[1];
        }

        if (timesHit == 2)
        {
            shakePlatform();
            this.GetComponent<SpriteRenderer>().sprite = sprites[2];
        }

        if (timesHit == 3)
        {
            shakePlatform();
            this.GetComponent<SpriteRenderer>().sprite = sprites[3];
        }
    }

    private void shakePlatform()
    {
        var speed = 1.0f; //how fast it shakes
        var amount = 1.0f; //how much it shakes
        float timeSpent = 0f;
        float shakeTime = 2f;

        while (timeSpent < shakeTime)
        {
            var pos = gameObject.transform.position;
            pos.x = Mathf.Sin(Time.time * speed) * amount;

            timeSpent += Time.deltaTime; 
        }
    }

    // Update is called once per frame
    void Update () {
        

        if (timesHit >= 3) {
            DestroyObject(this.gameObject);
        }
	}
}
