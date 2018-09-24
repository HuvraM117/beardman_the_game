using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    [SerializeField] GameObject startPlatform;
    [SerializeField] GameObject destinationPlatform;

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

    //THIS only works for up and down right now 
    void checkIfChangeDirection()
    {
        var pos = startPlatform.transform.position;

        if (pos.y > endPos.y)
        {
            goingUp = false;
        }else if (pos.y < startingPos.y)
        {
            goingUp = true;
        }
    }


	// Update is called once per frame
	void Update () {

        var pos = startPlatform.transform.position;
        if (goingUp)
        {
            float changedX = pos.x + 0.001f;
            float changedY = (changedX * scale) + b;
            startPlatform.transform.position = new Vector3(changedX, changedY, pos.z);
        }
        else{
            float changedX = pos.x - 0.001f;
            float changedY = (changedX * scale) + b;
            startPlatform.transform.position = new Vector3(changedX, changedY, pos.z);
        }
        checkIfChangeDirection();
    }
}
