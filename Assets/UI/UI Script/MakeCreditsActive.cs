using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCreditsActive : MonoBehaviour {

	public GameObject creditsMenu;
	public GameObject mainMenu;

	public void onClick(){
		creditsMenu.SetActive (true);
		mainMenu.SetActive (false);
	}
}
