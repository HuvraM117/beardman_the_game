using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeardAnimationController : MonoBehaviour {



    private const int HARDMAXSEGMENTS = 100; // the max number of beard segments that can ever exist (max of max beard length)
    private const float SEGMENTDISTANCE = .1f; // how far apart the segments are
    private Vector3 beardTipOffset = new Vector3(0f, .3f, 0f); // so the beard tip is centered on the beard
    [SerializeField] private GameObject beardSegmentPrefab;
    [SerializeField] private GameObject beardTipPrefab;
    private GameObject[] segments = new GameObject[HARDMAXSEGMENTS]; // segments of the beard
    private GameObject beardTip; // the collider on the tip of the beard
    private int visibleSegments = 0; // segments currently "active" (actually active + were active but disabled since behind the player)
    private int trailingSegments = 0; // segments currently "active" but behind the player (so not actually active)
    private int maxSegments; // the current max length of the beard in segments

    private Transform target;
    private Vector3 beardPath;
    private Vector3 beardOrigin;

    private BeardState nextState = BeardState.IDLE; // which state to transition to next after the current one

	// Use this for initialization
	void Start () {
        target = gameObject.transform;
        // uses object pooling so we don't waste resources spawning and destroying beard segments
		for(int i=0; i<HARDMAXSEGMENTS; i++)
        {
            segments[i] = Instantiate(beardSegmentPrefab);
            segments[i].transform.parent = transform;
            segments[i].SetActive(false);
        }
        beardTip = Instantiate(beardTipPrefab);
	}

    public void WhipBeard(Transform targetTransform)
    {
        if(PlayerState.CurrentBeardState != BeardState.IDLE) { return; }
        nextState = BeardState.RETRACTING;
        ExtendBeard(targetTransform);
    }

    public void GrappleBeard(Transform targetTransform)
    {
        if (PlayerState.CurrentBeardState != BeardState.IDLE) { return; }
        nextState = BeardState.PULLING;
        ExtendBeard(targetTransform);
    }

    // extend the beard out to a point
    private void ExtendBeard(Transform targetTransform)
    {
        target = targetTransform;
        maxSegments = (int)((target.position - beardOrigin).magnitude / SEGMENTDISTANCE); // can't use beardPath here as it hasn't been updated yet
        PlayerState.CurrentBeardState = BeardState.EXTENDING;
        beardTip.GetComponent<Collider2D>().enabled = true;
    }

    private void FixedUpdate()
    {
        beardOrigin = transform.position;
        beardPath = target.position - beardOrigin;
        switch (PlayerState.CurrentBeardState)
        {
            case BeardState.EXTENDING:
                AddBeardSegment();
                break;
            case BeardState.RETRACTING:
                RemoveBeardSegment();
                break;
            case BeardState.IDLE:
                break;
            case BeardState.PULLING:
                RemoveTrailingBeardSegments();
                break;
            default:
                Debug.Log("Invalid beard animation state");
                break;
        }
        UpdateCurrentBeardSegments();
    }

    private void UpdateCurrentBeardSegments()
    { 
        // relocate every beard segment to lie along the vector
        for (int i=0; i<visibleSegments; i++)
        {
            segments[i].transform.position = beardOrigin + beardPath.normalized * SEGMENTDISTANCE * i;
        }

        // relocate the beard tip
        if(visibleSegments > 0)
            beardTip.transform.position = segments[visibleSegments - 1].transform.position + beardTipOffset;

    }

    private void RemoveTrailingBeardSegments()
    {
        // if we've pulled the player all the way, transition to the next state, and remove segments that are now trailing behind the player
        // NOTE: the actual physics of pulling the player should be handled somewhere else, this class is for animation states only
        for(int i=trailingSegments; i<visibleSegments; i++)
        {
            // if the segment is farther from the target than the player
            if(beardPath.sqrMagnitude <= (segments[i].transform.position - beardOrigin).magnitude)
            {
                segments[i].SetActive(false);
                trailingSegments++;
            }
        }
        if (visibleSegments == trailingSegments)
        {
            visibleSegments = 0;
            trailingSegments = 0;
            PlayerState.CurrentBeardState = BeardState.IDLE;
        }
    }

    private void AddBeardSegment()
    {
        // if we've reached max length, transition to the next state, otherwise, lengthen the beard
        if (visibleSegments >= maxSegments)
        {
            PlayerState.CurrentBeardState = nextState;
        }
        else
        {
            //disable old collider
            if (visibleSegments > 0)
                segments[visibleSegments - 1].GetComponent<Collider2D>().enabled = false;
            segments[visibleSegments].SetActive(true);
            //enable new collider
            segments[visibleSegments].GetComponent<Collider2D>().enabled = true;
            visibleSegments++;
        }
    }

    private void RemoveBeardSegment()
    {
        // if we've retracted fully, transition to idle state, otherwize, shorten the beard
        if(visibleSegments == 0)
        {
            beardTip.GetComponent<Collider2D>().enabled = false;
            PlayerState.CurrentBeardState = BeardState.IDLE;
        }
        else
        {
            //disable old collider
            segments[visibleSegments].GetComponent<Collider2D>().enabled = false;
            visibleSegments--;
            segments[visibleSegments].SetActive(false);
            //enable new collider
            if (visibleSegments > 0)
                segments[visibleSegments-1].GetComponent<Collider2D>().enabled = true;
        }
    }
}
