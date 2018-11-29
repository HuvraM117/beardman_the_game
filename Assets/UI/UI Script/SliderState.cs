using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderState : MonoBehaviour {

    //private Slider slider;
    private float SLIDERWIDTHMULTIPLIER = .14f; // how long the slider should be in proportion to the health + beard length
    [SerializeField] private GameObject healthSegmentPrefab;
    [SerializeField] private GameObject beardSegmentPrefab;
    private GameObject[] beardSegments = new GameObject[100];
    private GameObject[] healthSegments = new GameObject[100];
    private Vector2 beardSegmentOrigin;
    private Vector2 healthSegmentOrigin;
    private float segmentSpacing = 2.11f;

    private void Start()
    {
        beardSegmentOrigin = new Vector2(-1.313f, -0.149f);
        healthSegmentOrigin = new Vector2(1.313f, -0.149f);
        for (int i = 0; i < healthSegments.Length; i++)
        {
            healthSegments[i] = Instantiate(healthSegmentPrefab, transform.position, Quaternion.identity, transform);
            Vector2 anchoredPos = healthSegments[i].GetComponent<RectTransform>().anchoredPosition;
            healthSegments[i].GetComponent<RectTransform>().anchoredPosition = anchoredPos + healthSegmentOrigin + new Vector2(segmentSpacing * i, 0f);
        }
        for (int i = 0; i < beardSegments.Length; i++)
        {
            beardSegments[i] = Instantiate(beardSegmentPrefab, transform.position, Quaternion.identity, transform);
            Vector2 anchoredPos = beardSegments[i].GetComponent<RectTransform>().anchoredPosition;
            beardSegments[i].GetComponent<RectTransform>().anchoredPosition = anchoredPos + beardSegmentOrigin + new Vector2(segmentSpacing * -i, 0f);

        }
        //slider = gameObject.GetComponent("Slider") as Slider;
        //Debug.Log("Slider Found = " + slider == null);
    }

    public void UpdateSlider(int health, int beardLength)
    {
        Debug.Log("health: " + health + "length: " + beardLength);

        // enable all health segments up to health
        for(int i=0; i<health; i++)
        {
            if (!healthSegments[i].activeSelf)
                healthSegments[i].SetActive(true);
        }
        // disable all health segments greater than health
        for(int i=health; i<healthSegments.Length; i++)
        {
            if (healthSegments[i].activeSelf)
                healthSegments[i].SetActive(false);
        }

        // enable all beard segments up to beardLength
        for (int i = 0; i < beardLength; i++)
        {
            if (!beardSegments[i].activeSelf)
                beardSegments[i].SetActive(true);
        }
        // disable all beard segments greater than beardLength
        for (int i = beardLength; i < beardSegments.Length; i++)
        {
            if (beardSegments[i].activeSelf)
                beardSegments[i].SetActive(false);
        }
    }
}
