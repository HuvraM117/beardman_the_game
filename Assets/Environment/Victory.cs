﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Victory : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (countdown ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator countdown(){

		yield return new WaitForSeconds (10f);
		SceneManager.LoadScene (0);
	}
}