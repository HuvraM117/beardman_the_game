using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeathScreen : MonoBehaviour {
	public GameObject deathBackgroudl;
	public GameObject animationPanel;
	public float animationspped = 1;
	float a=0f;
	// Use this for initialization
	void Start () {
		Image death = deathBackgroudl.GetComponent<Image> ();
		Image animation = animationPanel.GetComponent<Image> ();
		Animator a = animationPanel.GetComponent<Animator> ();
		a.speed = 1;
		Color deathc = death.color;
		deathc.a = 0;
		death.color = deathc;
	}

	// Update is called once per frame
	void Update () {
		Image death = deathBackgroudl.GetComponent<Image> ();
		Image animation = animationPanel.GetComponent<Image> ();
		Animator b = animationPanel.GetComponent<Animator> ();
		Color deathc = death.color;
		deathc.a = a;
		a=a+animationspped*Time.deltaTime;
		death.color = deathc;
		if (a < 250)
			b.StopPlayback ();
		else
			b.StartPlayback ();
}

}