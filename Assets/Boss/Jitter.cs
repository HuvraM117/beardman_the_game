using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jitter : MonoBehaviour {

    private Vector3 jitterOffset;
    [SerializeField] private float JITTERFACTOR = 1f;
    [SerializeField] private int JITTERPERIOD = 4;
    private int jitterIteration = 0; // counter for what point we are in the period

	// Use this for initialization
	void Start () {
        UpdateOffset();
	}

    private void FixedUpdate()
    {
        if(jitterIteration++ == JITTERPERIOD)
        {
            jitterIteration = 0;
            UpdateOffset();
        }

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, jitterOffset, Vector3.Distance(transform.localPosition, jitterOffset));
    }

    private void UpdateOffset()
    {
        // subtract .5 to make half the values negative and multiply by jitter factor
        jitterOffset = new Vector3((Random.value -.5f) * JITTERFACTOR, (Random.value - .5f) * JITTERFACTOR, (Random.value - .5f) * JITTERFACTOR);
    }
}
