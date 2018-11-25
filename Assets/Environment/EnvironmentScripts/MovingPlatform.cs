using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    [SerializeField] GameObject startPlatform;
    [SerializeField] GameObject destinationPlatform;
    [SerializeField] public float speed;
    [SerializeField] bool LEFTandRIGHT;
    [SerializeField] bool UPandDown;
    Vector3 startingPos;
    Vector3 endPos;

    float scale;

    float b;

    bool goingUp;
    
	// Use this for initialization
	void Start () {
        startingPos = startPlatform.transform.position;
        endPos = destinationPlatform.transform.position;

        float deltaX = endPos.x - startingPos.x;
        float deltaY = endPos.y - startingPos.y;

        scale = Mathf.Abs(deltaY / deltaX);
        //y = mx + b
        b = startingPos.y - scale * startingPos.x;

        goingUp = true;
    }

    void checkIfChangeDirection()
    {
        var pos = startPlatform.transform.position;

        if (UPandDown)
        {

            if (pos.y > endPos.y)
            {
                goingUp = false;
            }
            else if (pos.y < startingPos.y)
            {
                goingUp = true;
            }
        }

        if (LEFTandRIGHT)
        {
            if (pos.x > endPos.x)
            {
                goingUp = false;
            }
            else if (pos.x < startingPos.x)
            {
                goingUp = true;
            }
        }
    }

	void FixedUpdate(){
		var pos = startPlatform.transform.position;
		if (UPandDown)
		{
			if (goingUp)
			{
				float changedX = pos.x + (speed * .0001f);
				float changedY = (changedX * scale) + b;
				startPlatform.transform.position = new Vector3(changedX, changedY, pos.z);
			}
			else
			{
				float changedX = pos.x - (speed * .0001f);
				float changedY = (changedX * scale) + b;
				startPlatform.transform.position = new Vector3(changedX, changedY, pos.z);
			}
		}

		if (LEFTandRIGHT)
		{
			if (goingUp)
			{
				float changedX = pos.x + (speed * .001f);
				float changedY = pos.y; //(changedX * scale) + b;
				startPlatform.transform.position = new Vector3(changedX, changedY, pos.z);
			}
			else
			{
				float changedX = pos.x - (speed * .001f);
				float changedY = pos.y;// (changedX * scale) + b;
				startPlatform.transform.position = new Vector3(changedX, changedY, pos.z);
			}
		}
		checkIfChangeDirection();
	}
	/*
	// Update is called once per frame
	void Update () {

        var pos = startPlatform.transform.position;
        if (UPandDown)
        {
            if (goingUp)
            {
                float changedX = pos.x + (speed * .0001f);
                float changedY = (changedX * scale) + b;
                startPlatform.transform.position = new Vector3(changedX, changedY, pos.z);
            }
            else
            {
                float changedX = pos.x - (speed * .0001f);
                float changedY = (changedX * scale) + b;
                startPlatform.transform.position = new Vector3(changedX, changedY, pos.z);
            }
        }

        if (LEFTandRIGHT)
        {
            if (goingUp)
            {
                float changedX = pos.x + (speed * .001f);
                float changedY = pos.y; //(changedX * scale) + b;
                startPlatform.transform.position = new Vector3(changedX, changedY, pos.z);
            }
            else
            {
                float changedX = pos.x - (speed * .001f);
                float changedY = pos.y;// (changedX * scale) + b;
                startPlatform.transform.position = new Vector3(changedX, changedY, pos.z);
            }
        }
        checkIfChangeDirection();
    }
	*/
	void OnTriggerEnter2D(Collider2D col){


		if (col.gameObject.name == "Feet"){

			col.gameObject.transform.parent.gameObject.transform.parent = startPlatform.transform;

		}
			
	}

	void OnTriggerExit2D(Collider2D col){


		if (col.gameObject.name == "Feet"){
			col.gameObject.transform.parent.gameObject.transform.parent = null;

		}

	}
}
