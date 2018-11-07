using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderState : MonoBehaviour {

    private Slider slider;
    private float SLIDERWIDTHMULTIPLIER = .14f; // how long the slider should be in proportion to the health + beard length

    private void Start()
    {
        slider = gameObject.GetComponent("Slider") as Slider;
        Debug.Log("Slider Found = " + slider == null);
    }

    public void UpdateSlider(float health, float beardLength)
    {
        Debug.Log("health: " + health + "length: " + beardLength);

        gameObject.transform.localScale = new Vector3((health + beardLength) * SLIDERWIDTHMULTIPLIER, 1f, 1f);
        slider.value = health / (health + beardLength);
    }
}
