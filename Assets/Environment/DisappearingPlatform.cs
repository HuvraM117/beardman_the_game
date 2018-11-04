using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{

    private static BoxCollider2D boxCol;
    private int timesHit;
    private Sprite[] sprites;
    private float speed = 30.0f; //how fast it shakes
    private float amount = 0.02f; //how much it shakes
    private bool shake;
    private float timeSpent = 0f;
    private float shakeTime = 1f;
    [SerializeField] bool StayGone;

    // Use this for initialization
    void Start()
    {
        timesHit = 0;

        //get needed assets 
        sprites = Resources.LoadAll<Sprite>("Environment");
        shake = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerState>() != null)
        {
            timesHit++;
        }

        if (timesHit == 1)
        {
            shake = true;
            this.GetComponent<SpriteRenderer>().sprite = sprites[1];
        }

        if (timesHit == 2)
        {
            shake = true;
            this.GetComponent<SpriteRenderer>().sprite = sprites[2];
        }

        if (timesHit == 3)
        {
            shake = true;
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
        shake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shake)
        {

            var pos = gameObject.transform.position;
            pos.x = pos.x + Mathf.Sin(Time.time * speed) * amount;

            gameObject.transform.position = pos;

            if (timeSpent > shakeTime)
            {
                shake = false;
                timeSpent = 0f;
            }
            else
            {
                timeSpent += Time.deltaTime;
            }
        }

        if (timesHit >= 3)
        {

            StartCoroutine(removePlatform());

        }
    }

    IEnumerator removePlatform()
    {
        Debug.Log(StayGone);
        if (!StayGone)
        {

            this.GetComponent<SpriteRenderer>().sprite = null;
            BoxCollider2D bc = this.GetComponent<BoxCollider2D>();

            this.GetComponent<BoxCollider2D>().enabled = false;

            //wait a few seconds
            yield return new WaitForSeconds(5);

            //set game object as inactive 
            this.GetComponent<SpriteRenderer>().sprite = sprites[0];

            this.GetComponent<BoxCollider2D>().enabled = true;

            //reset times hit counter
            timesHit = 0;

        }
        else // destroy object 
        {
            yield return new WaitForSeconds(.2f);
            Debug.Log("destroy");
            DestroyObject(this.gameObject);
        }
    }
}
