using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeardAnimationController : MonoBehaviour {

    private enum BeardAnimationState { IDLE, EXTENDING, RETRACTING, PULLING };
    private BeardAnimationState currentAnimationState = BeardAnimationState.IDLE;

    private const int HARDMAXSEGMENTS = 20; // the max number of beard segments that can ever exist (max of max beard length)
    private const float SEGMENTDISTANCE = .1f; // how far apart the segments are
    [SerializeField] private GameObject beardSegmentPrefab;
    private GameObject[] segments = new GameObject[HARDMAXSEGMENTS]; // segments of the beard
    private int visibleSegments = 0; // segments currently "active" (actually active + were active but disabled since behind the player)
    private int trailingSegments = 0; // segments currently "active" but behind the player (so not actually active)
    private int maxSegments; // the current max length of the beard in segments

    private Transform target;
    private Vector3 beardPath;
    private Vector3 beardOrigin;

    private BeardAnimationState nextState = BeardAnimationState.IDLE; // which state to transition to next after the current one

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
	}

    public void WhipBeard(Transform targetTransform)
    {
        if(currentAnimationState != BeardAnimationState.IDLE) { return; }
        nextState = BeardAnimationState.RETRACTING;
        ExtendBeard(targetTransform);
    }

    public void GrappleBeard(Transform targetTransform)
    {
        if (currentAnimationState != BeardAnimationState.IDLE) { return; }
        nextState = BeardAnimationState.PULLING;
        ExtendBeard(targetTransform);
    }

    // extend the beard out to a point
    private void ExtendBeard(Transform targetTransform)
    {
        target = targetTransform;
        maxSegments = (int)((target.position - beardOrigin).magnitude / SEGMENTDISTANCE); // can't use beardPath here as it hasn't been updated yet
        currentAnimationState = BeardAnimationState.EXTENDING;
    }

    private void FixedUpdate()
    {
        beardOrigin = transform.position;
        beardPath = target.position - beardOrigin;
        switch (currentAnimationState)
        {
            case BeardAnimationState.EXTENDING:
                AddBeardSegment();
                break;
            case BeardAnimationState.RETRACTING:
                RemoveBeardSegment();
                break;
            case BeardAnimationState.IDLE:
                break;
            case BeardAnimationState.PULLING:
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
            currentAnimationState = BeardAnimationState.IDLE;
        }
    }

    private void AddBeardSegment()
    {
        // if we've reached max length, transition to the next state, otherwise, lengthen the beard
        if (visibleSegments >= maxSegments)
        {
            currentAnimationState = nextState;
        }
        else
        {
            segments[visibleSegments].SetActive(true);
            visibleSegments++;
        }
    }

    private void RemoveBeardSegment()
    {
        // if we've retracted fully, transition to idle state, otherwize, shorten the beard
        if(visibleSegments == 0)
        {
            currentAnimationState = BeardAnimationState.IDLE;
        }
        else
        {
            visibleSegments--;
            segments[visibleSegments].SetActive(false);
        }
    }
}
